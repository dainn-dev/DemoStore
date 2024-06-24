using Newtonsoft.Json;

namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.ElementDefinition.ImportModel
{
    public class BlockListImportModel
    {
        [JsonProperty("layout")]
        public BlocklistLayoutModel BlocklistLayout { get; set; } = new BlocklistLayoutModel();


        [JsonProperty("contentData")]
        public List<Dictionary<string, object>> ContentData { get; set; } = new List<Dictionary<string, object>>();


        [JsonProperty("settingsData")]
        public List<Dictionary<string, string>> SettingsData { get; set; } = new List<Dictionary<string, string>>();

    }
}
