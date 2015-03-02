using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.Domain
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string Number { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreatedOf { get; set; }
        public DateTime? PlannedDate { get; set; }
        public decimal Sum { get; set; }
        public decimal SaleSum { get; set; }

        public int? FromWarehouseId { get; set; }
        public int? ToWarehouseId { get; set; }
        public int? ContractId { get; set; }
        public int? ContractorId { get; set; }
        public int? EmployeeId { get; set; }

        
        public int TotalQuantity { get; set; }

        /// <summary>
        /// Проведено
        /// </summary>
        public bool IsCommitted { get; set; }

        public string Comment { get; set; }

        public bool IsReportOutdated { get; set; }

        public string CommonFolderName { get; set; }
        public string SecureFolderName { get; set; }

        public string ContractorName
        {
            get { return Contractor == null ? "" : Contractor.Code; }
        }

        public string EmployeeName
        {
            get { return Employee == null ? "" : Employee.DisplayName; }
        }

        public string DocumentTypeName
        {
            get { return DocumentType == null ? "" : DocumentType.Name; }
        }

        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int DocumentTypeId { get; set; }
        public int? ParentDocumentId { get; set; }

        public virtual DocumentType DocumentType { get; set; }
        public virtual Document ParentDocument { get; set; }
        public virtual ICollection<Document> ChildDocuments { get; set; }
        public virtual Warehouse FromWarehouse { get; set; }
        public virtual Warehouse ToWarehouse { get; set; }
        public virtual Contract Contract { get; set; }
        public virtual Contractor Contractor { get; set; }
        public virtual UserProfile Employee { get; set; }
        public virtual ICollection<ProductLine> Products { get; set; }
        public List<ProductLine> ProductsFilteredList { get; set; }
    }
}
