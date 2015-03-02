using System.Collections.Generic;

namespace SidBy.Sklad.Domain
{
    public class Warehouse
    {
        public Warehouse()
        {
            this.ChildWarehouses = new List<Warehouse>();
        }

        public int WarehouseId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public string Code { get; set; }
        public int? ParentId { get; set; }

        public virtual ICollection<Warehouse> ChildWarehouses { get; set; }
        public virtual Warehouse ParentWarehouse { get; set; }

        public virtual ICollection<Document> DocumentsFrom { get; set; }
        public virtual ICollection<Document> DocumentsTo { get; set; }
    }
}
