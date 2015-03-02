using SidBy.Sklad.DataAccess;
using SidBy.Sklad.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using SidBy.Sklad.Domain.Enums;


namespace SidBy.Sklad.Web.BL
{
    public class DocumentOperation
    {
        public int GetDocumentNumber(SkladDataContext context, int year, int documentTypeId)
        {
            int documentNumber = 1;

            // lock to all rows
            var transactionOptions = new TransactionOptions { IsolationLevel = IsolationLevel.RepeatableRead, Timeout = TimeSpan.MaxValue };
            using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                var item = context.DocumentCounters.Where(x => x.DocumentTypeId == documentTypeId &&
                    x.Year == year).FirstOrDefault();
                if (item == null)
                {
                    context.DocumentCounters.Add(new DocumentCounter {
                        Counter = documentNumber,
                        Year = year,
                        DocumentTypeId = documentTypeId
                    });
                }
                else
                {
                    item.Counter = item.Counter + 1;
                    documentNumber = item.Counter;
                }
                context.SaveChanges();
                scope.Complete();
            }

            return documentNumber;
        }

        public void UpdateDocument(SkladDataContext context, int documentId, bool isDeleting = false)
        {
         
        }

        public void DeleteDocument(SkladDataContext context, Document document)
        {
            // При удалении возврата - обновить поле Shipment в родителе 
            if (document.DocumentTypeId == (int)SidBy.Sklad.Domain.Enums.EntityEnum.DocumentTypeEnum.Refunds)
            {
                foreach (var product in document.Products)
                {
                    var productLineToUpdate = context.ProductLines.Where(x => x.ProductId == product.ProductId).FirstOrDefault();
                    if (productLineToUpdate != null) {
                        productLineToUpdate.Shipped = productLineToUpdate.Shipped - product.Quantity;
                        if (productLineToUpdate.Shipped < 0)
                            productLineToUpdate.Shipped = 0;
                    }
                }
                context.SaveChanges();
            }
        }

        public void UpdateDocumentProducts(IList<ProductLine> products, bool isDeleting = false)
        { 
        
        }

        public static string UrlToDocumentList(int documentId)
        {
           // string 
            switch (documentId)
            { 
                case (int)EntityEnum.DocumentTypeEnum.Posting :
                    return "/Warehouse/Posting";
                case (int)EntityEnum.DocumentTypeEnum.Cancellation:
                    return "/Warehouse/Cancellation";
                case (int)EntityEnum.DocumentTypeEnum.CustomerOrders:
                    return "/Sale/CustomerOrders";
                case (int)EntityEnum.DocumentTypeEnum.Shipment:
                    return "/Sale/Shipment";
                case (int)EntityEnum.DocumentTypeEnum.Refunds:
                    return "/Sale/Refunds";
                default:
                    return "/";
            }
        }
    }
}