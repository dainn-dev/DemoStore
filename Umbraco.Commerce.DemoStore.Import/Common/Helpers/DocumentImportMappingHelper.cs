using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Models;
using Umbraco.Commerce.DemoStore.Import.Common.Helpers.Definition;
using Umbraco.Commerce.DemoStore.Import.Common.Import;
using Umbraco.Commerce.DemoStore.Import.Common.Extensions;
using Umbraco.Commerce.DemoStore.Import.Core.Import;
using Umbraco.Commerce.DemoStore.Import.Core.Parsers;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.DataProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.FieldProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;
using Umbraco.Commerce.DemoStore.Import.Core.State;

namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers
{
    public class DocumentImportMappingHelper : IDocumentImportMappingHelper
    {
        private readonly MediaFileManager _mediaFileManager;

        private readonly IPropertyParser _propertyParser;

        public DocumentImportMappingHelper(MediaFileManager mediaFileManager, IPropertyParser propertyParser)
        {
            _mediaFileManager = mediaFileManager;
            _propertyParser = propertyParser;
        }

        public void MapDynamicPropertyValue(IContent doc, ImportMapping mapping, ImportPropertyInfo prop, object propValue, IProperty contentProp)
        {
            if (propValue is MediaFile mediaFile)
            {
                doc.SetFile(_mediaFileManager, mediaFile.FilePath, prop.PropertyAlias, prop.CanVaryByCulture ? mapping.Culture : null);
            }
            else
            {
                contentProp.SetValue(propValue, prop.CanVaryByCulture ? mapping.Culture : null);
            }
        }

        public object ParsePropertyValue(ImportState state, DataProvider dataProvider, IContent doc, IMediaImport mediaImport, object rawValue, ImportPropertyInfo prop)
        {
            return _propertyParser.Parse(dataProvider.ResolveLinks(state.SelectedDataProviderOptions, prop, state, rawValue), doc, prop, new FieldProviderOptions
            {
                ImportState = state,
                UserId = state.ImportAsUserId,
                MediaImport = mediaImport,
                DataProviderOptions = state.SelectedDataProviderOptions
            });
        }
    }
}
