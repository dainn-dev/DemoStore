using Umbraco.Commerce.DemoStore.Import.Core.Providers.FieldProviders;

namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.FieldProviders
{
    public class FieldProviderItem
    {
        public string PropertyEditorAlias { get; set; }

        public int Prio { get; set; }

        public IFieldProvider FieldProvider { get; set; }
    }
}
