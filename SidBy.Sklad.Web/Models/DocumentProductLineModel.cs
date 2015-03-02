using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SidBy.Sklad.Domain;
using Trirand.Web.Mvc;

namespace SidBy.Sklad.Web.Models
{
    public class DocumentProductLineModel
    {
        public Document DocumentItem { get; set; }
        public JQGrid Products { get; set; }
        public string JGridName { get; set; }
        public ICollection<Warehouse> WarehouseList { get; set; }
        public ICollection<UserProfile> EmployeeList { get; set; }
        public ICollection<LegalEntity> OurCompanyList { get; set; }
    }
}