using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers;

namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.ImportProviders
{
    public interface IImportProviderRepository
    {
        IEnumerable<ImportProvider> GetAll();

        IProviderOptions GetProviderOptions(string alias);

        ImportProvider Single(string alias);
    }
}
