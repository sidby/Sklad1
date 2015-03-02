namespace SidBy.Sklad.Web.Migration
{
    using SidBy.Sklad.DataAccess;
    using SidBy.Sklad.Domain;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using WebMatrix.WebData;

    public class SkladDataContextMigrationConfiguration : DbMigrationsConfiguration<SkladDataContext>
    {
        public SkladDataContextMigrationConfiguration()
        {
            base.set_AutomaticMigrationsEnabled(true);
        }

        protected override void Seed(SkladDataContext context)
        {
            ParameterExpression expression;
            if (!WebSecurity.get_Initialized())
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", true);
            }
            bool flag = false;
            if (context.get_LegalEntities().Where<LegalEntity>(Expression.Lambda<Func<LegalEntity, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(LegalEntity), "x"), (MethodInfo) methodof(LegalEntity.get_Name)), Expression.Constant("Трикотажный ряд", typeof(string)), false, (MethodInfo) methodof(string.op_Equality)), new ParameterExpression[] { expression })).FirstOrDefault<LegalEntity>() == null)
            {
                LegalEntity entity = new LegalEntity();
                entity.set_Name("Трикотажный ряд");
                entity.set_IsVATPayer(true);
                context.get_LegalEntities().Add(entity);
                flag = true;
            }
            if (context.get_Warehouses().Where<Warehouse>(Expression.Lambda<Func<Warehouse, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(Warehouse), "x"), (MethodInfo) methodof(Warehouse.get_Name)), Expression.Constant("Основной склад", typeof(string)), false, (MethodInfo) methodof(string.op_Equality)), new ParameterExpression[] { expression })).FirstOrDefault<Warehouse>() == null)
            {
                Warehouse warehouse = new Warehouse();
                warehouse.set_Name("Основной склад");
                context.get_Warehouses().Add(warehouse);
                flag = true;
            }
            if (context.get_ContactTypes().Where<ContactType>(Expression.Lambda<Func<ContactType, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(ContactType), "x"), (MethodInfo) methodof(ContactType.get_Name)), Expression.Constant("Сотрудник", typeof(string)), false, (MethodInfo) methodof(string.op_Equality)), new ParameterExpression[] { expression })).FirstOrDefault<ContactType>() == null)
            {
                ContactType type3 = new ContactType();
                type3.set_Name("Сотрудник");
                context.get_ContactTypes().Add(type3);
                if (context.get_ContactTypes().Where<ContactType>(Expression.Lambda<Func<ContactType, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(ContactType), "x"), (MethodInfo) methodof(ContactType.get_Name)), Expression.Constant("Менеджер", typeof(string)), false, (MethodInfo) methodof(string.op_Equality)), new ParameterExpression[] { expression })).FirstOrDefault<ContactType>() == null)
                {
                    ContactType type = new ContactType();
                    type.set_Name("Менеджер");
                    context.get_ContactTypes().Add(type);
                }
                if (context.get_ContactTypes().Where<ContactType>(Expression.Lambda<Func<ContactType, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(ContactType), "x"), (MethodInfo) methodof(ContactType.get_Name)), Expression.Constant("Контактное лицо", typeof(string)), false, (MethodInfo) methodof(string.op_Equality)), new ParameterExpression[] { expression })).FirstOrDefault<ContactType>() == null)
                {
                    ContactType type2 = new ContactType();
                    type2.set_Name("Контактное лицо");
                    context.get_ContactTypes().Add(type2);
                }
                context.SaveChanges();
            }
            if (context.get_ContractorTypes().Where<ContractorType>(Expression.Lambda<Func<ContractorType, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(ContractorType), "x"), (MethodInfo) methodof(ContractorType.get_Name)), Expression.Constant("Клиент", typeof(string)), false, (MethodInfo) methodof(string.op_Equality)), new ParameterExpression[] { expression })).FirstOrDefault<ContractorType>() == null)
            {
                ContractorType type4 = new ContractorType();
                type4.set_Name("Клиент");
                context.get_ContractorTypes().Add(type4);
                flag = true;
            }
            if (context.get_ContractorTypes().Where<ContractorType>(Expression.Lambda<Func<ContractorType, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(ContractorType), "x"), (MethodInfo) methodof(ContractorType.get_Name)), Expression.Constant("Фабрика", typeof(string)), false, (MethodInfo) methodof(string.op_Equality)), new ParameterExpression[] { expression })).FirstOrDefault<ContractorType>() == null)
            {
                ContractorType type5 = new ContractorType();
                type5.set_Name("Фабрика");
                context.get_ContractorTypes().Add(type5);
                flag = true;
            }
            if (context.get_DocumentTypes().Where<DocumentType>(Expression.Lambda<Func<DocumentType, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(DocumentType), "x"), (MethodInfo) methodof(DocumentType.get_Name)), Expression.Constant("Оприходование", typeof(string)), false, (MethodInfo) methodof(string.op_Equality)), new ParameterExpression[] { expression })).FirstOrDefault<DocumentType>() == null)
            {
                DocumentType type6 = new DocumentType();
                type6.set_Name("Оприходование");
                context.get_DocumentTypes().Add(type6);
                flag = true;
            }
            if (context.get_DocumentTypes().Where<DocumentType>(Expression.Lambda<Func<DocumentType, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(DocumentType), "x"), (MethodInfo) methodof(DocumentType.get_Name)), Expression.Constant("Списание", typeof(string)), false, (MethodInfo) methodof(string.op_Equality)), new ParameterExpression[] { expression })).FirstOrDefault<DocumentType>() == null)
            {
                DocumentType type7 = new DocumentType();
                type7.set_Name("Списание");
                context.get_DocumentTypes().Add(type7);
                flag = true;
            }
            if (context.get_DocumentTypes().Where<DocumentType>(Expression.Lambda<Func<DocumentType, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(DocumentType), "x"), (MethodInfo) methodof(DocumentType.get_Name)), Expression.Constant("Заказы покупателей", typeof(string)), false, (MethodInfo) methodof(string.op_Equality)), new ParameterExpression[] { expression })).FirstOrDefault<DocumentType>() == null)
            {
                DocumentType type8 = new DocumentType();
                type8.set_Name("Заказы покупателей");
                context.get_DocumentTypes().Add(type8);
                flag = true;
            }
            if (context.get_DocumentTypes().Where<DocumentType>(Expression.Lambda<Func<DocumentType, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(DocumentType), "x"), (MethodInfo) methodof(DocumentType.get_Name)), Expression.Constant("Отгрузки", typeof(string)), false, (MethodInfo) methodof(string.op_Equality)), new ParameterExpression[] { expression })).FirstOrDefault<DocumentType>() == null)
            {
                DocumentType type9 = new DocumentType();
                type9.set_Name("Отгрузки");
                context.get_DocumentTypes().Add(type9);
                flag = true;
            }
            if (context.get_DocumentTypes().Where<DocumentType>(Expression.Lambda<Func<DocumentType, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(DocumentType), "x"), (MethodInfo) methodof(DocumentType.get_Name)), Expression.Constant("Возвраты покупателей", typeof(string)), false, (MethodInfo) methodof(string.op_Equality)), new ParameterExpression[] { expression })).FirstOrDefault<DocumentType>() == null)
            {
                DocumentType type10 = new DocumentType();
                type10.set_Name("Возвраты покупателей");
                context.get_DocumentTypes().Add(type10);
                flag = true;
            }
            if (flag)
            {
                context.SaveChanges();
            }
        }
    }
}

