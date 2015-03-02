namespace SidBy.Sklad.Web.Models
{
    using SidBy.Sklad.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using Trirand.Web.Mvc;

    public class ContactListModel
    {
        public Contractor Company { get; set; }

        public JQGrid Grid { get; set; }
    }
}

