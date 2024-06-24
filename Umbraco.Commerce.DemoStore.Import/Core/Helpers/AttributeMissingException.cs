namespace Umbraco.Commerce.DemoStore.Import.Core.Helpers
{
    public class AttributeMissingException : ApplicationException
    {
        public AttributeMissingException(string className)
            : base("The attribute is missing from class " + className)
        {
        }
    }

}
