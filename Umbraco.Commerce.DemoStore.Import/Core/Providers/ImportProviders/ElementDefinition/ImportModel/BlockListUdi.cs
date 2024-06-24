using Newtonsoft.Json;

namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.ElementDefinition.ImportModel
{
    public class BlockListUdi
    {
        [JsonProperty("contentUdi")]
        public string ContentUdi { get; set; }
    }
}
