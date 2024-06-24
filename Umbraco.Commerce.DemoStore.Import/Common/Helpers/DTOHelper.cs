using NPoco;

namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers
{
    public static class DTOHelper
    {
        public static string GetTableName(Type dtoType)
        {
            TableNameAttribute tableNameAttribute = TypeExtensions.FirstAttribute<TableNameAttribute>(dtoType);
            if (tableNameAttribute != null)
            {
                return tableNameAttribute.Value;
            }
            return dtoType.Name;
        }
    }
}
