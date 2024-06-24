using Newtonsoft.Json;

namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.Definition
{
    public class CultureDefinition
    {
        [JsonProperty("isoCode")]
        public string IsoCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isDefault")]
        public bool IsDefault { get; set; }
    }
}
