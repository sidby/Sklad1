namespace SidBy.Sklad.Web.BL
{
    using SidBy.Sklad.DataAccess;
    using SidBy.Sklad.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Transactions;

    public class DocumentOperation
    {
        public int GetDocumentNumber(SkladDataContext context, int year, int documentTypeId)
        {
            int num = 1;
            TransactionOptions transactionOptions = new TransactionOptions {
                IsolationLevel = IsolationLevel.RepeatableRead,
                Timeout = TimeSpan.MaxValue
            };
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                ParameterExpression expression;
                DocumentCounter counter = context.get_DocumentCounters().Where<DocumentCounter>(Expression.Lambda<Func<DocumentCounter, bool>>(Expression.AndAlso(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(DocumentCounter), "x"), (MethodInfo) methodof(DocumentCounter.get_DocumentTypeId)), Expression.Constant(documentTypeId)), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(DocumentCounter.get_Year)), Expression.Constant(year))), new ParameterExpression[] { expression })).FirstOrDefault<DocumentCounter>();
                if (counter == null)
                {
                    DocumentCounter counter2 = new DocumentCounter();
                    counter2.set_Counter(num);
                    counter2.set_Year(year);
                    counter2.set_DocumentTypeId(documentTypeId);
                    context.get_DocumentCounters().Add(counter2);
                }
                else
                {
                    counter.set_Counter(counter.get_Counter() + 1);
                    num = counter.get_Counter();
                }
                context.SaveChanges();
                scope.Complete();
            }
            return num;
        }

        public void UpdateDocument(SkladDataContext context, int documentId, bool isDeleting = false)
        {
        }

        public void UpdateDocumentProducts(IList<ProductLine> products, bool isDeleting = false)
        {
        }

        public static string UrlToDocumentList(int documentId)
        {
            switch (documentId)
            {
                case 1:
                    return "/Warehouse/Posting";

                case 2:
                    return "/Warehouse/Cancellation";

                case 3:
                    return "/Sale/CustomerOrders";

                case 4:
                    return "/Sale/Shipment";

                case 5:
                    return "/Sale/Refunds";
            }
            return "/";
        }
    }
}

