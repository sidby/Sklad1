using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace SidBy.Sklad.Web.Models
{
    public class JqGridViewDocParamsModel
    {
        public string Title { get; set; }
        public string GridId { get; set; }
        public JQGrid Grid { get; set; }
        public string EditCreateUrl { get; set; }
        public int DocumentTypeId { get; set; }
        /// <summary>
        /// Create autocomplete control for contractor
        /// </summary>
        public bool ShowContractor { get; set; }
    }
}