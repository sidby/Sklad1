using SidBy.Sklad.DataAccess;
using SidBy.Sklad.Domain;
using SidBy.Sklad.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace SidBy.Sklad.Web.Migration
{
    /// <summary>
    /// Use for rebuild UserProfile table
    /// </summary>
    public class SkladDataContextMigrationConfiguration : DbMigrationsConfiguration<SkladDataContext>
    {
        public SkladDataContextMigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            // this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SkladDataContext context)
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            #if DEBUG
            bool saveChanges = false;

            #region LegalEntities
            if (context.LegalEntities.Where(x => x.Name == "Трикотажный ряд").FirstOrDefault() == null)
            {
                context.LegalEntities.Add(new LegalEntity() { Name = "Трикотажный ряд", IsVATPayer = true });
                saveChanges = true;
            }
            #endregion

            #region Warehouses
            if (context.Warehouses.Where(x => x.Name == "Основной склад").FirstOrDefault() == null)
            {
                context.Warehouses.Add(new Warehouse() { Name = "Основной склад" });
                saveChanges = true;
            }
            #endregion

            #region ContactTypes
            if (context.ContactTypes.Where(x => x.Name == "Сотрудник").FirstOrDefault() == null)
            {
                context.ContactTypes.Add(new ContactType() { Name = "Сотрудник" });

                if (context.ContactTypes.Where(x => x.Name == "Менеджер").FirstOrDefault() == null)
                {
                    context.ContactTypes.Add(new ContactType() { Name = "Менеджер" });
                }

                if (context.ContactTypes.Where(x => x.Name == "Контактное лицо").FirstOrDefault() == null)
                {
                    context.ContactTypes.Add(new ContactType() { Name = "Контактное лицо" });
                }

                if (context.ContactTypes.Where(x => x.Name == "Сотрудник с огр. правами").FirstOrDefault() == null)
                {
                    context.ContactTypes.Add(new ContactType() { Name = "Сотрудник с огр. правами" });
                }

                context.SaveChanges();
            }
            #endregion

            //#region Regions
            //if (context.Regions.Where(x => x.Name == "Минск").FirstOrDefault() == null)
            //{
            //    context.Regions.Add(new Region() { Name = "Минск" });
            //    saveChanges = true;
            //}
            //#endregion

            #region ContractorType
            if (context.ContractorTypes.Where(x => x.Name == "Клиент").FirstOrDefault() == null)
            {
                context.ContractorTypes.Add(new ContractorType() { Name = "Клиент" });
                saveChanges = true;
            }

            if (context.ContractorTypes.Where(x => x.Name == "Фабрика").FirstOrDefault() == null)
            {
                context.ContractorTypes.Add(new ContractorType() { Name = "Фабрика" });
                saveChanges = true;
            }
            #endregion

            #region DocumentType

            if (context.DocumentTypes.Where(x => x.Name == "Оприходование").FirstOrDefault() == null)
            {
                context.DocumentTypes.Add(new DocumentType() { Name = "Оприходование" });
                saveChanges = true;
            }

            if (context.DocumentTypes.Where(x => x.Name == "Списание").FirstOrDefault() == null)
            {
                context.DocumentTypes.Add(new DocumentType() { Name = "Списание" });
                saveChanges = true;
            }

            if (context.DocumentTypes.Where(x => x.Name == "Заказы покупателей").FirstOrDefault() == null)
            {
                context.DocumentTypes.Add(new DocumentType() { Name = "Заказы покупателей" });
                saveChanges = true;
            }

            if (context.DocumentTypes.Where(x => x.Name == "Отгрузки").FirstOrDefault() == null)
            {
                context.DocumentTypes.Add(new DocumentType() { Name = "Отгрузки" });
                saveChanges = true;
            }

            if (context.DocumentTypes.Where(x => x.Name == "Возвраты покупателей").FirstOrDefault() == null)
            {
                context.DocumentTypes.Add(new DocumentType() { Name = "Возвраты покупателей" });
                saveChanges = true;
            }
            #endregion

            if (saveChanges)
                context.SaveChanges();

            #endif
        }
    }
}