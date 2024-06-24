using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.ElementDefinition.ImportModel
{
    public class BlockGridImportModel
    {
        [JsonProperty("layout")]
        public BlockGridLayoutModel BlockGridLayout { get; set; } = new BlockGridLayoutModel();


        [JsonProperty("contentData")]
        public List<Dictionary<string, object>> ContentData { get; set; } = new List<Dictionary<string, object>>();


        [JsonProperty("settingsData")]
        public List<Dictionary<string, string>> SettingsData { get; set; } = new List<Dictionary<string, string>>();

    }
}
