using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Commerce.DemoStore.Import.Core.Models.Import
{
    public class MediaRelationInfo
    {
        public Guid UmbracoId { get; set; }

        public string SourceUrl { get; set; }

        public long ByteSize { get; set; }
    }
}
