using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.ElementDefinition
{
    public class ElementDefinitionMapping
    {
        public string ContentTypeAlias { get; set; }

        public string ContentTypeName { get; set; }

        public string PropertyAlias { get; set; }

        public string PropertyName { get; set; }

        public Guid DataTypeKey { get; set; }

        public string View { get; set; }

        public string PropertyEditorAlias { get; set; }

        public List<ElementDefinition> Elements { get; set; } = new List<ElementDefinition>();

    }

}
