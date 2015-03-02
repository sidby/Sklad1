using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Trirand.Web.Mvc;

namespace SidBy.Sklad.Web.Models
{
    public class LogJqGridModel
    {
        public LogJqGridModel()
        {
            LogGrid = new JQGrid
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn {
                        DataField = "Id",
                        PrimaryKey = true,
                        DataType = typeof(int),
                        Editable = false,
                        Visible = false,
                    },
                   
                    new JQGridColumn {
                        DataField = "Date",
                        HeaderText = "Дата",
                        DataType = typeof(DateTime),
                        Width = 140,
                        Searchable = true,
                        SearchToolBarOperation = SearchOperation.IsLessThan,
                        SearchType = SearchType.DatePicker,
                        SearchControlID = "LogCreatedPicker",
                        // dataEvents: [type: 'change', fn: function(e) { $("#elem")[0].triggerToolbar() }]
                        //DataFormatString = "{0:dd.MM.yyyy}"
                    },

                    new JQGridColumn {
                        DataField = "Message",
                        HeaderText = "Сообщение",
                        DataType = typeof(string),
                        Editable = true,
                        Width = 500,
                        Searchable = true,
                    },
                }
            };
            //LogGrid.ClientSideEvents.AfterAjaxRequest = "OrderByDateCreated";
            LogGrid.SortSettings = new SortSettings() { InitialSortColumn = "Date", 
                InitialSortDirection = Trirand.Web.Mvc.SortDirection.Desc };
            LogGrid.ToolBarSettings.ShowRefreshButton = true;
            LogGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.Top;
            //LogGrid.Width = 650;
            LogGrid.AutoWidth = false;
            LogGrid.Height = Unit.Pixel(200);
            LogGrid.ToolBarSettings.ShowSearchToolBar = true;
        }

        public JQGrid LogGrid { get; set; }

    }
}