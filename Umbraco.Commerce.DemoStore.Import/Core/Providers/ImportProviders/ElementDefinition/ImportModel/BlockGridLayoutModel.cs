using Newtonsoft.Json;
using Umbraco.Cms.Core.Models.Blocks;

namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.ElementDefinition.ImportModel
{
    public class BlockGridLayoutModel
    {
        [JsonProperty("Umbraco.BlockList")]
        public List<BlockListUdi> BlockListUdis { get; set; } = new List<BlockListUdi>();


        [JsonProperty("Umbraco.BlockGrid")]
        public List<BlockGridLayoutItem> BlockGridItems { get; set; } = new List<BlockGridLayoutItem>();

    }
}
