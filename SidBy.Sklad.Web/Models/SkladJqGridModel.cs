using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Trirand.Web.Mvc;

namespace SidBy.Sklad.Web.Models
{
    public class SkladJqGridModel
    {
        public SkladJqGridModel()
        {
            #region LegalEntityGrid

            LegalEntityGrid = new JQGrid
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn {
                        DataField = "LegalEntityId",
                        PrimaryKey = true,
                        DataType = typeof(int),
                        Editable = false,
                        Visible = false,
                    },

                     new JQGridColumn {
                        DataField = "Name",
                        HeaderText = "Наименование",
                        DataType = typeof(string),
                        Editable = true,
                        EditClientSideValidators = new List<JQGridEditClientSideValidator> 
                            { new RequiredValidator() },
                        Width = 100,
                    },

                    new JQGridColumn {
                        DataField = "Code",
                        HeaderText = "Код",
                        DataType = typeof(string),
                        Editable = true,
                        Width = 100,
                    },

                    new JQGridColumn {
                        DataField = "Phone",
                        HeaderText = "Телефон",
                        Width = 60,
                        DataType = typeof(string),
                        Editable = true,
                    },

                    new JQGridColumn {
                        DataField = "Fax",
                        HeaderText = "Факс",
                        DataType = typeof(string),
                        Editable = true,
                        Visible = false,
                    },

                    new JQGridColumn {
                        DataField = "Email",
                        HeaderText = "Email",
                        Formatter = new EmailFormatter(),
                        DataType = typeof(string),
                        Editable = true,
                        Width = 60,
                        EditClientSideValidators = new List<JQGridEditClientSideValidator> { new EmailValidator() },
                    },

                     new JQGridColumn {
                        DataField = "IsVATPayer",
                        HeaderText = "Плательщик НДС",
                        DataType = typeof(bool),
                        Editable = true,
                        Visible = false,
                        EditType = EditType.CheckBox,
                        EditFieldAttributes = new List<JQGridEditFieldAttribute> 
                            {   
                                new JQGridEditFieldAttribute() { Name = "defaultValue", Value = "true" },
                                new JQGridEditFieldAttribute() { Name = "editoptions", Value = "True:False" }
                            },
                        Formatter = new CheckBoxFormatter(),
                    },

                    new JQGridColumn {
                        DataField = "ActualAddress",
                        HeaderText = "Фактический адрес",
                        DataType = typeof(string),
                        Editable = true,
                        Visible = false,
                        EditType = EditType.TextArea,
                        EditFieldAttributes = new List<JQGridEditFieldAttribute> 
                            {  
                                 new JQGridEditFieldAttribute() { Name = "cols", Value = "50" }
                            },
                    },

                    new JQGridColumn {
                        DataField = "Comment",
                        HeaderText = "Комментарий",
                        DataType = typeof(string),
                        Editable = true,
                        EditType = EditType.TextArea,
                        EditFieldAttributes = new List<JQGridEditFieldAttribute> 
                            {  
                                 new JQGridEditFieldAttribute() { Name = "cols", Value = "50" }
                            },
                    },

                    new JQGridColumn {
                        DataField = "Director",
                        HeaderText = "Директор",
                        DataType = typeof(string),
                        Editable = true,
                        Visible = false,
                    },

                    new JQGridColumn {
                        DataField = "ChiefAccountant",
                        HeaderText = "Главный бухгалтер",
                        DataType = typeof(string),
                        Editable = true,
                        Visible = false,
                    },
                }
            };

            LegalEntityGrid.ToolBarSettings.ShowRefreshButton = true;
            LegalEntityGrid.AutoWidth = true;
            LegalEntityGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.TopAndBottom;
            LegalEntityGrid.Height = Unit.Pixel(300);
            LegalEntityGrid.EditDialogSettings.Width = 450;
            LegalEntityGrid.AddDialogSettings.Width = 450;
            #endregion

            #region MyEmployeesGrid
            MyEmployeesGrid = new JQGrid
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn {
                        DataField = "UserId",
                        PrimaryKey = true,
                        DataType = typeof(int),
                        Editable = false,
                        Visible = false,
                    },

