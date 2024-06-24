using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Commerce.DemoStore.Import.Core.Models.Resolver
{
    public class ResolverResult
    {
        public object Value { get; set; }

        public ResolverStatus Status { get; set; }
    }
}
