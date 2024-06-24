using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.ElementDefinition
{
    public class ElementDefinitionConfig
    {
        public Guid ContentElementTypeKey { get; set; }

        public string ContentElementAlias { get; set; }

        public string ContentElementName { get; set; }

        public string ContentElementIcon { get; set; }
    }
}
