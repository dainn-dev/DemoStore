namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.DataProviders
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DataProviderAttribute : Attribute
    {
        public string Alias { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Intro { get; set; }

        public int SortOrder { get; set; }

        public string ContentType { get; set; }
    }
}
