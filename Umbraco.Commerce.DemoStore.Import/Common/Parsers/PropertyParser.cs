using Umbraco.Cms.Core.Models;
using Umbraco.Commerce.DemoStore.Import.Core.Parsers;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.FieldProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;

namespace Umbraco.Commerce.DemoStore.Import.Common.Parsers
{
    public class PropertyParser : IPropertyParser
    {
        private IFieldProviderParser _fieldProviderParser;

        private IAdvancedPropertyParser _advancedPropertyParser;

        public PropertyParser(IFieldProviderParser fieldProviderParser, IAdvancedPropertyParser advancedPropertyParser)
        {
            _fieldProviderParser = fieldProviderParser;
            _advancedPropertyParser = advancedPropertyParser;
        }

        public object Parse(object value, IContentBase importedItem, ImportPropertyInfo property, FieldProviderOptions fieldProviderOptions)
        {
            object obj = _fieldProviderParser.Parse(value, importedItem, property, fieldProviderOptions);
            if (!fieldProviderOptions.Break)
            {
                return _advancedPropertyParser.Parse(obj, importedItem, property, new ImportOptions
                {
                    UserId = fieldProviderOptions.UserId,
                    MediaImport = fieldProviderOptions.MediaImport,
                    DataProviderOptions = fieldProviderOptions.DataProviderOptions
                });
            }
            return obj;
        }
    }
}
