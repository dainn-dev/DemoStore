using Umbraco.Commerce.DemoStore.Import.Core.Providers.FieldProviders;

namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.FieldProviders
{
    public interface IFieldProviderRepository : IRepository<IFieldProvider, string>
    {
        IEnumerable<IFieldProvider> GetByPropertyEditorAlias(string propertyEditorAlias);
    }
}
