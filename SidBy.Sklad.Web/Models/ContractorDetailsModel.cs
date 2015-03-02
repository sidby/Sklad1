using SidBy.Sklad.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace SidBy.Sklad.Web.Models
{
    public class ContractorDetailsModel
    {
        public Contractor Company { get; set; }
        public JQGrid DocumentGrid { get; set; }
        public JQGrid ProductLineGrid { get; set; }
        
    }
}