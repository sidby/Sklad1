using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SidBy.Sklad.Domain;

namespace SidBy.Sklad.Web.Models
{
    public class IndicatorsModel
    {
        public LogJqGridModel LogGrid { get; set; }

        public IQueryable<UserProfile> Employees { get; set; }
        public IQueryable<UserProfile> Managers { get; set; }
    }
}