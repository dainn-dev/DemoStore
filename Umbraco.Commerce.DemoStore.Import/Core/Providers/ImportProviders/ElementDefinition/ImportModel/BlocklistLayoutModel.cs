using Newtonsoft.Json;

namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.ElementDefinition.ImportModel
{
    public class BlocklistLayoutModel
    {
        [JsonProperty("Umbraco.BlockList")]
        public List<BlockListUdi> BlockListUdis { get; set; } = new List<BlockListUdi>();

    }
}
