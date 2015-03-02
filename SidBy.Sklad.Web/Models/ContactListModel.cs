using SidBy.Sklad.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace SidBy.Sklad.Web.Models
{
    public class ContactListModel
    {
        public Contractor Company { get; set; }
        public JQGrid Grid { get; set; }
    }
}