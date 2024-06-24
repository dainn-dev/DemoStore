namespace Umbraco.Commerce.DemoStore.Import.Core.Helpers
{
    public static class AttributeHelper
    {
        public static List<T> GetAttributes<T>(object o) where T : Attribute
        {
            return GetAttributes<T>(o, attributeRequired: false);
        }

        public static List<T> GetAttributes<T>(object o, bool attributeRequired) where T : Attribute
        {
            object[] customAttributes = o.GetType().GetCustomAttributes(typeof(T), inherit: true);
            if (attributeRequired && !customAttributes.Any())
            {
                throw new AttributeMissingException(o.GetType().Name);
            }
            return (from item in customAttributes
                    select item as T into customAttribute
                    where customAttribute != null
                    select customAttribute).ToList();
        }
    }
}
