namespace SidBy.Sklad.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.UI.WebControls;
    using Trirand.Web.Mvc;

    public class MyEmployeesJqGridModel
    {
        public MyEmployeesJqGridModel()
        {
            JQGrid grid = new JQGrid();
            List<JQGridColumn> list = new List<JQGridColumn>();
            JQGridColumn item = new JQGridColumn {
                DataField = "UserId",
                PrimaryKey = true,
                DataType = typeof(int),
                Editable = false,
                Visible = false
            };
            list.Add(item);
            JQGridColumn column2 = new JQGridColumn {
                DataField = "UserName",
                HeaderText = "Логин",
                DataType = typeof(string),
                Editable = true,
                Width = 100
            };
            list.Add(column2);
            JQGridColumn column3 = new JQGridColumn {
                DataField = "UserEmail",
                HeaderText = "Email",
                Formatter = new EmailFormatter(),
                DataType = typeof(string),
                Editable = true,
                Width = 60
            };
            list.Add(column3);
            JQGridColumn column4 = new JQGridColumn {
                DataField = "Surname",
                HeaderText = "Фамилия",
                DataType = typeof(string),
                Editable = true,
                Width = 100
            };
            list.Add(column4);
            JQGridColumn column5 = new JQGridColumn {
                DataField = "Name",
                HeaderText = "Имя",
                DataType = typeof(string),
                Editable = true,
                Width = 100
            };
            list.Add(column5);
            JQGridColumn column6 = new JQGridColumn {
                DataField = "MiddleName",
                HeaderText = "Отчество",
                DataType = typeof(string),
                Editable = true,
                Width = 100
            };
            list.Add(column6);
            JQGridColumn column7 = new JQGridColumn {
                DataField = "Phone",
                HeaderText = "Телефон",
                Width = 60,
                DataType = typeof(string),
                Editable = true
            };
            list.Add(column7);
            JQGridColumn column8 = new JQGridColumn {
                DataField = "Description",
                HeaderText = "Описание",
                DataType = typeof(string),
                Editable = true,
                EditType = EditType.TextArea,
                Visible = false
            };
            List<JQGridEditFieldAttribute> list2 = new List<JQGridEditFieldAttribute>();
            JQGridEditFieldAttribute attribute = new JQGridEditFieldAttribute {
                Name = "cols",
                Value = "50"
            };
            list2.Add(attribute);
            column8.EditFieldAttributes = list2;
            list.Add(column8);
            JQGridColumn column9 = new JQGridColumn {
                DataField = "LegalEntityId",
                HeaderText = "Организация",
                Editable = true
            };
            list.Add(column9);
            grid.Columns = list;
            this.MyEmployeesGrid = grid;
            this.MyEmployeesGrid.ToolBarSettings.ShowRefreshButton = true;
            this.MyEmployeesGrid.AutoWidth = true;
            this.MyEmployeesGrid.Height = Unit.Pixel(300);
            this.MyEmployeesGrid.EditDialogSettings.Width = 450;
            this.MyEmployeesGrid.AddDialogSettings.Width = 450;
        }

        public JQGrid MyEmployeesGrid { get; set; }
    }
}

