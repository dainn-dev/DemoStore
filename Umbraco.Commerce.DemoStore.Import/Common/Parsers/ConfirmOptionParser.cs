using Umbraco.Commerce.DemoStore.Import.Core.Composers.CollectionBuilders;
using Umbraco.Commerce.DemoStore.Import.Core.Extensions;
using Umbraco.Commerce.DemoStore.Import.Core.Parsers;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ConfirmOptionProviders;

namespace Umbraco.Commerce.DemoStore.Import.Common.Parsers
{
    public class ConfirmOptionParser : IConfirmOptionParser
    {
        private readonly ConfirmOptionProviderCollection _confirmOptionParsers;

        public ConfirmOptionParser(ConfirmOptionProviderCollection confirmOptionParsers)
        {
            _confirmOptionParsers = confirmOptionParsers;
        }

        public string ParseConfirmOption(string propertyEditorAlias, object value)
        {
            IConfirmOptionProvider confirmOptionProvider = _confirmOptionParsers.FirstOrDefault((IConfirmOptionProvider p) => p.PropertyEditorAlias.Equals(propertyEditorAlias, StringComparison.InvariantCultureIgnoreCase));
            if (confirmOptionProvider == null)
            {
                return value.AsString();
            }
            return confirmOptionProvider.ParseConfirmOption(value).AsString();
        }
    }
}