                    new JQGridColumn {
                        DataField = "UserName",
                        HeaderText = "Логин",
                        DataType = typeof(string),
                        Editable = true,
                        //EditClientSideValidators = new List<JQGridEditClientSideValidator> 
                        //    { new RequiredValidator() },
                        Width = 100,
                    },
                    
                    new JQGridColumn {
                        DataField = "DisplayName",
                        HeaderText = "Отображаемое имя",
                        DataType = typeof(string),
                        Editable = true,
                        //EditClientSideValidators = new List<JQGridEditClientSideValidator> 
                        //    { new RequiredValidator() },
                        Width = 100,
                    },

                    new JQGridColumn {
                        DataField = "NewPassword",
                        HeaderText = "Новый пароль",
                        DataType = typeof(string),
                        Editable = true,
                        EditType = EditType.TextBox,
                        Visible = false,
                    },

                    //NewPassword

                    new JQGridColumn {
                        DataField = "UserEmail",
                        HeaderText = "Email",
                        Formatter = new EmailFormatter(),
                        DataType = typeof(string),
                        Editable = true,
                        Width = 60,
                        //EditClientSideValidators = new List<JQGridEditClientSideValidator> { new EmailValidator() },
                    },

                    new JQGridColumn {
                        DataField = "Name",
                        HeaderText = "Имя",
                        DataType = typeof(string),
                        Editable = true,
                        Width = 100,
                    },

                    new JQGridColumn {
                        DataField = "Surname",
                        HeaderText = "Фамилия",
                        DataType = typeof(string),
                        Editable = true,
                        Width = 100,
                    },

                    new JQGridColumn {
                        DataField = "MiddleName",
                        HeaderText = "Отчество",
                        DataType = typeof(string),
                        Editable = true,
                        Visible = false,
                        Width = 100,
                    },

                    new JQGridColumn {
                        DataField = "Phone1",
                        HeaderText = "Телефон 1",
                        Width = 60,
                        DataType = typeof(string),
                        Editable = true,
                    },

                    new JQGridColumn {
                        DataField = "Phone2",
                        HeaderText = "Телефон 2",
                        Visible = false,
                        DataType = typeof(string),
                        Editable = true,
                    },

                    new JQGridColumn {
                        DataField = "Skype",
                        HeaderText = "Skype",
                        Visible = true,
                        DataType = typeof(string),
                        Editable = true,
                    },

                    new JQGridColumn {
                        DataField = "ContactTypeName",
                        HeaderText = " ",
                        DataType = typeof(string),
                        Editable = false,
                        Searchable = false,
                        Width = 1,
                        Formatter = new CustomFormatter {
                            SetAttributesFunction = "colSpanAttrJqGrid",
                        },
                    },

                    new JQGridColumn {
                        DataField = "ContactTypeId",
                        HeaderText = "Тип",
                        DataType = typeof(int),
                        Editable = true,
                        EditType = EditType.DropDown,
                        Searchable = true,
                        SearchToolBarOperation = SearchOperation.IsEqualTo,
                        SearchType = SearchType.DropDown,
                        Formatter = new CustomFormatter {
                            SetAttributesFunction = "colHideAttrJqGrid",
                        },
                    },


                    new JQGridColumn {
                        DataField = "Comment",
                        HeaderText = "Описание",
                        DataType = typeof(string),
                        Editable = true,
                        EditType = EditType.TextArea,
                        Visible = true,
                        EditFieldAttributes = new List<JQGridEditFieldAttribute> 
                            {  
                                 new JQGridEditFieldAttribute() { Name = "cols", Value = "50" }
                            },
                    },

