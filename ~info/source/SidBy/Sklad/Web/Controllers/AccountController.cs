namespace SidBy.Sklad.Web.Controllers
{
    using DotNetOpenAuth.AspNet;
    using Microsoft.Web.WebPages.OAuth;
    using SidBy.Sklad.DataAccess;
    using SidBy.Sklad.Domain;
    using SidBy.Sklad.Web.Filters;
    using SidBy.Sklad.Web.Models;
    using SidBy.Sklad.Web.Resources.Controllers.Account;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Transactions;
    using System.Web.Mvc;
    using System.Web.Security;
    using WebMatrix.WebData;

    [Authorize, InitializeSimpleMembership]
    public class AccountController : Controller
    {
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            string userName = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? nullable = null;
            if (userName == base.User.Identity.Name)
            {
                TransactionOptions transactionOptions = new TransactionOptions {
                    IsolationLevel = IsolationLevel.Serializable
                };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
                {
                    if (OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(base.User.Identity.Name)) || (OAuthWebSecurity.GetAccountsFromUserName(base.User.Identity.Name).Count > 1))
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        nullable = 2;
                    }
                }
            }
            return base.RedirectToAction("Manage", new { Message = nullable });
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.InvalidUserName:
                    return ErrorStrings.MembershipCreateStatusInvalidUserName;

                case MembershipCreateStatus.InvalidPassword:
                    return ErrorStrings.MembershipCreateStatusInvalidPassword;

                case MembershipCreateStatus.InvalidQuestion:
                    return ErrorStrings.MembershipCreateStatusInvalidQuestion;

                case MembershipCreateStatus.InvalidAnswer:
                    return ErrorStrings.MembershipCreateStatusInvalidAnswer;

                case MembershipCreateStatus.InvalidEmail:
                    return ErrorStrings.MembershipCreateStatusInvalidEmail;

                case MembershipCreateStatus.DuplicateUserName:
                    return ErrorStrings.DuplicateUserName;

                case MembershipCreateStatus.DuplicateEmail:
                    return ErrorStrings.MembershipCreateStatusDuplicateEmail;

                case MembershipCreateStatus.UserRejected:
                    return ErrorStrings.MembershipCreateStatusUserRejected;

                case MembershipCreateStatus.ProviderError:
                    return ErrorStrings.MembershipCreateStatusProviderError;
            }
            return ErrorStrings.MembershipCreateStatusUknownError;
        }

        [AllowAnonymous, ValidateAntiForgeryToken, HttpPost]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, base.Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(base.Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.get_IsSuccessful())
            {
                return base.RedirectToAction("ExternalLoginFailure");
            }
            if (OAuthWebSecurity.Login(result.get_Provider(), result.get_ProviderUserId(), false))
            {
                return this.RedirectToLocal(returnUrl);
            }
            if (base.User.Identity.IsAuthenticated)
            {
                OAuthWebSecurity.CreateOrUpdateAccount(result.get_Provider(), result.get_ProviderUserId(), base.User.Identity.Name);
                return this.RedirectToLocal(returnUrl);
            }
            string str = OAuthWebSecurity.SerializeProviderUserId(result.get_Provider(), result.get_ProviderUserId());
            ((dynamic) base.ViewBag).ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.get_Provider()).get_DisplayName();
            ((dynamic) base.ViewBag).ReturnUrl = returnUrl;
            RegisterExternalLoginModel model = new RegisterExternalLoginModel {
                UserName = result.get_UserName(),
                ExternalLoginData = str
            };
            return base.View("ExternalLoginConfirmation", model);
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string str = null;
            string str2 = null;
            if (!(!base.User.Identity.IsAuthenticated && OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, ref str, ref str2)))
            {
                return base.RedirectToAction("Manage");
            }
            if (base.ModelState.IsValid)
            {
                using (UsersContext context = new UsersContext())
                {
                    ParameterExpression expression;
                    if (context.get_UserProfiles().FirstOrDefault<UserProfile>(Expression.Lambda<Func<UserProfile, bool>>(Expression.Equal(Expression.Call(Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "u"), (MethodInfo) methodof(UserProfile.get_UserName)), (MethodInfo) methodof(string.ToLower), new Expression[0]), Expression.Call(Expression.Property(Expression.Constant(model), (MethodInfo) methodof(RegisterExternalLoginModel.get_UserName)), (MethodInfo) methodof(string.ToLower), new Expression[0]), false, (MethodInfo) methodof(string.op_Equality)), new ParameterExpression[] { expression })) == null)
                    {
                        UserProfile profile2 = new UserProfile();
                        profile2.set_UserName(model.UserName);
                        context.get_UserProfiles().Add(profile2);
                        context.SaveChanges();
                        OAuthWebSecurity.CreateOrUpdateAccount(str, str2, model.UserName);
                        OAuthWebSecurity.Login(str, str2, false);
                        return this.RedirectToLocal(returnUrl);
                    }
                    base.ModelState.AddModelError("UserName", ErrorStrings.DuplicateUserName);
                }
            }
            ((dynamic) base.ViewBag).ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(str).get_DisplayName();
            ((dynamic) base.ViewBag).ReturnUrl = returnUrl;
            return base.View(model);
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return base.View();
        }

        [AllowAnonymous, ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ((dynamic) base.ViewBag).ReturnUrl = returnUrl;
            return this.PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.get_RegisteredClientData());
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ((dynamic) base.ViewBag).ReturnUrl = returnUrl;
            return base.View();
        }

        [ValidateAntiForgeryToken, HttpPost, AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (base.ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, model.RememberMe))
            {
                return this.RedirectToLocal(returnUrl);
            }
            base.ModelState.AddModelError("", ErrorStrings.UserNameOrPasswordIsIncorrect);
            return base.View(model);
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            return base.RedirectToAction("Index", "Indicators");
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool flag = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(base.User.Identity.Name));
            ((dynamic) base.ViewBag).HasLocalPassword = flag;
            ((dynamic) base.ViewBag).ReturnUrl = base.Url.Action("Manage");
            if (flag)
            {
                if (base.ModelState.IsValid)
                {
                    bool flag2;
                    try
                    {
                        flag2 = WebSecurity.ChangePassword(base.User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        flag2 = false;
                    }
                    if (flag2)
                    {
                        return base.RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    base.ModelState.AddModelError("", ErrorStrings.CurrentPasswordIncorrectOrNewPasswordInvalid);
                }
            }
            else
            {
                ModelState state = base.ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }
                if (base.ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(base.User.Identity.Name, model.NewPassword, false);
                        return base.RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception)
                    {
                        base.ModelState.AddModelError("", string.Format(ErrorStrings.UnableToCreateLocalAccountMayAlreadyExist, base.User.Identity.Name));
                    }
                }
            }
            return base.View(model);
        }

        public ActionResult Manage(ManageMessageId? message)
        {
            ManageMessageId? nullable;
            ((dynamic) base.ViewBag).StatusMessage = (((ManageMessageId) message) == ManageMessageId.ChangePasswordSuccess) ? StatusMessageStrings.PasswordChanged : (((((ManageMessageId) (nullable = message).GetValueOrDefault()) == ManageMessageId.SetPasswordSuccess) && nullable.HasValue) ? StatusMessageStrings.PasswordSet : (((((ManageMessageId) (nullable = message).GetValueOrDefault()) == ManageMessageId.RemoveLoginSuccess) && nullable.HasValue) ? StatusMessageStrings.ExternalLoginWasRemoved : ""));
            ((dynamic) base.ViewBag).HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(base.User.Identity.Name));
            ((dynamic) base.ViewBag).ReturnUrl = base.Url.Action("Manage");
            return base.View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (base.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            return base.RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return base.View();
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (base.ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password, null, false);
                    WebSecurity.Login(model.UserName, model.Password, false);
                    return base.RedirectToAction("Contractor", "Reference");
                }
                catch (MembershipCreateUserException exception)
                {
                    base.ModelState.AddModelError("", ErrorCodeToString(exception.StatusCode));
                }
            }
            return base.View(model);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> accountsFromUserName = OAuthWebSecurity.GetAccountsFromUserName(base.User.Identity.Name);
            List<SidBy.Sklad.Web.Models.ExternalLogin> model = new List<SidBy.Sklad.Web.Models.ExternalLogin>();
            foreach (OAuthAccount account in accountsFromUserName)
            {
                AuthenticationClientData oAuthClientData = OAuthWebSecurity.GetOAuthClientData(account.get_Provider());
                SidBy.Sklad.Web.Models.ExternalLogin item = new SidBy.Sklad.Web.Models.ExternalLogin {
                    Provider = account.get_Provider(),
                    ProviderDisplayName = oAuthClientData.get_DisplayName(),
                    ProviderUserId = account.get_ProviderUserId()
                };
                model.Add(item);
            }
            ((dynamic) base.ViewBag).ShowRemoveButton = (model.Count > 1) || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(base.User.Identity.Name));
            return this.PartialView("_RemoveExternalLoginsPartial", model);
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                this.Provider = provider;
                this.ReturnUrl = returnUrl;
            }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(this.Provider, this.ReturnUrl);
            }

            public string Provider { get; private set; }

            public string ReturnUrl { get; private set; }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess
        }
    }
}

