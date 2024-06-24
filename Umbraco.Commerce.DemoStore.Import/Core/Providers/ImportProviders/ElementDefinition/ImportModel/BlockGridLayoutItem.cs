using Newtonsoft.Json;

namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.ElementDefinition.ImportModel
{
    public class BlockGridLayoutItem
    {
        [JsonProperty("contentUdi")]
        public string ContentUdi { get; set; }

        [JsonProperty("areas")]
        public string[] Areas { get; set; } = Array.Empty<string>();


        [JsonProperty("columnSpan")]
        public int ColumnSpan { get; set; } = 12;


        [JsonProperty("rowSpan")]
        public int RowSpan { get; set; } = 1;

    }
}