                    new JQGridColumn {
                        DataField = "LegalEntityId",
                        HeaderText = "Организация",
                        Visible = false,
                        Editable = true,
                    },

                }
            };

            MyEmployeesGrid.ToolBarSettings.ShowRefreshButton = true;
            MyEmployeesGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.TopAndBottom;
            MyEmployeesGrid.AutoWidth = true;
            MyEmployeesGrid.Height = Unit.Pixel(300);
            MyEmployeesGrid.EditDialogSettings.Width = 450;
            MyEmployeesGrid.AddDialogSettings.Width = 450;

            #endregion

            #region WarehouseGrid
            WarehouseGrid = new JQGrid
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn {
                        DataField = "WarehouseId",
                        PrimaryKey = true,
                        DataType = typeof(int),
                        Editable = false,
                        Visible = false,
                    },

                    new JQGridColumn {
                        DataField = "Name",
                        HeaderText = "Наименование",
                        DataType = typeof(string),
                        Editable = true,
                        EditClientSideValidators = new List<JQGridEditClientSideValidator> 
                            { new RequiredValidator() },
                        Width = 100,
                    },

                    new JQGridColumn {
                        DataField = "Code",
                        HeaderText = "Код",
                        DataType = typeof(string),
                        Editable = true,
                        Width = 70,
                    },

                    new JQGridColumn {
                        DataField = "Address",
                        HeaderText = "Адрес",
                        DataType = typeof(string),
                        Editable = true,
                        EditType = EditType.TextArea,
                        EditFieldAttributes = new List<JQGridEditFieldAttribute> 
                        {  
                            new JQGridEditFieldAttribute() { Name = "cols", Value = "50" }
                        },
                    },

                    new JQGridColumn {
                        DataField = "Comment",
                        HeaderText = "Комментарий",
                        DataType = typeof(string),
                        Editable = true,
                        EditType = EditType.TextArea,
                        Visible = false,
                        EditFieldAttributes = new List<JQGridEditFieldAttribute> 
                        {  
                            new JQGridEditFieldAttribute() { Name = "cols", Value = "50" }
                        },
                    },

                    new JQGridColumn {
                        DataField = "ParentId",
                        HeaderText = "Группа",
                        Visible = false,
                        Editable = true,
                    },

                }
            };

            WarehouseGrid.ToolBarSettings.ShowRefreshButton = true;
            WarehouseGrid.AutoWidth = true;
            WarehouseGrid.Height = Unit.Pixel(300);
            WarehouseGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.TopAndBottom;
            WarehouseGrid.EditDialogSettings.Width = 450;
            WarehouseGrid.AddDialogSettings.Width = 450;

            #endregion

            #region RegionGrid
            //RegionGrid = new JQGrid
            //{
            //    Columns = new List<JQGridColumn>()
            //    {
            //        new JQGridColumn {
            //            DataField = "RegionId",
            //            PrimaryKey = true,
            //            DataType = typeof(int),
            //            Editable = false,
            //            Visible = false,
            //        },

            //        new JQGridColumn {
            //            DataField = "Name",
            //            HeaderText = "Наименование",
            //            DataType = typeof(string),
            //            Editable = true,
            //            EditClientSideValidators = new List<JQGridEditClientSideValidator> 
            //                { new RequiredValidator() },
            //            Width = 100,
            //        },
            //    }
            //};

            //RegionGrid.ToolBarSettings.ShowRefreshButton = true;
            //RegionGrid.AutoWidth = true;
            //RegionGrid.Height = Unit.Pixel(300);
            //RegionGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.TopAndBottom;
            //RegionGrid.EditDialogSettings.Width = 450;
            //RegionGrid.AddDialogSettings.Width = 450;

            #endregion

            #region ContractorGrid

            ContractorGrid = new JQGrid
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn {
                        DataField = "ContractorId",
                        HeaderText = " ",
                        Width = 60,
                        PrimaryKey = true,
                        DataType = typeof(int),
                        Editable = false,
                        Searchable = false,
                        Sortable = false,
                        Visible = true,
                        Formatter = new CustomFormatter {
                             FormatFunction = "formatContactsLink",
                             UnFormatFunction = "unFormatContactsLink",
                        },
                    },

                    //new JQGridColumn {
                    //    DataField = "ContactsLink",
                    //    Sortable = false,
                    //    DataType = typeof(int),
                    //    Visible = true,
                    //    Editable = false,
                    //    Formatter = new CustomFormatter {
                    //         FormatFunction = "formatContactsLink",
                    //         UnFormatFunction = "unFormatContactsLink",
                    //    },
                    //},

                    new JQGridColumn {
                        DataField = "Name",
                        HeaderText = "Наименование",
                        DataType = typeof(string),
                        Editable = true,
                        EditClientSideValidators = new List<JQGridEditClientSideValidator> 
                            { new RequiredValidator() },
                        Width = 100,
                    },

                    new JQGridColumn {
                        DataField = "Code",
                        HeaderText = "Код",
                        DataType = typeof(string),
                        Editable = true,
                    },

                    new JQGridColumn {
                        DataField = "ContactPersonName",
                        HeaderText = "Контактное лицо",
                        DataType = typeof(string),
                        Editable = true,
                        Width = 110,
                    },

                    new JQGridColumn {
                        DataField = "Phone",
                        HeaderText = "Телефон",
                        DataType = typeof(string),
                        Editable = true,
                        Width = 110,
                    },

                    new JQGridColumn {
                        DataField = "Fax",
                        HeaderText = "Факс",
                        DataType = typeof(string),
                        Editable = true,
                        Width = 110,
                    },

                    new JQGridColumn {
                        DataField = "Email",
                        HeaderText = "Email",
                        DataType = typeof(string),
                        Editable = true,
                    },

                    new JQGridColumn {
                        DataField = "Region",
                        HeaderText = "Регион",
                        DataType = typeof(string),
                        Editable = true,
                    },

                    new JQGridColumn {
                        DataField = "ActualAddress",
                        HeaderText = "Фактический адрес",
                        DataType = typeof(string),
                        Editable = true,
                        EditType = EditType.TextArea,
                        EditFieldAttributes = new List<JQGridEditFieldAttribute> 
                        {  
                            new JQGridEditFieldAttribute() { Name = "cols", Value = "50" }
                        },
                    },

                    new JQGridColumn {
                        DataField = "Comment",
                        HeaderText = "Комментарий",
                        DataType = typeof(string),
                        Editable = true,
                        EditType = EditType.TextArea,
                        EditFieldAttributes = new List<JQGridEditFieldAttribute> 
                        {  
                            new JQGridEditFieldAttribute() { Name = "cols", Value = "50" }
                        },
                    },

                    new JQGridColumn {
                        DataField = "MarginAbs",
                        HeaderText = "Наценка",
                        DataType = typeof(decimal),
                        Editable = true,
                        Width = 50,
                        Visible = true,
                    },

                    new JQGridColumn {
                        DataField = "ResponsibleName",
                        HeaderText = " ",
                        DataType = typeof(string),
                        Editable = false,
                        Searchable = false,
                        Width = 1,
                        Formatter = new CustomFormatter {
                            SetAttributesFunction = "colSpanAttrJqGrid",
                        },
                    },

                    new JQGridColumn {
                        DataField = "ResponsibleId",
                        HeaderText = "Ответственный",
                        DataType = typeof(int),
                        Editable = true,
                        EditType = EditType.DropDown,
                        Searchable = true,
                        SearchToolBarOperation = SearchOperation.IsEqualTo,
                        SearchType = SearchType.DropDown,
                        Formatter = new CustomFormatter {
                            SetAttributesFunction = "colHideAttrJqGrid",
                        },
                    },
                    
                    //new JQGridColumn {
                    //    DataField = "RegionId",
                    //    HeaderText = "Регион",
                    //    Editable = true,
                    //    EditType = EditType.DropDown,
                    //    DataType = typeof(int),
                    //    Searchable = true,
                    //    Width = 80,
                    //    SearchToolBarOperation = SearchOperation.IsEqualTo,
                    //    SearchType = SearchType.DropDown,
                    //    Formatter = new CustomFormatter {
                    //        SetAttributesFunction = "colHideAttrJqGrid",
                    //    },
                    //},

                    //new JQGridColumn {
                    //    DataField = "RegionName",
                    //    HeaderText = " ",
                    //    DataType = typeof(string),
                    //    Editable = false,
                    //    Searchable = false,
                    //    Width = 1,
                    //    Formatter = new CustomFormatter {
                    //        SetAttributesFunction = "colSpanAttrJqGrid",
                    //    },
                    //},

                    new JQGridColumn {
                        DataField = "ContractorTypeId",
                        HeaderText = "Тип контрагента",
                        Editable = true,
                        EditType = EditType.DropDown,
                        DataType = typeof(int),
                        Searchable = true,
                        Width = 70,
                        SearchToolBarOperation = SearchOperation.IsEqualTo,
                        SearchType = SearchType.DropDown,
                        Formatter = new CustomFormatter {
                            SetAttributesFunction = "colHideAttrJqGrid",
                        },
                    },

                    new JQGridColumn {
                        DataField = "ContractorTypeName",
                        HeaderText = " ",
                        DataType = typeof(string),
                        Editable = false,
                        Searchable = false,
                        Width = 1,
                        Formatter = new CustomFormatter {
                            SetAttributesFunction = "colSpanAttrJqGrid",
                        },
                    },

                    new JQGridColumn {
                        DataField = "CreatedAt",
                        HeaderText = "Создан",
                        Editable = false,
                        DataType = typeof(DateTime),
                        Searchable = false,
                        Visible = false,
                        Width = 140,
                        
                        //SearchType = SearchType.DatePicker,
                        //SearchToolBarOperation = SearchOperation.IsEqualTo,
                    },
                }
            };

            ContractorGrid.PagerSettings = new Trirand.Web.Mvc.PagerSettings()
            {
                PageSize = 60,
                PageSizeOptions = "[60,120,180]"
            };

            ContractorGrid.ToolBarSettings.ShowRefreshButton = true;
            ContractorGrid.AutoWidth = true;
            ContractorGrid.Height = Unit.Pixel(300);
            ContractorGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.TopAndBottom;
            ContractorGrid.EditDialogSettings.Width = 450;
            ContractorGrid.AddDialogSettings.Width = 450;

            #endregion

            #region ProductGrid
            ProductGrid = new JQGrid
            {
                Columns = new List<JQGridColumn>()
                {
                
                    new JQGridColumn {
                        DataField = "ProductId",
                        HeaderText = " ",
                        Width = 40,
                        PrimaryKey = true,
                        DataType = typeof(int),
                        Editable = false,
                        Searchable = false,
                        Sortable = false,
                        Visible = false,
                    },

                    new JQGridColumn {
                        DataField = "Article",
                        HeaderText = "Артикул",
                        Width = 100,
                        DataType = typeof(string),
                        Editable = true,
                        Searchable = true,
                        Sortable = true,
                        Visible = true,
                    },
                       
                   new JQGridColumn {
                        DataField = "PurchasePrice",
                        HeaderText = "Цена закупки",
                        Width = 50,
                        DataType = typeof(decimal),
                        Editable = true,
                        Searchable = false,
                        Sortable = true,
                        Visible = true,
                    },

                    new JQGridColumn {
                        DataField = "SalePrice",
                        HeaderText = "Цена продажи",
                        Width = 50,
                        DataType = typeof(decimal),
                        Editable = true,
                        Searchable = false,
                        Sortable = true,
                        Visible = true,
                    },

                     new JQGridColumn {
                        DataField = "VAT",
                        HeaderText = "НДС, %",
                        Width = 40,
                        DataType = typeof(decimal),
                        Editable = true,
                        Searchable = false,
                        Sortable = true,
                        Visible = true,
                    },

                    new JQGridColumn {
                        DataField = "Description",
                        HeaderText = "Описание",
                        DataType = typeof(string),
                        Editable = true,
                        EditType = EditType.TextArea,
                        EditFieldAttributes = new List<JQGridEditFieldAttribute> 
                        {  
                            new JQGridEditFieldAttribute() { Name = "cols", Value = "50" }
                        },
                    },

                    new JQGridColumn {
                        DataField = "ContractorName",
                        HeaderText = " ",
                        DataType = typeof(string),
                        Editable = false,
                        Searchable = false,
                        Width = 1,
                        Formatter = new CustomFormatter {
                            SetAttributesFunction = "colSpanAttrJqGrid",
                        },
                    },

                    new JQGridColumn {
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
                        EditorControlID = "SupplierAutoComplete",
                        Formatter = new CustomFormatter {
                            SetAttributesFunction = "colHideAttrJqGrid",
                        },
                    },
                }
            };

            ProductGrid.ToolBarSettings.ShowRefreshButton = true;
            ProductGrid.AutoWidth = true;
            ProductGrid.Height = Unit.Pixel(300);
            ProductGrid.ToolBarSettings.ShowAddButton = false;
            ProductGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.TopAndBottom;
            ProductGrid.EditDialogSettings.Width = 450;
            ProductGrid.AddDialogSettings.Width = 450;
            #endregion

            #region DocumentGrid
            DocumentGrid = new JQGrid
            {
                Columns = new List<JQGridColumn>()
                {
                
                    new JQGridColumn {
                        DataField = "DocumentId",
                        HeaderText = " ",
                        Width = 10,
                        PrimaryKey = true,
                        DataType = typeof(int),
                        Editable = false,
                        Searchable = false,
                        Sortable = false,
                        Visible = true,
                        Formatter = new CustomFormatter {
                             FormatFunction = "formatDocumentEditLink",
                             UnFormatFunction = "unFormatDocumentEditLink",
                        },
                    },

                    new JQGridColumn {
                        DataField = "Number",
                        HeaderText = "Номер",
                        Width = 100,
                        DataType = typeof(string),
                        Editable = true,
                        Searchable = true,
                        Sortable = true,
                        Visible = true,
                    },

                    new JQGridColumn {
                        DataField = "CreatedOf",
                        HeaderText = "Дата создания документа",
                        DataType = typeof(DateTime),
                        Width = 140,
                        Searchable = true,
                        SearchToolBarOperation = SearchOperation.IsEqualTo,
                        SearchType = SearchType.DatePicker,
                        SearchControlID = "CreatedOfPicker",
                        DataFormatString = "{0:dd.MM.yyyy}",
                    },

                      new JQGridColumn {
                        DataField = "PlannedDate",
                        HeaderText = "Планируемая дата отгрузки",
                        DataType = typeof(DateTime),
                        Width = 140,
                        Searchable = true,
                        SearchToolBarOperation = SearchOperation.IsLessThan,
                        SearchType = SearchType.DatePicker,
                        SearchControlID = "PlannedDatePicker",
                        DataFormatString = "{0:dd.MM.yyyy}",
                        Visible = false,
                    },

                    new JQGridColumn {
                        DataField = "IsCommitted",
                        HeaderText = "Пров.",
                        Width = 20,
                        DataType = typeof(bool),
                        Editable = true,
                        Searchable = true,
                        Sortable = true,
                        SearchType = SearchType.DropDown,
                    },
                       
                   new JQGridColumn {
                        DataField = "Sum",
                        HeaderText = "Сумма",
                        Width = 40,
                        DataType = typeof(decimal),
                        Editable = true,
                        Searchable = true,
                        Sortable = true,
                        Visible = true,
                    },

                     new JQGridColumn {
                        DataField = "SaleSum",
                        HeaderText = "Сумма продажи",
                        Width = 40,
                        DataType = typeof(decimal),
                        Editable = true,
                        Searchable = true,
                        Sortable = true,
                        Visible = true,
                    },

                    new JQGridColumn {
                        DataField = "Comment",
                        HeaderText = "Комментарий",
                        DataType = typeof(string),
                        Editable = true,
                        EditType = EditType.TextArea,
                        EditFieldAttributes = new List<JQGridEditFieldAttribute> 
                        {  
                            new JQGridEditFieldAttribute() { Name = "cols", Value = "50" }
                        },
                    },

                    new JQGridColumn {
                        DataField = "EmployeeName",
                        HeaderText = " ",
                        DataType = typeof(string),
                        Editable = false,
                        Searchable = false,
                        Width = 1,
                        Visible = false,
                        Formatter = new CustomFormatter {
                            SetAttributesFunction = "colSpanAttrJqGrid",
                        },
                    },

                    new JQGridColumn {
                        DataField = "EmployeeId",
                        HeaderText = "Сотрудник",
                        DataType = typeof(int),
                        Editable = false,
                        EditType = EditType.DropDown,
                        Searchable = true,
                        Visible = false,
                        SearchToolBarOperation = SearchOperation.IsEqualTo,
                        SearchType = SearchType.DropDown,
                        Formatter = new CustomFormatter {
                            SetAttributesFunction = "colHideAttrJqGrid",
                        },
                    },

                    new JQGridColumn {
                        DataField = "ContractorName",
                        HeaderText = " ",
                        DataType = typeof(string),
                        Editable = false,
                        Searchable = false,
                        Width = 1,
                        Visible = false,
                        Formatter = new CustomFormatter {
                            SetAttributesFunction = "colSpanAttrJqGrid",
                        },
                    },

                    new JQGridColumn {
                        Width = 100,
                        DataField = "ContractorId",
                        HeaderText = "Контрагент",
                        DataType = typeof(int),
                        Editable = false,
                        EditType = EditType.AutoComplete,
                       // Searchable = true,
                        Visible = false,
                        SearchToolBarOperation = SearchOperation.BeginsWith,
                        SearchType = SearchType.AutoComplete,
                        SearchControlID = "ContractorAutoComplete",
                        EditorControlID = "ContractorAutoComplete",
                        Formatter = new CustomFormatter {
                            SetAttributesFunction = "colHideAttrJqGrid",
                        },
                    },
                }
            };

            DocumentGrid.PagerSettings = new Trirand.Web.Mvc.PagerSettings()
            {
                PageSize = 60,
                PageSizeOptions = "[60,120,180]"
            };

            DocumentGrid.SortSettings = new SortSettings() {
                InitialSortColumn = "CreatedOf",
                InitialSortDirection = Trirand.Web.Mvc.SortDirection.Desc
            };

            DocumentGrid.ToolBarSettings.ShowRefreshButton = true;
            DocumentGrid.ToolBarSettings.ShowEditButton = false;
            DocumentGrid.AutoWidth = true;
            DocumentGrid.Height = Unit.Pixel(300);
            DocumentGrid.ToolBarSettings.ShowAddButton = false;
            DocumentGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.TopAndBottom;
            DocumentGrid.EditDialogSettings.Width = 450;
            DocumentGrid.AddDialogSettings.Width = 450;
            #endregion

            #region ProductLineGrid
            ProductLineGrid = new JQGrid
            {
                Columns = new List<JQGridColumn>()
                {
                 
                    new JQGridColumn {
                        DataField = "ProductLineId",
                        HeaderText = " ",
                        Width = 40,
                        PrimaryKey = true,
                        DataType = typeof(int),
                        Editable = false,
                        Searchable = false,
                        Sortable = false,
                        Visible = false,
                    },

                    new JQGridColumn {
                        DataField = "SupplierCode",
                        HeaderText = "Код фабрики",
                        Width = 100,
                        DataType = typeof(string),
                        Editable = false,
                        Searchable = true,
                        Sortable = true,
                        Visible = false,
                    },

                    new JQGridColumn {
                        DataField = "ProductArticle",
                        HeaderText = "Артикул",
                        Width = 100,
                        DataType = typeof(string),
                        Editable = false,
                        Searchable = true,
                        Sortable = true,
                        Visible = true,
                    },

                    new JQGridColumn {
                        DataField = "PurchasePrice",
                        HeaderText = "Цена закупки",
                        Width = 50,
                        DataType = typeof(decimal),
                        Editable = true,
                        Searchable = false,
                        Sortable = true,
                        Visible = true,
                    },

                    new JQGridColumn {
                        DataField = "Quantity",
                        HeaderText = "Кол.",
                        Width = 30,
                        DataType = typeof(int),
                        Editable = true,
                        Searchable = false,
                        Sortable = false,
                        Visible = true,
                        GroupSummaryType = GroupSummaryType.Sum,
                    },

                    new JQGridColumn {
                        DataField = "MarginAbs",
                        HeaderText = "Наценка",
                        Width = 50,
                        DataType = typeof(decimal),
                        Editable = false,
                        Searchable = false,
                        Sortable = true,
                        Visible = false,
                    },

                    new JQGridColumn {
                        DataField = "Discount",
                        HeaderText = "Скидка",
                        Width = 50,
                        DataType = typeof(decimal),
                        Editable = true,
                        Searchable = false,
                        Sortable = true,
                        Visible = true,
                    },

                     new JQGridColumn {
                        DataField = "SalePrice",
                        HeaderText = "Цена продажи",
                        Width = 50,
                        DataType = typeof(decimal),
                        Editable = false,
                        Searchable = false,
                        Sortable = true,
                        Visible = false,
                    },

                    new JQGridColumn {
                        DataField = "Sum",
                        HeaderText = "Сумма",
                        Width = 50,
                        DataType = typeof(decimal),
                        Editable = false,
                        Searchable = false,
                        Sortable = true,
                        Visible = true,
                        GroupSummaryType = GroupSummaryType.Sum,
                    },

                      new JQGridColumn {
                        DataField = "SaleSum",
                        HeaderText = "Сумма продажи",
                        Width = 50,
                        DataType = typeof(decimal),
                        Editable = false,
                        Searchable = false,
                        Sortable = true,
                        Visible = false,
                        
                        GroupSummaryType = GroupSummaryType.SetCustomSummaryFunc,
                        GroupSummaryFunc = "sum",
                        
                        //Formatter = new NumberFormatter{DecimalPlaces = 2, DecimalSeparator = ","},
                        //decimalSeparator
                        //Formatter = new CustomFormatter {
                        //    SetAttributesFunction = "jqGridDecimalCellFormat",
                        //},
                    
                    },

                    new JQGridColumn {
                        DataField = "Shipped",
                        HeaderText = "Отгружено",
                        Width = 30,
                        DataType = typeof(int),
                        Editable = false,
                        Searchable = false,
                        Sortable = false,
                        Visible = false,
                    },

                    new JQGridColumn {
                        DataField = "Comment",
                        HeaderText = "Комментарий",
                        DataType = typeof(string),
                        Editable = true,
                        EditType = EditType.TextArea,
                        EditFieldAttributes = new List<JQGridEditFieldAttribute> 
                        {  
                            new JQGridEditFieldAttribute() { Name = "cols", Value = "50" }
                        },
                    },

                    new JQGridColumn {
                        DataField = "DocumentId",
                        DataType = typeof(int),
                        Editable = false,
                        Searchable = false,
                        Sortable = false,
                        Visible = false,
                    },

                    new JQGridColumn {
                        DataField = "ProductId",
                        DataType = typeof(int),
                        Editable = false,
                        Searchable = false,
                        Sortable = false,
                        Visible = false,
                    },
                }
            };
            ProductLineGrid.MultiSelect = true;
            ProductLineGrid.PagerSettings = new Trirand.Web.Mvc.PagerSettings()
            {
                PageSize = 60,
                PageSizeOptions = "[60,120,180]"
            };
            ProductLineGrid.GroupSettings = new GroupSettings()
            {
                GroupFields = new List<GroupField>(){new GroupField
                {
                     DataField = "SupplierCode",
                     ShowGroupSummary = true,
                      ShowGroupColumn = false
                }},
                CollapseGroups = false,
            };
           
            
            ProductLineGrid.ToolBarSettings.ShowRefreshButton = true;
            ProductLineGrid.AutoWidth = true;
            ProductLineGrid.Height = Unit.Pixel(300);
            ProductLineGrid.ToolBarSettings.ShowAddButton = false;
            ProductLineGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.TopAndBottom;
            ProductLineGrid.EditDialogSettings.Width = 450;
            ProductLineGrid.AddDialogSettings.Width = 450;
            #endregion
        }

        // Properties
        public JQGrid ContractorGrid { get; set; }
        public JQGrid DocumentGrid { get; set; }
        public JQGrid LegalEntityGrid { get; set; }
        public JQGrid MyEmployeesGrid { get; set; }
        public JQGrid ProductGrid { get; set; }
        public JQGrid ProductLineGrid { get; set; }
        public JQGrid WarehouseGrid { get; set; }
    }
}