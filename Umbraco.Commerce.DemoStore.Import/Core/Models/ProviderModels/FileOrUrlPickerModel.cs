using Newtonsoft.Json;

namespace Umbraco.Commerce.DemoStore.Import.Core.Models.ProviderModels
{
    public class FileOrUrlPickerModel
    {
        public const string FilePicker = "filePicker";

        public const string UrlPicker = "urlPicker";

        [JsonProperty("pickerType")]
        public string PickerType { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("uploadedFile")]
        public FileModel UploadedFile { get; set; }

        [JsonProperty("urlRequiresAuthentication")]
        public bool UrlRequiresAuthentication { get; set; }

        [JsonProperty("authenticationUserName")]
        public string AuthenticationUserName { get; set; }

        [JsonProperty("authenticationUserPassword")]
        public string AuthenticationUserPassword { get; set; }

        public FileOrUrlPickerModel()
        {
            UploadedFile = new FileModel();
            PickerType = "filePicker";
        }
    }
}
