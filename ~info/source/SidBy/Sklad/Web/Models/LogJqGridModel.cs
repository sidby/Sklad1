namespace SidBy.Sklad.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.UI.WebControls;
    using Trirand.Web.Mvc;

    public class LogJqGridModel
    {
        public LogJqGridModel()
        {
            JQGrid grid = new JQGrid();
            List<JQGridColumn> list = new List<JQGridColumn>();
            JQGridColumn item = new JQGridColumn {
                DataField = "Id",
                PrimaryKey = true,
                DataType = typeof(int),
                Editable = false,
                Visible = false
            };
            list.Add(item);
            JQGridColumn column2 = new JQGridColumn {
                DataField = "Date",
                HeaderText = "Дата",
                DataType = typeof(DateTime),
                Width = 140,
                Searchable = true,
                SearchToolBarOperation = SearchOperation.IsLessThan,
                SearchType = SearchType.DatePicker,
                SearchControlID = "LogCreatedPicker"
            };
            list.Add(column2);
            JQGridColumn column3 = new JQGridColumn {
                DataField = "Message",
                HeaderText = "Сообщение",
                DataType = typeof(string),
                Editable = true,
                Width = 500,
                Searchable = true
            };
            list.Add(column3);
            grid.Columns = list;
            this.LogGrid = grid;
            SortSettings settings = new SortSettings {
                InitialSortColumn = "Date",
                InitialSortDirection = SortDirection.Desc
            };
            this.LogGrid.SortSettings = settings;
            this.LogGrid.ToolBarSettings.ShowRefreshButton = true;
            this.LogGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.Top;
            this.LogGrid.AutoWidth = false;
            this.LogGrid.Height = Unit.Pixel(200);
            this.LogGrid.ToolBarSettings.ShowSearchToolBar = true;
        }

        public JQGrid LogGrid { get; set; }
    }
}

