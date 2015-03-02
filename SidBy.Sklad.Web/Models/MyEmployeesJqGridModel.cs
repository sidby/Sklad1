using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Trirand.Web.Mvc;

namespace SidBy.Sklad.Web.Models
{
    public class MyEmployeesJqGridModel
    {
        public MyEmployeesJqGridModel()
        {
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
                        DataField = "UserEmail",
                        HeaderText = "Email",
                        Formatter = new EmailFormatter(),
                        DataType = typeof(string),
                        Editable = true,
                        Width = 60,
                        //EditClientSideValidators = new List<JQGridEditClientSideValidator> { new EmailValidator() },
                    },

                    new JQGridColumn {
                        DataField = "Surname",
                        HeaderText = "Фамилия",
                        DataType = typeof(string),
                        Editable = true,
                        Width = 100,
                    },

                    new JQGridColumn {
                        DataField = "Name",
                        HeaderText = "Имя",
                        DataType = typeof(string),
                        Editable = true,
                        Width = 100,
                    },

                    new JQGridColumn {
                        DataField = "MiddleName",
                        HeaderText = "Отчество",
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
                        DataField = "Description",
                        HeaderText = "Описание",
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
                        DataField = "LegalEntityId",
                        HeaderText = "Организация",
                        Editable = true,
                    },

                }
            };

            MyEmployeesGrid.ToolBarSettings.ShowRefreshButton = true;
            MyEmployeesGrid.AutoWidth = true;
            MyEmployeesGrid.Height = Unit.Pixel(300);
            MyEmployeesGrid.EditDialogSettings.Width = 450;
            MyEmployeesGrid.AddDialogSettings.Width = 450;

            #endregion
        }

        public JQGrid MyEmployeesGrid { get; set; }
    }
}