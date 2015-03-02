namespace SidBy.Sklad.Web.Models
{
    using System;
    using System.Runtime.CompilerServices;
    using Trirand.Web.Mvc;

    public class JqGridViewDocParamsModel
    {
        public int DocumentTypeId { get; set; }

        public string EditCreateUrl { get; set; }

        public JQGrid Grid { get; set; }

        public string GridId { get; set; }

        public bool ShowContractor { get; set; }

        public string Title { get; set; }
    }
}

