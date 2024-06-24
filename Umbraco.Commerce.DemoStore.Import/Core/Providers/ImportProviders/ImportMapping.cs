using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders
{
    public class ImportMapping
    {
        public string Culture { get; set; }

        public bool IsDefaultCulture { get; set; }

        public IEnumerable<ImportPropertyInfo> PropertyInfo { get; set; }

        public ImportMapping()
        {
            PropertyInfo = new List<ImportPropertyInfo>();
        }
    }
}
