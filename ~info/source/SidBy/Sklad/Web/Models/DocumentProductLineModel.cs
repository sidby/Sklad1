namespace SidBy.Sklad.Web.Models
{
    using SidBy.Sklad.Domain;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Trirand.Web.Mvc;

    public class DocumentProductLineModel
    {
        public Document DocumentItem { get; set; }

        public ICollection<UserProfile> EmployeeList { get; set; }

        public string JGridName { get; set; }

        public ICollection<LegalEntity> OurCompanyList { get; set; }

        public JQGrid Products { get; set; }

        public ICollection<Warehouse> WarehouseList { get; set; }
    }
}

