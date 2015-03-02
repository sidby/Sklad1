namespace SidBy.Sklad.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.UI.WebControls;
    using Trirand.Web.Mvc;

    public class SkladJqGridModel
    {
        public SkladJqGridModel()
        {
            JQGrid grid = new JQGrid();
            List<JQGridColumn> list = new List<JQGridColumn>();
            JQGridColumn item = new JQGridColumn {
                DataField = "LegalEntityId",
                PrimaryKey = true,
                DataType = typeof(int),
                Editable = false,
                Visible = false
            };
            list.Add(item);
            JQGridColumn column2 = new JQGridColumn {
                DataField = "Name",
                HeaderText = "Наименование",
                DataType = typeof(string),
                Editable = true,
                EditClientSideValidators = new List<JQGridEditClientSideValidator> { new RequiredValidator() },
                Width = 100
            };
            list.Add(column2);
            JQGridColumn column3 = new JQGridColumn {
                DataField = "Code",
                HeaderText = "Код",
                DataType = typeof(string),
                Editable = true,
                Width = 100
            };
            list.Add(column3);
            JQGridColumn column4 = new JQGridColumn {
                DataField = "Phone",
                HeaderText = "Телефон",
                Width = 60,
                DataType = typeof(string),
                Editable = true
            };
            list.Add(column4);
            JQGridColumn column5 = new JQGridColumn {
                DataField = "Fax",
                HeaderText = "Факс",
                DataType = typeof(string),
                Editable = true,
                Visible = false
            };
            list.Add(column5);
            JQGridColumn column6 = new JQGridColumn {
                DataField = "Email",
                HeaderText = "Email",
                Formatter = new EmailFormatter(),
                DataType = typeof(string),
                Editable = true,
                Width = 60,
                EditClientSideValidators = new List<JQGridEditClientSideValidator> { new EmailValidator() }
            };
            list.Add(column6);
            JQGridColumn column7 = new JQGridColumn {
                DataField = "IsVATPayer",
                HeaderText = "Плательщик НДС",
                DataType = typeof(bool),
                Editable = true,
                Visible = false,
                EditType = EditType.CheckBox
            };
            List<JQGridEditFieldAttribute> list4 = new List<JQGridEditFieldAttribute>();
            JQGridEditFieldAttribute attribute = new JQGridEditFieldAttribute {
                Name = "defaultValue",
                Value = "true"
            };
            list4.Add(attribute);
            JQGridEditFieldAttribute attribute2 = new JQGridEditFieldAttribute {
                Name = "editoptions",
                Value = "True:False"
            };
            list4.Add(attribute2);
            column7.EditFieldAttributes = list4;
            column7.Formatter = new CheckBoxFormatter();
            list.Add(column7);
            JQGridColumn column8 = new JQGridColumn {
                DataField = "ActualAddress",
                HeaderText = "Фактический адрес",
                DataType = typeof(string),
                Editable = true,
                Visible = false,
                EditType = EditType.TextArea
            };
            List<JQGridEditFieldAttribute> list5 = new List<JQGridEditFieldAttribute>();
            JQGridEditFieldAttribute attribute3 = new JQGridEditFieldAttribute {
                Name = "cols",
                Value = "50"
            };
            list5.Add(attribute3);
            column8.EditFieldAttributes = list5;
            list.Add(column8);
            JQGridColumn column9 = new JQGridColumn {
                DataField = "Comment",
                HeaderText = "Комментарий",
                DataType = typeof(string),
                Editable = true,
                EditType = EditType.TextArea
            };
            List<JQGridEditFieldAttribute> list6 = new List<JQGridEditFieldAttribute>();
            JQGridEditFieldAttribute attribute4 = new JQGridEditFieldAttribute {
                Name = "cols",
                Value = "50"
            };
            list6.Add(attribute4);
            column9.EditFieldAttributes = list6;
            list.Add(column9);
            JQGridColumn column10 = new JQGridColumn {
                DataField = "Director",
                HeaderText = "Директор",
                DataType = typeof(string),
                Editable = true,
                Visible = false
            };
            list.Add(column10);
            JQGridColumn column11 = new JQGridColumn {
                DataField = "ChiefAccountant",
                HeaderText = "Главный бухгалтер",
                DataType = typeof(string),
                Editable = true,
                Visible = false
            };
            list.Add(column11);
            grid.Columns = list;
            this.LegalEntityGrid = grid;
            this.LegalEntityGrid.ToolBarSettings.ShowRefreshButton = true;
            this.LegalEntityGrid.AutoWidth = true;
            this.LegalEntityGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.TopAndBottom;
            this.LegalEntityGrid.Height = Unit.Pixel(300);
            this.LegalEntityGrid.EditDialogSettings.Width = 450;
            this.LegalEntityGrid.AddDialogSettings.Width = 450;
            JQGrid grid2 = new JQGrid();
            List<JQGridColumn> list7 = new List<JQGridColumn>();
            JQGridColumn column12 = new JQGridColumn {
                DataField = "UserId",
                PrimaryKey = true,
                DataType = typeof(int),
                Editable = false,
                Visible = false
            };
            list7.Add(column12);
            JQGridColumn column13 = new JQGridColumn {
                DataField = "UserName",
                HeaderText = "Логин",
                DataType = typeof(string),
                Editable = true,
                Width = 100
            };
            list7.Add(column13);
            JQGridColumn column14 = new JQGridColumn {
                DataField = "DisplayName",
                HeaderText = "Отображаемое имя",
                DataType = typeof(string),
                Editable = true,
                Width = 100
            };
            list7.Add(column14);
            JQGridColumn column15 = new JQGridColumn {
                DataField = "NewPassword",
                HeaderText = "Новый пароль",
                DataType = typeof(string),
                Editable = true,
                EditType = EditType.TextBox,
                Visible = false
            };
            list7.Add(column15);
            JQGridColumn column16 = new JQGridColumn {
                DataField = "UserEmail",
                HeaderText = "Email",
                Formatter = new EmailFormatter(),
                DataType = typeof(string),
                Editable = true,
                Width = 60
            };
            list7.Add(column16);
            JQGridColumn column17 = new JQGridColumn {
                DataField = "Name",
                HeaderText = "Имя",
                DataType = typeof(string),
                Editable = true,
                Width = 100
            };
            list7.Add(column17);
            JQGridColumn column18 = new JQGridColumn {
                DataField = "Surname",
                HeaderText = "Фамилия",
                DataType = typeof(string),
                Editable = true,
                Width = 100
            };
            list7.Add(column18);
            JQGridColumn column19 = new JQGridColumn {
                DataField = "MiddleName",
                HeaderText = "Отчество",
                DataType = typeof(string),
                Editable = true,
                Visible = false,
                Width = 100
            };
            list7.Add(column19);
            JQGridColumn column20 = new JQGridColumn {
                DataField = "Phone1",
                HeaderText = "Телефон 1",
                Width = 60,
                DataType = typeof(string),
                Editable = true
            };
            list7.Add(column20);
            JQGridColumn column21 = new JQGridColumn {
                DataField = "Phone2",
                HeaderText = "Телефон 2",
                Visible = false,
                DataType = typeof(string),
                Editable = true
            };
            list7.Add(column21);
            JQGridColumn column22 = new JQGridColumn {
                DataField = "Skype",
                HeaderText = "Skype",
                Visible = true,
                DataType = typeof(string),
                Editable = true
            };
            list7.Add(column22);
            JQGridColumn column23 = new JQGridColumn {
                DataField = "ContactTypeName",
                HeaderText = " ",
                DataType = typeof(string),
                Editable = false,
                Searchable = false,
                Width = 1
            };
            CustomFormatter formatter = new CustomFormatter {
                SetAttributesFunction = "colSpanAttrJqGrid"
            };
            column23.Formatter = formatter;
            list7.Add(column23);
            JQGridColumn column24 = new JQGridColumn {
                DataField = "ContactTypeId",
                HeaderText = "Тип",
                DataType = typeof(int),
                Editable = true,
                EditType = EditType.DropDown,
                Searchable = true,
                SearchToolBarOperation = SearchOperation.IsEqualTo,
                SearchType = SearchType.DropDown
            };
            CustomFormatter formatter2 = new CustomFormatter {
                SetAttributesFunction = "colHideAttrJqGrid"
            };
            column24.Formatter = formatter2;
            list7.Add(column24);
            JQGridColumn column25 = new JQGridColumn {
                DataField = "Comment",
                HeaderText = "Описание",
                DataType = typeof(string),
                Editable = true,
                EditType = EditType.TextArea,
                Visible = true
            };
            List<JQGridEditFieldAttribute> list8 = new List<JQGridEditFieldAttribute>();
            JQGridEditFieldAttribute attribute5 = new JQGridEditFieldAttribute {
                Name = "cols",
                Value = "50"
            };
            list8.Add(attribute5);
            column25.EditFieldAttributes = list8;
            list7.Add(column25);
            JQGridColumn column26 = new JQGridColumn {
                DataField = "LegalEntityId",
                HeaderText = "Организация",
                Visible = false,
                Editable = true
            };
            list7.Add(column26);
            grid2.Columns = list7;
            this.MyEmployeesGrid = grid2;
            this.MyEmployeesGrid.ToolBarSettings.ShowRefreshButton = true;
            this.MyEmployeesGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.TopAndBottom;
            this.MyEmployeesGrid.AutoWidth = true;
            this.MyEmployeesGrid.Height = Unit.Pixel(300);
            this.MyEmployeesGrid.EditDialogSettings.Width = 450;
            this.MyEmployeesGrid.AddDialogSettings.Width = 450;
            JQGrid grid3 = new JQGrid();
            List<JQGridColumn> list9 = new List<JQGridColumn>();
            JQGridColumn column27 = new JQGridColumn {
                DataField = "WarehouseId",
                PrimaryKey = true,
                DataType = typeof(int),
                Editable = false,
                Visible = false
            };
            list9.Add(column27);
            JQGridColumn column28 = new JQGridColumn {
                DataField = "Name",
                HeaderText = "Наименование",
                DataType = typeof(string),
                Editable = true,
                EditClientSideValidators = new List<JQGridEditClientSideValidator> { new RequiredValidator() },
                Width = 100
            };
            list9.Add(column28);
            JQGridColumn column29 = new JQGridColumn {
                DataField = "Code",
                HeaderText = "Код",
                DataType = typeof(string),
                Editable = true,
                Width = 70
            };
            list9.Add(column29);
            JQGridColumn column30 = new JQGridColumn {
                DataField = "Address",
                HeaderText = "Адрес",
                DataType = typeof(string),
                Editable = true,
                EditType = EditType.TextArea
            };
            List<JQGridEditFieldAttribute> list11 = new List<JQGridEditFieldAttribute>();
            JQGridEditFieldAttribute attribute6 = new JQGridEditFieldAttribute {
                Name = "cols",
                Value = "50"
            };
            list11.Add(attribute6);
            column30.EditFieldAttributes = list11;
            list9.Add(column30);
            JQGridColumn column31 = new JQGridColumn {
                DataField = "Comment",
                HeaderText = "Комментарий",
                DataType = typeof(string),
                Editable = true,
                EditType = EditType.TextArea,
                Visible = false
            };
            List<JQGridEditFieldAttribute> list12 = new List<JQGridEditFieldAttribute>();
            JQGridEditFieldAttribute attribute7 = new JQGridEditFieldAttribute {
                Name = "cols",
                Value = "50"
            };
            list12.Add(attribute7);
            column31.EditFieldAttributes = list12;
            list9.Add(column31);
            JQGridColumn column32 = new JQGridColumn {
                DataField = "ParentId",
                HeaderText = "Группа",
                Visible = false,
                Editable = true
            };
            list9.Add(column32);
            grid3.Columns = list9;
            this.WarehouseGrid = grid3;
            this.WarehouseGrid.ToolBarSettings.ShowRefreshButton = true;
            this.WarehouseGrid.AutoWidth = true;
            this.WarehouseGrid.Height = Unit.Pixel(300);
            this.WarehouseGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.TopAndBottom;
            this.WarehouseGrid.EditDialogSettings.Width = 450;
            this.WarehouseGrid.AddDialogSettings.Width = 450;
            JQGrid grid4 = new JQGrid();
            List<JQGridColumn> list13 = new List<JQGridColumn>();
            JQGridColumn column33 = new JQGridColumn {
                DataField = "ContractorId",
                HeaderText = " ",
                Width = 60,
                PrimaryKey = true,
                DataType = typeof(int),
                Editable = false,
                Searchable = false,
                Sortable = false,
                Visible = true
            };
            CustomFormatter formatter3 = new CustomFormatter {
                FormatFunction = "formatContactsLink",
                UnFormatFunction = "unFormatContactsLink"
            };
            column33.Formatter = formatter3;
            list13.Add(column33);
            JQGridColumn column34 = new JQGridColumn {
                DataField = "Name",
                HeaderText = "Наименование",
                DataType = typeof(string),
                Editable = true,
                EditClientSideValidators = new List<JQGridEditClientSideValidator> { new RequiredValidator() },
                Width = 100
            };
            list13.Add(column34);
            JQGridColumn column35 = new JQGridColumn {
                DataField = "Code",
                HeaderText = "Код",
                DataType = typeof(string),
                Editable = true
            };
            list13.Add(column35);
            JQGridColumn column36 = new JQGridColumn {
                DataField = "ContactPersonName",
                HeaderText = "Контактное лицо",
                DataType = typeof(string),
                Editable = true,
                Width = 110
            };
            list13.Add(column36);
            JQGridColumn column37 = new JQGridColumn {
                DataField = "Phone",
                HeaderText = "Телефон",
                DataType = typeof(string),
                Editable = true,
                Width = 110
            };
            list13.Add(column37);
            JQGridColumn column38 = new JQGridColumn {
                DataField = "Fax",
                HeaderText = "Факс",
                DataType = typeof(string),
                Editable = true,
                Width = 110
            };
            list13.Add(column38);
            JQGridColumn column39 = new JQGridColumn {
                DataField = "Email",
                HeaderText = "Email",
                DataType = typeof(string),
                Editable = true
            };
            list13.Add(column39);
            JQGridColumn column40 = new JQGridColumn {
                DataField = "Region",
                HeaderText = "Регион",
                DataType = typeof(string),
                Editable = true
            };
            list13.Add(column40);
            JQGridColumn column41 = new JQGridColumn {
                DataField = "ActualAddress",
                HeaderText = "Фактический адрес",
                DataType = typeof(string),
                Editable = true,
                EditType = EditType.TextArea
            };
            List<JQGridEditFieldAttribute> list15 = new List<JQGridEditFieldAttribute>();
            JQGridEditFieldAttribute attribute8 = new JQGridEditFieldAttribute {
                Name = "cols",
                Value = "50"
            };
            list15.Add(attribute8);
            column41.EditFieldAttributes = list15;
            list13.Add(column41);
            JQGridColumn column42 = new JQGridColumn {
                DataField = "Comment",
                HeaderText = "Комментарий",
                DataType = typeof(string),
                Editable = true,
                EditType = EditType.TextArea
            };
            List<JQGridEditFieldAttribute> list16 = new List<JQGridEditFieldAttribute>();
            JQGridEditFieldAttribute attribute9 = new JQGridEditFieldAttribute {
                Name = "cols",
                Value = "50"
            };
            list16.Add(attribute9);
            column42.EditFieldAttributes = list16;
            list13.Add(column42);
            JQGridColumn column43 = new JQGridColumn {
                DataField = "MarginAbs",
                HeaderText = "Наценка",
                DataType = typeof(decimal),
                Editable = true,
                Width = 50,
                Visible = true
            };
            list13.Add(column43);
            JQGridColumn column44 = new JQGridColumn {
                DataField = "ResponsibleName",
                HeaderText = " ",
                DataType = typeof(string),
                Editable = false,
                Searchable = false,
                Width = 1
            };
            CustomFormatter formatter4 = new CustomFormatter {
                SetAttributesFunction = "colSpanAttrJqGrid"
            };
            column44.Formatter = formatter4;
            list13.Add(column44);
            JQGridColumn column45 = new JQGridColumn {
                DataField = "ResponsibleId",
                HeaderText = "Ответственный",
                DataType = typeof(int),
                Editable = true,
                EditType = EditType.DropDown,
                Searchable = true,
                SearchToolBarOperation = SearchOperation.IsEqualTo,
                SearchType = SearchType.DropDown
            };
            CustomFormatter formatter5 = new CustomFormatter {
                SetAttributesFunction = "colHideAttrJqGrid"
            };
            column45.Formatter = formatter5;
            list13.Add(column45);
            JQGridColumn column46 = new JQGridColumn {
                DataField = "ContractorTypeId",
                HeaderText = "Тип контрагента",
                Editable = true,
                EditType = EditType.DropDown,
                DataType = typeof(int),
                Searchable = true,
                Width = 70,
                SearchToolBarOperation = SearchOperation.IsEqualTo,
                SearchType = SearchType.DropDown
            };
            CustomFormatter formatter6 = new CustomFormatter {
                SetAttributesFunction = "colHideAttrJqGrid"
            };
            column46.Formatter = formatter6;
            list13.Add(column46);
            JQGridColumn column47 = new JQGridColumn {
                DataField = "ContractorTypeName",
                HeaderText = " ",
                DataType = typeof(string),
                Editable = false,
                Searchable = false,
                Width = 1
            };
            CustomFormatter formatter7 = new CustomFormatter {
                SetAttributesFunction = "colSpanAttrJqGrid"
            };
            column47.Formatter = formatter7;
            list13.Add(column47);
            JQGridColumn column48 = new JQGridColumn {
                DataField = "CreatedAt",
                HeaderText = "Создан",
                Editable = false,
                DataType = typeof(DateTime),
                Searchable = false,
                Visible = false,
                Width = 140
            };
            list13.Add(column48);
            grid4.Columns = list13;
            this.ContractorGrid = grid4;
            PagerSettings settings = new PagerSettings {
                PageSize = 30
            };
            this.ContractorGrid.PagerSettings = settings;
            this.ContractorGrid.ToolBarSettings.ShowRefreshButton = true;
            this.ContractorGrid.AutoWidth = true;
            this.ContractorGrid.Height = Unit.Pixel(300);
            this.ContractorGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.TopAndBottom;
            this.ContractorGrid.EditDialogSettings.Width = 450;
            this.ContractorGrid.AddDialogSettings.Width = 450;
            JQGrid grid5 = new JQGrid();
            List<JQGridColumn> list17 = new List<JQGridColumn>();
            JQGridColumn column49 = new JQGridColumn {
                DataField = "ProductId",
                HeaderText = " ",
                Width = 40,
                PrimaryKey = true,
                DataType = typeof(int),
                Editable = false,
                Searchable = false,
                Sortable = false,
                Visible = false
            };
            list17.Add(column49);
            JQGridColumn column50 = new JQGridColumn {
                DataField = "Article",
                HeaderText = "Артикул",
                Width = 100,
                DataType = typeof(string),
                Editable = true,
                Searchable = true,
                Sortable = true,
                Visible = true
            };
            list17.Add(column50);
            JQGridColumn column51 = new JQGridColumn {
                DataField = "PurchasePrice",
                HeaderText = "Цена закупки",
                Width = 50,
                DataType = typeof(decimal),
                Editable = true,
                Searchable = false,
                Sortable = true,
                Visible = true
            };
            list17.Add(column51);
            JQGridColumn column52 = new JQGridColumn {
                DataField = "SalePrice",
                HeaderText = "Цена продажи",
                Width = 50,
                DataType = typeof(decimal),
                Editable = true,
                Searchable = false,
                Sortable = true,
                Visible = true
            };
            list17.Add(column52);
            JQGridColumn column53 = new JQGridColumn {
                DataField = "VAT",
                HeaderText = "НДС, %",
                Width = 40,
                DataType = typeof(decimal),
                Editable = true,
                Searchable = false,
                Sortable = true,
                Visible = true
            };
            list17.Add(column53);
            JQGridColumn column54 = new JQGridColumn {
                DataField = "Description",
                HeaderText = "Описание",
                DataType = typeof(string),
                Editable = true,
                EditType = EditType.TextArea
            };
            List<JQGridEditFieldAttribute> list18 = new List<JQGridEditFieldAttribute>();
            JQGridEditFieldAttribute attribute10 = new JQGridEditFieldAttribute {
                Name = "cols",
                Value = "50"
            };
            list18.Add(attribute10);
            column54.EditFieldAttributes = list18;
            list17.Add(column54);
            JQGridColumn column55 = new JQGridColumn {
                DataField = "ContractorName",
                HeaderText = " ",
                DataType = typeof(string),
                Editable = false,
                Searchable = false,
                Width = 1
            };
            CustomFormatter formatter8 = new CustomFormatter {
                SetAttributesFunction = "colSpanAttrJqGrid"
            };
            column55.Formatter = formatter8;
            list17.Add(column55);
            JQGridColumn column56 = new JQGridColumn {
                Width = 100,
                DataField = "ContractorId",
                HeaderText = "Фабрика",
                DataType = typeof(int),
                Editable = false,
                EditType = EditType.AutoComplete,
                Searchable = true,
                SearchToolBarOperation = SearchOperation.BeginsWith,
                SearchType = SearchType.AutoComplete,
                SearchControlID = "SupplierAutoComplete",
                EditorControlID = "SupplierAutoComplete"
            };
            CustomFormatter formatter9 = new CustomFormatter {
                SetAttributesFunction = "colHideAttrJqGrid"
            };
            column56.Formatter = formatter9;
            list17.Add(column56);
            grid5.Columns = list17;
            this.ProductGrid = grid5;
            this.ProductGrid.ToolBarSettings.ShowRefreshButton = true;
            this.ProductGrid.AutoWidth = true;
            this.ProductGrid.Height = Unit.Pixel(300);
            this.ProductGrid.ToolBarSettings.ShowAddButton = false;
            this.ProductGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.TopAndBottom;
            this.ProductGrid.EditDialogSettings.Width = 450;
            this.ProductGrid.AddDialogSettings.Width = 450;
            JQGrid grid6 = new JQGrid();
            List<JQGridColumn> list19 = new List<JQGridColumn>();
            JQGridColumn column57 = new JQGridColumn {
                DataField = "DocumentId",
                HeaderText = " ",
                Width = 10,
                PrimaryKey = true,
                DataType = typeof(int),
                Editable = false,
                Searchable = false,
                Sortable = false,
                Visible = true
            };
            CustomFormatter formatter10 = new CustomFormatter {
                FormatFunction = "formatDocumentEditLink",
                UnFormatFunction = "unFormatDocumentEditLink"
            };
            column57.Formatter = formatter10;
            list19.Add(column57);
            JQGridColumn column58 = new JQGridColumn {
                DataField = "Number",
                HeaderText = "Номер",
                Width = 100,
                DataType = typeof(string),
                Editable = true,
                Searchable = true,
                Sortable = true,
                Visible = true
            };
            list19.Add(column58);
            JQGridColumn column59 = new JQGridColumn {
                DataField = "CreatedOf",
                HeaderText = "Дата создания документа",
                DataType = typeof(DateTime),
                Width = 140,
                Searchable = true,
                SearchToolBarOperation = SearchOperation.IsEqualTo,
                SearchType = SearchType.DatePicker,
                SearchControlID = "CreatedOfPicker",
                DataFormatString = "{0:dd.MM.yyyy}"
            };
            list19.Add(column59);
            JQGridColumn column60 = new JQGridColumn {
                DataField = "PlannedDate",
                HeaderText = "Планируемая дата отгрузки",
                DataType = typeof(DateTime),
                Width = 140,
                Searchable = true,
                SearchToolBarOperation = SearchOperation.IsLessThan,
                SearchType = SearchType.DatePicker,
                SearchControlID = "PlannedDatePicker",
                DataFormatString = "{0:dd.MM.yyyy}",
                Visible = false
            };
            list19.Add(column60);
            JQGridColumn column61 = new JQGridColumn {
                DataField = "IsCommitted",
                HeaderText = "Пров.",
                Width = 20,
                DataType = typeof(bool),
                Editable = true,
                Searchable = true,
                Sortable = true,
                Visible = true,
                SearchType = SearchType.DropDown
            };
            list19.Add(column61);
            JQGridColumn column62 = new JQGridColumn {
                DataField = "Sum",
                HeaderText = "Сумма",
                Width = 40,
                DataType = typeof(decimal),
                Editable = true,
                Searchable = true,
                Sortable = true,
                Visible = true
            };
            list19.Add(column62);
            JQGridColumn column63 = new JQGridColumn {
                DataField = "SaleSum",
                HeaderText = "Сумма продажи",
                Width = 40,
                DataType = typeof(decimal),
                Editable = true,
                Searchable = true,
                Sortable = true,
                Visible = false
            };
            list19.Add(column63);
            JQGridColumn column64 = new JQGridColumn {
                DataField = "Comment",
                HeaderText = "Комментарий",
                DataType = typeof(string),
                Editable = true,
                EditType = EditType.TextArea
            };
            List<JQGridEditFieldAttribute> list20 = new List<JQGridEditFieldAttribute>();
            JQGridEditFieldAttribute attribute11 = new JQGridEditFieldAttribute {
                Name = "cols",
                Value = "50"
            };
            list20.Add(attribute11);
            column64.EditFieldAttributes = list20;
            list19.Add(column64);
            JQGridColumn column65 = new JQGridColumn {
                DataField = "EmployeeName",
                HeaderText = " ",
                DataType = typeof(string),
                Editable = false,
                Searchable = false,
                Width = 1,
                Visible = false
            };
            CustomFormatter formatter11 = new CustomFormatter {
                SetAttributesFunction = "colSpanAttrJqGrid"
            };
            column65.Formatter = formatter11;
            list19.Add(column65);
            JQGridColumn column66 = new JQGridColumn {
                DataField = "EmployeeId",
                HeaderText = "Сотрудник",
                DataType = typeof(int),
                Editable = false,
                EditType = EditType.DropDown,
                Searchable = true,
                Visible = false,
                SearchToolBarOperation = SearchOperation.IsEqualTo,
                SearchType = SearchType.DropDown
            };
            CustomFormatter formatter12 = new CustomFormatter {
                SetAttributesFunction = "colHideAttrJqGrid"
            };
            column66.Formatter = formatter12;
            list19.Add(column66);
            JQGridColumn column67 = new JQGridColumn {
                DataField = "ContractorName",
                HeaderText = " ",
                DataType = typeof(string),
                Editable = false,
                Searchable = false,
                Width = 1,
                Visible = false
            };
            CustomFormatter formatter13 = new CustomFormatter {
                SetAttributesFunction = "colSpanAttrJqGrid"
            };
            column67.Formatter = formatter13;
            list19.Add(column67);
            JQGridColumn column68 = new JQGridColumn {
                Width = 100,
                DataField = "ContractorId",
                HeaderText = "Контрагент",
                DataType = typeof(int),
                Editable = false,
                EditType = EditType.AutoComplete,
                Searchable = true,
                Visible = false,
                SearchToolBarOperation = SearchOperation.BeginsWith,
                SearchType = SearchType.AutoComplete,
                SearchControlID = "ContractorAutoComplete",
                EditorControlID = "ContractorAutoComplete"
            };
            CustomFormatter formatter14 = new CustomFormatter {
                SetAttributesFunction = "colHideAttrJqGrid"
            };
            column68.Formatter = formatter14;
            list19.Add(column68);
            grid6.Columns = list19;
            this.DocumentGrid = grid6;
            PagerSettings settings2 = new PagerSettings {
                PageSize = 30
            };
            this.DocumentGrid.PagerSettings = settings2;
            this.DocumentGrid.ToolBarSettings.ShowRefreshButton = true;
            this.DocumentGrid.ToolBarSettings.ShowEditButton = false;
            this.DocumentGrid.AutoWidth = true;
            this.DocumentGrid.Height = Unit.Pixel(300);
            this.DocumentGrid.ToolBarSettings.ShowAddButton = false;
            this.DocumentGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.TopAndBottom;
            this.DocumentGrid.EditDialogSettings.Width = 450;
            this.DocumentGrid.AddDialogSettings.Width = 450;
            JQGrid grid7 = new JQGrid();
            List<JQGridColumn> list21 = new List<JQGridColumn>();
            JQGridColumn column69 = new JQGridColumn {
                DataField = "ProductLineId",
                HeaderText = " ",
                Width = 40,
                PrimaryKey = true,
                DataType = typeof(int),
                Editable = false,
                Searchable = false,
                Sortable = false,
                Visible = false
            };
            list21.Add(column69);
            JQGridColumn column70 = new JQGridColumn {
                DataField = "SupplierCode",
                HeaderText = "Код фабрики",
                Width = 100,
                DataType = typeof(string),
                Editable = false,
                Searchable = true,
                Sortable = true,
                Visible = false
            };
            list21.Add(column70);
            JQGridColumn column71 = new JQGridColumn {
                DataField = "ProductArticle",
                HeaderText = "Артикул",
                Width = 100,
                DataType = typeof(string),
                Editable = false,
                Searchable = true,
                Sortable = true,
                Visible = true
            };
            list21.Add(column71);
            JQGridColumn column72 = new JQGridColumn {
                DataField = "Quantity",
                HeaderText = "Кол.",
                Width = 30,
                DataType = typeof(int),
                Editable = true,
                Searchable = false,
                Sortable = false,
                Visible = true,
                GroupSummaryType = GroupSummaryType.Sum
            };
            list21.Add(column72);
            JQGridColumn column73 = new JQGridColumn {
                DataField = "PurchasePrice",
                HeaderText = "Цена закупки",
                Width = 50,
                DataType = typeof(decimal),
                Editable = true,
                Searchable = false,
                Sortable = true,
                Visible = true,
                GroupSummaryType = GroupSummaryType.Sum
            };
            list21.Add(column73);
            JQGridColumn column74 = new JQGridColumn {
                DataField = "MarginAbs",
                HeaderText = "Наценка",
                Width = 50,
                DataType = typeof(decimal),
                Editable = false,
                Searchable = false,
                Sortable = true,
                Visible = false
            };
            list21.Add(column74);
            JQGridColumn column75 = new JQGridColumn {
                DataField = "Discount",
                HeaderText = "Скидка",
                Width = 50,
                DataType = typeof(decimal),
                Editable = true,
                Searchable = false,
                Sortable = true,
                Visible = true
            };
            list21.Add(column75);
            JQGridColumn column76 = new JQGridColumn {
                DataField = "SalePrice",
                HeaderText = "Цена продажи",
                Width = 50,
                DataType = typeof(decimal),
                Editable = false,
                Searchable = false,
                Sortable = true,
                Visible = false,
                GroupSummaryType = GroupSummaryType.Sum
            };
            list21.Add(column76);
            JQGridColumn column77 = new JQGridColumn {
                DataField = "Sum",
                HeaderText = "Сумма",
                Width = 50,
                DataType = typeof(decimal),
                Editable = false,
                Searchable = false,
                Sortable = true,
                Visible = true,
                GroupSummaryType = GroupSummaryType.Sum
            };
            list21.Add(column77);
            JQGridColumn column78 = new JQGridColumn {
                DataField = "SaleSum",
                HeaderText = "Сумма продажи",
                Width = 50,
                DataType = typeof(decimal),
                Editable = false,
                Searchable = false,
                Sortable = true,
                Visible = false,
                GroupSummaryType = GroupSummaryType.Sum
            };
            list21.Add(column78);
            JQGridColumn column79 = new JQGridColumn {
                DataField = "Shipped",
                HeaderText = "Отгружено",
                Width = 30,
                DataType = typeof(int),
                Editable = false,
                Searchable = false,
                Sortable = false,
                Visible = false
            };
            list21.Add(column79);
            JQGridColumn column80 = new JQGridColumn {
                DataField = "Comment",
                HeaderText = "Комментарий",
                DataType = typeof(string),
                Editable = true,
                EditType = EditType.TextArea
            };
            List<JQGridEditFieldAttribute> list22 = new List<JQGridEditFieldAttribute>();
            JQGridEditFieldAttribute attribute12 = new JQGridEditFieldAttribute {
                Name = "cols",
                Value = "50"
            };
            list22.Add(attribute12);
            column80.EditFieldAttributes = list22;
            list21.Add(column80);
            JQGridColumn column81 = new JQGridColumn {
                DataField = "DocumentId",
                DataType = typeof(int),
                Editable = false,
                Searchable = false,
                Sortable = false,
                Visible = false
            };
            list21.Add(column81);
            JQGridColumn column82 = new JQGridColumn {
                DataField = "ProductId",
                DataType = typeof(int),
                Editable = false,
                Searchable = false,
                Sortable = false,
                Visible = false
            };
            list21.Add(column82);
            grid7.Columns = list21;
            this.ProductLineGrid = grid7;
            PagerSettings settings3 = new PagerSettings {
                PageSize = 30
            };
            this.ProductLineGrid.PagerSettings = settings3;
            GroupSettings settings4 = new GroupSettings();
            List<GroupField> list23 = new List<GroupField>();
            GroupField field = new GroupField {
                DataField = "SupplierCode",
                ShowGroupSummary = true,
                ShowGroupColumn = false
            };
            list23.Add(field);
            settings4.GroupFields = list23;
            settings4.CollapseGroups = false;
            this.ProductLineGrid.GroupSettings = settings4;
            this.ProductLineGrid.ToolBarSettings.ShowRefreshButton = true;
            this.ProductLineGrid.AutoWidth = true;
            this.ProductLineGrid.Height = Unit.Pixel(300);
            this.ProductLineGrid.ToolBarSettings.ShowAddButton = false;
            this.ProductLineGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.TopAndBottom;
            this.ProductLineGrid.EditDialogSettings.Width = 450;
            this.ProductLineGrid.AddDialogSettings.Width = 450;
        }

        public JQGrid ContractorGrid { get; set; }

        public JQGrid DocumentGrid { get; set; }

        public JQGrid LegalEntityGrid { get; set; }

        public JQGrid MyEmployeesGrid { get; set; }

        public JQGrid ProductGrid { get; set; }

        public JQGrid ProductLineGrid { get; set; }

        public JQGrid WarehouseGrid { get; set; }
    }
}

