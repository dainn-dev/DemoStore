using Umbraco.Cms.Core.Models;
using Umbraco.Commerce.DemoStore.Import.Core.Import;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.DataProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;
using Umbraco.Commerce.DemoStore.Import.Core.State;

namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers.Definition
{
    public interface IDocumentImportMappingHelper
    {
        object ParsePropertyValue(ImportState state, DataProvider dataProvider, IContent doc, IMediaImport mediaImport, object rawValue, ImportPropertyInfo prop);

        void MapDynamicPropertyValue(IContent doc, ImportMapping mapping, ImportPropertyInfo prop, object propValue, IProperty contentProp);
    }
}
