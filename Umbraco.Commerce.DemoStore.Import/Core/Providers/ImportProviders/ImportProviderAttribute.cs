namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ImportProviderAttribute : Attribute
    {
        public string Alias { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public int SortOrder { get; set; }

        public string ContentType { get; set; }

        public bool ChildImportOnly { get; set; }

        public string SupportedChild { get; set; }

        public string Title { get; set; }

        public string Intro { get; set; }
    }

}
