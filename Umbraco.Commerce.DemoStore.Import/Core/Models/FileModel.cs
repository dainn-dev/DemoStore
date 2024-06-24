using Newtonsoft.Json;

namespace Umbraco.Commerce.DemoStore.Import.Core.Models
{
    public class FileModel
    {
        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("uniqueFileName")]
        public string UniqueFileName { get; set; }

        [JsonProperty("tempFolder")]
        public string TempFolder { get; set; }

        [JsonProperty("fileChanged")]
        public bool FileChanged { get; set; }
    }
}
