using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.Domain.Enums
{
    public static class EntityEnum
    {
        public enum ContractorTypeEnum : int
        { 
            Client = 1,
            Factory = 2
        }

        public enum ReportTypeEnum : int
        { 
            ByDay = 1,
            ByMonth
        }

        public enum DocumentTypeEnum : int
        { 
            /// <summary>
            /// Оприходование
            /// </summary>
            Posting = 1,

            /// <summary>
            /// Списание
            /// </summary>
            Cancellation = 2,

            /// <summary>
            /// Заказы покупателей
            /// </summary>
            CustomerOrders = 3,

            /// <summary>
            /// Отгрузки
            /// </summary>
            Shipment = 4,

            /// <summary>
            /// Возвраты покупателей
            /// </summary>
            Refunds = 5
        }

        public enum ReportType : int
        { 
            SaleReport = 1,

            SaleReportToCustomer = 2
        }

        public enum ContactTypeEnum : int
        {
            /// <summary>
            /// Сотрудник
            /// </summary>
            Employee = 1,
            /// <summary>
            /// Менеджер
            /// </summary>
            Manager = 2,
            /// <summary>
            /// Сотрудник с ограниченными правами
            /// </summary>
            LimitedEmployee = 4,
            /// <summary>
            /// Контактное лицо
            /// </summary>
            Contact = 3
        }
    }
}
