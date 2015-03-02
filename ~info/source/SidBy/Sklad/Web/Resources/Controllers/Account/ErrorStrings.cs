namespace SidBy.Sklad.Web.Resources.Controllers.Account
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode, CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    public class ErrorStrings
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal ErrorStrings()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        public static string CurrentPasswordIncorrectOrNewPasswordInvalid
        {
            get
            {
                return ResourceManager.GetString("CurrentPasswordIncorrectOrNewPasswordInvalid", resourceCulture);
            }
        }

        public static string DuplicateUserName
        {
            get
            {
                return ResourceManager.GetString("DuplicateUserName", resourceCulture);
            }
        }

        public static string MembershipCreateStatusDuplicateEmail
        {
            get
            {
                return ResourceManager.GetString("MembershipCreateStatusDuplicateEmail", resourceCulture);
            }
        }

        public static string MembershipCreateStatusInvalidAnswer
        {
            get
            {
                return ResourceManager.GetString("MembershipCreateStatusInvalidAnswer", resourceCulture);
            }
        }

        public static string MembershipCreateStatusInvalidEmail
        {
            get
            {
                return ResourceManager.GetString("MembershipCreateStatusInvalidEmail", resourceCulture);
            }
        }

        public static string MembershipCreateStatusInvalidPassword
        {
            get
            {
                return ResourceManager.GetString("MembershipCreateStatusInvalidPassword", resourceCulture);
            }
        }

        public static string MembershipCreateStatusInvalidQuestion
        {
            get
            {
                return ResourceManager.GetString("MembershipCreateStatusInvalidQuestion", resourceCulture);
            }
        }

        public static string MembershipCreateStatusInvalidUserName
        {
            get
            {
                return ResourceManager.GetString("MembershipCreateStatusInvalidUserName", resourceCulture);
            }
        }

        public static string MembershipCreateStatusProviderError
        {
            get
            {
                return ResourceManager.GetString("MembershipCreateStatusProviderError", resourceCulture);
            }
        }

        public static string MembershipCreateStatusUknownError
        {
            get
            {
                return ResourceManager.GetString("MembershipCreateStatusUknownError", resourceCulture);
            }
        }

        public static string MembershipCreateStatusUserRejected
        {
            get
            {
                return ResourceManager.GetString("MembershipCreateStatusUserRejected", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("SidBy.Sklad.Web.Resources.Controllers.Account.ErrorStrings", typeof(ErrorStrings).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        public static string UnableToCreateLocalAccountMayAlreadyExist
        {
            get
            {
                return ResourceManager.GetString("UnableToCreateLocalAccountMayAlreadyExist", resourceCulture);
            }
        }

        public static string UserNameOrPasswordIsIncorrect
        {
            get
            {
                return ResourceManager.GetString("UserNameOrPasswordIsIncorrect", resourceCulture);
            }
        }
    }
}

