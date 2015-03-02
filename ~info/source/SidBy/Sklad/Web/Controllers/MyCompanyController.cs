namespace SidBy.Sklad.Web.Controllers
{
    using log4net;
    using SidBy.Common.Helpers;
    using SidBy.Sklad.DataAccess;
    using SidBy.Sklad.Domain;
    using SidBy.Sklad.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Security;
    using Trirand.Web.Mvc;
    using WebMatrix.WebData;

    [Authorize(Roles="admin,employee")]
    public class MyCompanyController : Controller
    {
        private readonly ILog logger;

        public MyCompanyController()
        {
            this.logger = LogManager.GetLogger(base.GetType());
        }

        public void AddContactById(int contractorId, int userId)
        {
            if ((contractorId > 0) && (userId > 0))
            {
                ParameterExpression expression;
                SkladDataContext context = new SkladDataContext();
                UserProfile profile = QueryableExtensions.Include<UserProfile, ICollection<Contractor>>(context.get_UserProfiles(), Expression.Lambda<Func<UserProfile, ICollection<Contractor>>>(Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "x"), (MethodInfo) methodof(UserProfile.get_Contractors)), new ParameterExpression[] { expression })).Where<UserProfile>(Expression.Lambda<Func<UserProfile, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "x"), (MethodInfo) methodof(UserProfile.get_UserId)), Expression.Constant(userId)), new ParameterExpression[] { expression })).First<UserProfile>();
                Contractor item = context.get_Contractors().Where<Contractor>(Expression.Lambda<Func<Contractor, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(Contractor), "x"), (MethodInfo) methodof(Contractor.get_ContractorId)), Expression.Constant(contractorId)), new ParameterExpression[] { expression })).FirstOrDefault<Contractor>();
                profile.get_Contractors().Add(item);
                context.SaveChanges();
                this.logger.InfoFormat("у контрагента {0} появился контакт {1}", item.get_Code(), profile.get_DisplayName());
            }
        }

        public void AddNewContact(int contractorId, string displayName, string name, string surname, string phone1, string userEmail, string skype)
        {
            UserProfile item;
            if ((contractorId > 0) && !string.IsNullOrEmpty(displayName))
            {
                UserProfile profile2 = new UserProfile();
                profile2.set_DisplayName(displayName);
                profile2.set_Name(name);
                profile2.set_Surname(surname);
                profile2.set_Phone1(phone1);
                profile2.set_UserEmail(userEmail);
                profile2.set_Skype(skype);
                item = profile2;
                SkladDataContext context = new SkladDataContext();
                this.PrepareUsersData(item);
                item.set_ContactTypeId(3);
                string str = this.ValidateUsersData(context.get_UserProfiles(), item, true);
                if (!string.IsNullOrEmpty(str))
                {
                    this.logger.Error(str);
                }
                else
                {
                    ParameterExpression expression;
                    item.set_NewPassword(Membership.GeneratePassword(5, 1));
                    WebSecurity.CreateUserAndAccount(item.get_UserName(), item.get_NewPassword(), null, false);
                    UserProfile profile = context.get_UserProfiles().Where<UserProfile>(Expression.Lambda<Func<UserProfile, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "x"), (MethodInfo) methodof(UserProfile.get_UserName)), Expression.Property(Expression.Constant(item), (MethodInfo) methodof(UserProfile.get_UserName)), false, (MethodInfo) methodof(string.op_Equality)), new ParameterExpression[] { expression })).First<UserProfile>();
                    this.UpdateMyEmployees(profile, item);
                    Contractor contractor = context.get_Contractors().Where<Contractor>(Expression.Lambda<Func<Contractor, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(Contractor), "x"), (MethodInfo) methodof(Contractor.get_ContractorId)), Expression.Constant(contractorId)), new ParameterExpression[] { expression })).FirstOrDefault<Contractor>();
                    profile.get_Contractors().Add(contractor);
                    context.SaveChanges();
                    this.logger.InfoFormat("добавлен контакт {0} с паролем - {1}", item.get_DisplayName(), item.get_NewPassword());
                }
            }
        }

        public ActionResult ContactList(int contractorId)
        {
            ParameterExpression expression;
            if (contractorId <= 0)
            {
                return null;
            }
            ContactListModel model = new ContactListModel();
            SkladJqGridModel model2 = new SkladJqGridModel();
            JQGrid myEmployeesGrid = model2.MyEmployeesGrid;
            SkladDataContext context = new SkladDataContext();
            Contractor contractor = QueryableExtensions.Include<Contractor, UserProfile>(context.get_Contractors().Where<Contractor>(Expression.Lambda<Func<Contractor, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(Contractor), "x"), (MethodInfo) methodof(Contractor.get_ContractorId)), Expression.Constant(contractorId)), new ParameterExpression[] { expression })), Expression.Lambda<Func<Contractor, UserProfile>>(Expression.Property(expression = Expression.Parameter(typeof(Contractor), "x"), (MethodInfo) methodof(Contractor.get_Responsible)), new ParameterExpression[] { expression })).First<Contractor>();
            model.Company = contractor;
            this.ContactListSetupGrid(myEmployeesGrid);
            model.Grid = myEmployeesGrid;
            return base.View(model);
        }

        public ActionResult ContactListEditRows(UserProfile editedItem)
        {
            UserProfile profile;
            ParameterExpression expression;
            int contractorId = 0;
            int.TryParse(base.Request.QueryString["contractorId"], out contractorId);
            if (contractorId <= 0)
            {
                return null;
            }
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            if (model.MyEmployeesGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                this.PrepareUsersData(editedItem);
                string str = this.ValidateUsersData(context.get_UserProfiles(), editedItem, false);
                if (!string.IsNullOrEmpty(str))
                {
                    return model.MyEmployeesGrid.ShowEditValidationMessage(str);
                }
                profile = context.get_UserProfiles().Where<UserProfile>(Expression.Lambda<Func<UserProfile, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "x"), (MethodInfo) methodof(UserProfile.get_UserId)), Expression.Property(Expression.Constant(editedItem), (MethodInfo) methodof(UserProfile.get_UserId))), new ParameterExpression[] { expression })).First<UserProfile>();
                if (profile.get_ContactTypeId() != 3)
                {
                    return null;
                }
                this.UpdateMyEmployees(profile, editedItem);
                context.SaveChanges();
                this.logger.InfoFormat("контакт {0} изменён", profile.get_DisplayName());
                if (!string.IsNullOrEmpty(editedItem.get_NewPassword()))
                {
                    WebSecurity.ResetPassword(WebSecurity.GeneratePasswordResetToken(editedItem.get_UserName(), 0x5a0), editedItem.get_NewPassword());
                    this.logger.InfoFormat("у контакта {0} был изменён пароль", editedItem.get_DisplayName());
                }
            }
            if (model.MyEmployeesGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
            {
                profile = QueryableExtensions.Include<UserProfile, ICollection<Contractor>>(context.get_UserProfiles(), Expression.Lambda<Func<UserProfile, ICollection<Contractor>>>(Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "x"), (MethodInfo) methodof(UserProfile.get_Contractors)), new ParameterExpression[] { expression })).Where<UserProfile>(Expression.Lambda<Func<UserProfile, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "x"), (MethodInfo) methodof(UserProfile.get_UserId)), Expression.Property(Expression.Constant(editedItem), (MethodInfo) methodof(UserProfile.get_UserId))), new ParameterExpression[] { expression })).First<UserProfile>();
                Contractor item = context.get_Contractors().Where<Contractor>(Expression.Lambda<Func<Contractor, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(Contractor), "x"), (MethodInfo) methodof(Contractor.get_ContractorId)), Expression.Constant(contractorId)), new ParameterExpression[] { expression })).FirstOrDefault<Contractor>();
                profile.get_Contractors().Remove(item);
                context.SaveChanges();
            }
            return base.RedirectToAction("ContactList", "MyCompany", new { contractorId = contractorId });
        }

        public JsonResult ContactListSearchGridDataRequested()
        {
            ParameterExpression expression;
            int contractorId = 0;
            int.TryParse(base.Request.QueryString["contractorId"], out contractorId);
            if (contractorId <= 0)
            {
                return null;
            }
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            this.ContactListSetupGrid(model.MyEmployeesGrid);
            return model.MyEmployeesGrid.DataBind(context.get_Contractors().Where<Contractor>(Expression.Lambda<Func<Contractor, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(Contractor), "x"), (MethodInfo) methodof(Contractor.get_ContractorId)), Expression.Constant(contractorId)), new ParameterExpression[] { expression })).SelectMany<Contractor, UserProfile>(Expression.Lambda<Func<Contractor, IEnumerable<UserProfile>>>(Expression.Property(expression = Expression.Parameter(typeof(Contractor), "x"), (MethodInfo) methodof(Contractor.get_Users)), new ParameterExpression[] { expression })));
        }

        private void ContactListSetupGrid(JQGrid grid)
        {
            int result = 0;
            int.TryParse(base.Request.QueryString["contractorId"], out result);
            if (result > 0)
            {
                this.SetUpGrid(grid, "ContactList1", base.Url.Action("ContactListSearchGridDataRequested", new { contractorId = result }), base.Url.Action("ContactListEditRows"));
                this.SetUpContactListDropDown(grid);
                grid.Height = 100;
                grid.ToolBarSettings.ShowDeleteButton = true;
                grid.ToolBarSettings.ShowAddButton = false;
            }
        }

        public JsonResult GetUsersByDisplayName(string term)
        {
            ParameterExpression expression;
            SkladDataContext context = new SkladDataContext();
            var data = context.get_UserProfiles().Where<UserProfile>(Expression.Lambda<Func<UserProfile, bool>>(Expression.AndAlso(Expression.Call(Expression.Call(Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "u"), (MethodInfo) methodof(UserProfile.get_DisplayName)), (MethodInfo) methodof(string.ToLower), new Expression[0]), (MethodInfo) methodof(string.Contains), new Expression[] { Expression.Call(Expression.Constant(term), (MethodInfo) methodof(string.ToLower), new Expression[0]) }), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(UserProfile.get_ContactTypeId)), Expression.Convert(Expression.Constant(3, typeof(int)), typeof(int?)))), new ParameterExpression[] { expression })).Select(Expression.Lambda(Expression.New((ConstructorInfo) methodof(<>f__AnonymousTypea<int, string, string, string, string, string, string>..ctor, <>f__AnonymousTypea<int, string, string, string, string, string, string>), new Expression[] { Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "u"), (MethodInfo) methodof(UserProfile.get_UserId)), Expression.Property(expression, (MethodInfo) methodof(UserProfile.get_DisplayName)), Expression.Property(expression, (MethodInfo) methodof(UserProfile.get_Name)), Expression.Property(expression, (MethodInfo) methodof(UserProfile.get_UserEmail)), Expression.Property(expression, (MethodInfo) methodof(UserProfile.get_Surname)), Expression.Property(expression, (MethodInfo) methodof(UserProfile.get_Phone1)), Expression.Property(expression, (MethodInfo) methodof(UserProfile.get_Skype)) }, new MethodInfo[] { (MethodInfo) methodof(<>f__AnonymousTypea<int, string, string, string, string, string, string>.get_UserId, <>f__AnonymousTypea<int, string, string, string, string, string, string>), (MethodInfo) methodof(<>f__AnonymousTypea<int, string, string, string, string, string, string>.get_DisplayName, <>f__AnonymousTypea<int, string, string, string, string, string, string>), (MethodInfo) methodof(<>f__AnonymousTypea<int, string, string, string, string, string, string>.get_Name, <>f__AnonymousTypea<int, string, string, string, string, string, string>), (MethodInfo) methodof(<>f__AnonymousTypea<int, string, string, string, string, string, string>.get_UserEmail, <>f__AnonymousTypea<int, string, string, string, string, string, string>), (MethodInfo) methodof(<>f__AnonymousTypea<int, string, string, string, string, string, string>.get_Surname, <>f__AnonymousTypea<int, string, string, string, string, string, string>), (MethodInfo) methodof(<>f__AnonymousTypea<int, string, string, string, string, string, string>.get_Phone1, <>f__AnonymousTypea<int, string, string, string, string, string, string>), (MethodInfo) methodof(<>f__AnonymousTypea<int, string, string, string, string, string, string>.get_Skype, <>f__AnonymousTypea<int, string, string, string, string, string, string>) }), new ParameterExpression[] { expression })).ToList();
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LegalEntity()
        {
            ((dynamic) base.ViewBag).Title = "Юр. лица";
            SkladJqGridModel model = new SkladJqGridModel();
            JQGrid legalEntityGrid = model.LegalEntityGrid;
            this.LegalEntitySetupGrid(legalEntityGrid);
            return base.View(model);
        }

        public ActionResult LegalEntityEditRows(SidBy.Sklad.Domain.LegalEntity editedItem)
        {
            SidBy.Sklad.Domain.LegalEntity entity;
            ParameterExpression expression;
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            if (model.LegalEntityGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                entity = context.get_LegalEntities().Where<SidBy.Sklad.Domain.LegalEntity>(Expression.Lambda<Func<SidBy.Sklad.Domain.LegalEntity, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.LegalEntity), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.LegalEntity.get_LegalEntityId)), Expression.Property(Expression.Constant(editedItem), (MethodInfo) methodof(SidBy.Sklad.Domain.LegalEntity.get_LegalEntityId))), new ParameterExpression[] { expression })).First<SidBy.Sklad.Domain.LegalEntity>();
                this.UpdateLegalEntity(entity, editedItem);
                context.SaveChanges();
            }
            if (model.LegalEntityGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
            {
                entity = new SidBy.Sklad.Domain.LegalEntity();
                this.UpdateLegalEntity(entity, editedItem);
                context.get_LegalEntities().Add(entity);
                context.SaveChanges();
            }
            if (model.LegalEntityGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
            {
                entity = context.get_LegalEntities().Where<SidBy.Sklad.Domain.LegalEntity>(Expression.Lambda<Func<SidBy.Sklad.Domain.LegalEntity, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.LegalEntity), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.LegalEntity.get_LegalEntityId)), Expression.Property(Expression.Constant(editedItem), (MethodInfo) methodof(SidBy.Sklad.Domain.LegalEntity.get_LegalEntityId))), new ParameterExpression[] { expression })).First<SidBy.Sklad.Domain.LegalEntity>();
                context.get_LegalEntities().Remove(entity);
                context.SaveChanges();
            }
            return base.RedirectToAction("LegalEntity", "MyCompany");
        }

        public JsonResult LegalEntitySearchGridDataRequested()
        {
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            this.LegalEntitySetupGrid(model.LegalEntityGrid);
            return model.LegalEntityGrid.DataBind(context.get_LegalEntities());
        }

        private void LegalEntitySetupGrid(JQGrid grid)
        {
            this.SetUpGrid(grid, "LegalEntityGrid1", base.Url.Action("LegalEntitySearchGridDataRequested"), base.Url.Action("LegalEntityEditRows"));
            grid.ToolBarSettings.ShowDeleteButton = false;
        }

        public ActionResult MyEmployees()
        {
            ((dynamic) base.ViewBag).Title = "Контакты";
            SkladJqGridModel model = new SkladJqGridModel();
            JQGrid myEmployeesGrid = model.MyEmployeesGrid;
            this.MyEmployeesSetupGrid(myEmployeesGrid);
            return base.View(model);
        }

        public ActionResult MyEmployeesEditRows(UserProfile editedItem)
        {
            string str;
            UserProfile profile;
            ParameterExpression expression;
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            if (model.MyEmployeesGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                if (!(!(editedItem.get_UserName() == "admin") || base.User.IsInRole("admin")))
                {
                    return model.MyEmployeesGrid.ShowEditValidationMessage("Эту запись изменить нельзя");
                }
                this.PrepareUsersData(editedItem);
                str = this.ValidateUsersData(context.get_UserProfiles(), editedItem, false);
                if (!string.IsNullOrEmpty(str))
                {
                    return model.MyEmployeesGrid.ShowEditValidationMessage(str);
                }
                profile = context.get_UserProfiles().Where<UserProfile>(Expression.Lambda<Func<UserProfile, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "x"), (MethodInfo) methodof(UserProfile.get_UserId)), Expression.Property(Expression.Constant(editedItem), (MethodInfo) methodof(UserProfile.get_UserId))), new ParameterExpression[] { expression })).First<UserProfile>();
                if (editedItem.get_UserId() == profile.get_UserId())
                {
                    this.UpdateMyEmployees(profile, editedItem);
                    context.SaveChanges();
                    this.logger.InfoFormat("изменён контакт {0}", editedItem.get_DisplayName());
                    if (!string.IsNullOrEmpty(editedItem.get_NewPassword()))
                    {
                        WebSecurity.ResetPassword(WebSecurity.GeneratePasswordResetToken(editedItem.get_UserName(), 0x5a0), editedItem.get_NewPassword());
                        this.logger.InfoFormat("изменён пароль контакта {0}", editedItem.get_DisplayName());
                    }
                }
            }
            if (model.MyEmployeesGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
            {
                this.PrepareUsersData(editedItem);
                str = this.ValidateUsersData(context.get_UserProfiles(), editedItem, true);
                if (!string.IsNullOrEmpty(str))
                {
                    return model.MyEmployeesGrid.ShowEditValidationMessage(str);
                }
                if (string.IsNullOrEmpty(editedItem.get_NewPassword()))
                {
                    editedItem.set_NewPassword(Membership.GeneratePassword(5, 1));
                }
                WebSecurity.CreateUserAndAccount(editedItem.get_UserName(), editedItem.get_NewPassword(), null, false);
                profile = context.get_UserProfiles().Where<UserProfile>(Expression.Lambda<Func<UserProfile, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "x"), (MethodInfo) methodof(UserProfile.get_UserName)), Expression.Property(Expression.Constant(editedItem), (MethodInfo) methodof(UserProfile.get_UserName)), false, (MethodInfo) methodof(string.op_Equality)), new ParameterExpression[] { expression })).First<UserProfile>();
                this.UpdateMyEmployees(profile, editedItem);
                context.SaveChanges();
                this.logger.InfoFormat("добавлен контакт {0} с паролем - {1}", editedItem.get_DisplayName(), editedItem.get_NewPassword());
            }
            if (model.MyEmployeesGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
            {
                profile = context.get_UserProfiles().Where<UserProfile>(Expression.Lambda<Func<UserProfile, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "x"), (MethodInfo) methodof(UserProfile.get_UserId)), Expression.Property(Expression.Constant(editedItem), (MethodInfo) methodof(UserProfile.get_UserId))), new ParameterExpression[] { expression })).First<UserProfile>();
                if (profile.get_ContactTypeId() == 1)
                {
                    return model.MyEmployeesGrid.ShowEditValidationMessage("Невозможно удалить сотрудника");
                }
                Membership.DeleteUser(profile.get_UserName());
                this.logger.InfoFormat("удален контакт {0}", editedItem.get_DisplayName());
            }
            return base.RedirectToAction("MyEmployees", "MyCompany");
        }

        public JsonResult MyEmployeesSearchGridDataRequested()
        {
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            this.MyEmployeesSetupGrid(model.MyEmployeesGrid);
            return model.MyEmployeesGrid.DataBind(context.get_UserProfiles());
        }

        private void MyEmployeesSetupGrid(JQGrid grid)
        {
            this.SetUpGrid(grid, "MyEmployeesGrid1", base.Url.Action("MyEmployeesSearchGridDataRequested"), base.Url.Action("MyEmployeesEditRows"));
            grid.ToolBarSettings.ShowDeleteButton = false;
            this.SetUpMyEmployeesDropDown(grid);
        }

        private void PrepareUsersData(UserProfile profile)
        {
            profile.set_UserName((profile.get_UserName() ?? string.Empty).Trim());
            profile.set_DisplayName((profile.get_DisplayName() ?? string.Empty).Trim());
            profile.set_UserEmail((profile.get_UserEmail() ?? string.Empty).Trim());
            profile.set_Surname((profile.get_Surname() ?? string.Empty).Trim());
            profile.set_Name((profile.get_Name() ?? string.Empty).Trim());
            profile.set_MiddleName((profile.get_MiddleName() ?? string.Empty).Trim());
            profile.set_Phone1((profile.get_Phone1() ?? string.Empty).Trim());
            profile.set_Phone2((profile.get_Phone2() ?? string.Empty).Trim());
            profile.set_Skype((profile.get_Skype() ?? string.Empty).Trim());
            profile.set_Comment((profile.get_Comment() ?? string.Empty).Trim());
            if (string.IsNullOrEmpty(profile.get_UserName()))
            {
                profile.set_UserName(profile.get_DisplayName());
            }
            if (string.IsNullOrEmpty(profile.get_DisplayName()))
            {
                profile.set_DisplayName(profile.get_UserName());
            }
            profile.set_UserName(StringHelper.Transliterate(profile.get_UserName(), true));
        }

        private void SetUpContactListDropDown(JQGrid itemGrid)
        {
            itemGrid.Columns.Find(c => c.DataField == "LegalEntityId").Editable = false;
            JQGridColumn column2 = itemGrid.Columns.Find(c => c.DataField == "ContactTypeId");
            column2.Visible = false;
            column2.Editable = false;
            itemGrid.Columns.Find(c => c.DataField == "ContactTypeName").Visible = false;
        }

        private void SetUpGrid(JQGrid grid, string gridID, string dataUrl, string editUrl)
        {
            grid.ID = gridID;
            grid.DataUrl = dataUrl;
            grid.EditUrl = editUrl;
            grid.ToolBarSettings.ShowSearchToolBar = true;
            grid.ToolBarSettings.ShowEditButton = true;
            grid.ToolBarSettings.ShowAddButton = true;
            grid.ToolBarSettings.ShowDeleteButton = true;
            grid.EditDialogSettings.CloseAfterEditing = true;
            grid.AddDialogSettings.CloseAfterAdding = true;
        }

        private void SetUpMyEmployeesDropDown(JQGrid itemGrid)
        {
            JQGridColumn column = itemGrid.Columns.Find(c => c.DataField == "LegalEntityId");
            column.Editable = true;
            column.EditType = EditType.DropDown;
            JQGridColumn column2 = itemGrid.Columns.Find(c => c.DataField == "ContactTypeId");
            column2.Editable = true;
            column2.EditType = EditType.DropDown;
            if (itemGrid.AjaxCallBackMode == AjaxCallBackMode.RequestData)
            {
                ParameterExpression expression;
                SkladDataContext context = new SkladDataContext();
                IEnumerable<SelectListItem> source = from x in (from m in context.get_LegalEntities() select m).AsEnumerable<SidBy.Sklad.Domain.LegalEntity>() select new SelectListItem { Text = x.get_Name(), Value = x.get_LegalEntityId().ToString() };
                column.EditList = source.ToList<SelectListItem>();
                SelectListItem item = new SelectListItem {
                    Text = "(Иная)",
                    Value = ""
                };
                column.EditList.Insert(0, item);
                IEnumerable<SelectListItem> enumerable2 = from x in context.get_ContactTypes().OrderByDescending<ContactType, int>(Expression.Lambda<Func<ContactType, int>>(Expression.Property(expression = Expression.Parameter(typeof(ContactType), "m"), (MethodInfo) methodof(ContactType.get_ContactTypeId)), new ParameterExpression[] { expression })).AsEnumerable<ContactType>() select new SelectListItem { Text = x.get_Name(), Value = x.get_ContactTypeId().ToString() };
                column2.EditList = enumerable2.ToList<SelectListItem>();
                column2.SearchList = enumerable2.ToList<SelectListItem>();
                SelectListItem item2 = new SelectListItem {
                    Text = "Все",
                    Value = ""
                };
                column2.SearchList.Insert(0, item2);
            }
        }

        private void SetUpWarehouseParentIdEditDropDown(JQGrid itemGrid)
        {
            JQGridColumn column = itemGrid.Columns.Find(c => c.DataField == "ParentId");
            column.Editable = true;
            column.EditType = EditType.DropDown;
            SelectListItem item = new SelectListItem {
                Text = "(нет)",
                Value = "null"
            };
            column.EditList.Add(item);
            if (itemGrid.AjaxCallBackMode == AjaxCallBackMode.RequestData)
            {
                SkladDataContext context = new SkladDataContext();
                IEnumerable<SelectListItem> source = from x in (from m in context.get_Warehouses() select m).AsEnumerable<SidBy.Sklad.Domain.Warehouse>() select new SelectListItem { Text = x.get_Name(), Value = x.get_WarehouseId().ToString() };
                column.EditList.AddRange(source.ToList<SelectListItem>());
            }
        }

        private void UpdateLegalEntity(SidBy.Sklad.Domain.LegalEntity item, SidBy.Sklad.Domain.LegalEntity editedItem)
        {
            item.set_Name(editedItem.get_Name());
            item.set_Code(editedItem.get_Code());
            item.set_Phone(editedItem.get_Phone());
            item.set_Fax(editedItem.get_Fax());
            item.set_Email(editedItem.get_Email());
            item.set_IsVATPayer(editedItem.get_IsVATPayer());
            item.set_ActualAddress(editedItem.get_ActualAddress());
            item.set_Comment(editedItem.get_Comment());
            item.set_Director(editedItem.get_Director());
            item.set_ChiefAccountant(editedItem.get_ChiefAccountant());
        }

        private void UpdateMyEmployees(UserProfile item, UserProfile editedItem)
        {
            item.set_UserName(editedItem.get_UserName());
            item.set_DisplayName(editedItem.get_DisplayName());
            item.set_UserEmail(editedItem.get_UserEmail());
            item.set_Surname(editedItem.get_Surname());
            item.set_Name(editedItem.get_Name());
            item.set_MiddleName(editedItem.get_MiddleName());
            item.set_Phone1(editedItem.get_Phone1());
            item.set_Phone2(editedItem.get_Phone2());
            item.set_Skype(editedItem.get_Skype());
            item.set_Comment(editedItem.get_Comment());
            item.set_ContactTypeId(editedItem.get_ContactTypeId());
            item.set_LegalEntityId(editedItem.get_LegalEntityId());
        }

        private void UpdateWarehouse(SidBy.Sklad.Domain.Warehouse item, SidBy.Sklad.Domain.Warehouse editedItem)
        {
            item.set_Name(editedItem.get_Name());
            item.set_Code(editedItem.get_Code());
            item.set_Address(editedItem.get_Address());
            item.set_Comment(editedItem.get_Comment());
            int num = editedItem.get_WarehouseId();
            if (num != editedItem.get_ParentId())
            {
                item.set_ParentId(editedItem.get_ParentId());
            }
        }

        private string ValidateUsersData(DbSet<UserProfile> dbSet, UserProfile profile, bool isNew)
        {
            ParameterExpression expression;
            if (string.IsNullOrEmpty(profile.get_UserName()))
            {
                return "Имя пользователя обязательно";
            }
            if (string.IsNullOrEmpty(profile.get_DisplayName()))
            {
                return "Отображаемое имя обязательно";
            }
            if (isNew)
            {
                if (dbSet.Where<UserProfile>(Expression.Lambda<Func<UserProfile, bool>>(Expression.OrElse(Expression.Equal(Expression.Call(null, (MethodInfo) methodof(string.Compare), new Expression[] { Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "x"), (MethodInfo) methodof(UserProfile.get_DisplayName)), Expression.Property(Expression.Constant(profile), (MethodInfo) methodof(UserProfile.get_DisplayName)), Expression.Constant(StringComparison.OrdinalIgnoreCase, typeof(StringComparison)) }), Expression.Constant(0, typeof(int))), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(UserProfile.get_UserName)), Expression.Property(Expression.Constant(profile), (MethodInfo) methodof(UserProfile.get_UserName)), false, (MethodInfo) methodof(string.op_Equality))), new ParameterExpression[] { expression })).FirstOrDefault<UserProfile>() != null)
                {
                    return "Пользователь с таким именем существует";
                }
            }
            else
            {
                UserProfile profile2 = dbSet.Where<UserProfile>(Expression.Lambda<Func<UserProfile, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "x"), (MethodInfo) methodof(UserProfile.get_UserId)), Expression.Property(Expression.Constant(profile), (MethodInfo) methodof(UserProfile.get_UserId))), new ParameterExpression[] { expression })).First<UserProfile>();
                if ((profile2.get_DisplayName().ToLower() != profile.get_DisplayName().ToLower()) && (dbSet.Where<UserProfile>(Expression.Lambda<Func<UserProfile, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "x"), (MethodInfo) methodof(UserProfile.get_DisplayName)), Expression.Property(Expression.Constant(profile), (MethodInfo) methodof(UserProfile.get_DisplayName)), false, (MethodInfo) methodof(string.op_Equality)), new ParameterExpression[] { expression })).FirstOrDefault<UserProfile>() != null))
                {
                    return "Пользователь с таким именем существует";
                }
                if ((profile2.get_UserName().ToLower() != profile.get_UserName().ToLower()) && (dbSet.Where<UserProfile>(Expression.Lambda<Func<UserProfile, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "x"), (MethodInfo) methodof(UserProfile.get_UserName)), Expression.Property(Expression.Constant(profile), (MethodInfo) methodof(UserProfile.get_UserName)), false, (MethodInfo) methodof(string.op_Equality)), new ParameterExpression[] { expression })).FirstOrDefault<UserProfile>() != null))
                {
                    return "Пользователь с таким именем существует";
                }
            }
            if (!string.IsNullOrEmpty(profile.get_UserEmail()) && !ValidationHelper.IsValidEmail(profile.get_UserEmail()))
            {
                return "Неверный формат email";
            }
            return string.Empty;
        }

        public ActionResult Warehouse()
        {
            ((dynamic) base.ViewBag).Title = "Склады";
            SkladJqGridModel model = new SkladJqGridModel();
            JQGrid warehouseGrid = model.WarehouseGrid;
            this.WarehouseSetupGrid(warehouseGrid);
            return base.View(model);
        }

        public ActionResult WarehouseEditRows(SidBy.Sklad.Domain.Warehouse editedItem)
        {
            SidBy.Sklad.Domain.Warehouse warehouse;
            ParameterExpression expression;
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            if (model.WarehouseGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                warehouse = context.get_Warehouses().Where<SidBy.Sklad.Domain.Warehouse>(Expression.Lambda<Func<SidBy.Sklad.Domain.Warehouse, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Warehouse), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Warehouse.get_WarehouseId)), Expression.Property(Expression.Constant(editedItem), (MethodInfo) methodof(SidBy.Sklad.Domain.Warehouse.get_WarehouseId))), new ParameterExpression[] { expression })).First<SidBy.Sklad.Domain.Warehouse>();
                this.UpdateWarehouse(warehouse, editedItem);
                context.SaveChanges();
            }
            if (model.WarehouseGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
            {
                warehouse = new SidBy.Sklad.Domain.Warehouse();
                this.UpdateWarehouse(warehouse, editedItem);
                context.get_Warehouses().Add(warehouse);
                context.SaveChanges();
            }
            if (model.WarehouseGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
            {
                warehouse = context.get_Warehouses().Where<SidBy.Sklad.Domain.Warehouse>(Expression.Lambda<Func<SidBy.Sklad.Domain.Warehouse, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Warehouse), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Warehouse.get_WarehouseId)), Expression.Property(Expression.Constant(editedItem), (MethodInfo) methodof(SidBy.Sklad.Domain.Warehouse.get_WarehouseId))), new ParameterExpression[] { expression })).First<SidBy.Sklad.Domain.Warehouse>();
                context.get_Warehouses().Remove(warehouse);
                context.SaveChanges();
            }
            return base.RedirectToAction("Warehouse", "MyCompany");
        }

        public JsonResult WarehouseSearchGridDataRequested()
        {
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            this.WarehouseSetupGrid(model.WarehouseGrid);
            return model.WarehouseGrid.DataBind(context.get_Warehouses());
        }

        private void WarehouseSetupGrid(JQGrid grid)
        {
            this.SetUpGrid(grid, "WarehouseGrid1", base.Url.Action("WarehouseSearchGridDataRequested"), base.Url.Action("WarehouseEditRows"));
            grid.ToolBarSettings.ShowDeleteButton = false;
            this.SetUpWarehouseParentIdEditDropDown(grid);
        }
    }
}

