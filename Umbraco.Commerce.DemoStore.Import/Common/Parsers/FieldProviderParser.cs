using Umbraco.Cms.Core.Models;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.FieldProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Parsers;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.FieldProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;

namespace Umbraco.Commerce.DemoStore.Import.Common.Parsers
{
    public class FieldProviderParser : IFieldProviderParser
    {
        private readonly IFieldProviderRepository _fieldProviderRepository;

        public FieldProviderParser(IFieldProviderRepository fieldProviderRepository)
        {
            _fieldProviderRepository = fieldProviderRepository;
        }

        public object Parse(object value, IContentBase importedItem, ImportPropertyInfo property, FieldProviderOptions fieldProviderOptions)
        {
            foreach (IFieldProvider item in _fieldProviderRepository.GetByPropertyEditorAlias(property.PropertyEditorAlias))
            {
                value = item.Parse(value, importedItem, property, fieldProviderOptions);
                if (fieldProviderOptions.Break)
                {
                    break;
                }
            }
            return value;
        }
    }
}
