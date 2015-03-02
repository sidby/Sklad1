using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace SidBy.Sklad.Web.Models
{
    public class JqGridViewParamsModel
    {
        public string Title { get; set; }
        public string GridId { get; set; }
        public JQGrid Grid { get; set; }
    }
}