using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Umbraco.Cms.Core.IO;
using Umbraco.Commerce.DemoStore.Import.Core.Helpers;
using Umbraco.Commerce.DemoStore.Import.Core.Models.ProviderModels;

namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers
{
    public class FileOrUrlModelHelper : IFileOrUrlModelHelper
    {
        private readonly IIOHelper _ioHelper;

        private readonly IHttpClientFactory _httpClientFactory;

        public FileOrUrlModelHelper(IIOHelper ioHelper, IHttpClientFactory httpClientFactory)
        {
            _ioHelper = ioHelper;
            _httpClientFactory = httpClientFactory;
        }

        public bool FileExist(FileOrUrlPickerModel fileUrlPicker)
        {
            if (fileUrlPicker.PickerType == "filePicker")
            {
                return File.Exists(GetFileOnDisk(fileUrlPicker));
            }
            if (fileUrlPicker.PickerType == "urlPicker")
            {
                return UrlExists(fileUrlPicker);
            }
            return false;
        }

        public string GetDataFromFileOrUrl(FileOrUrlPickerModel model)
        {
            if (!(model.PickerType == "filePicker"))
            {
                return GetDataFromUrl(model);
            }
            return GetDataFromDisk(model);
        }

        public string GetFileOnDisk(FileOrUrlPickerModel fileUrlPicker)
        {
            return _ioHelper.MapPath("/umbraco/Data/TEMP/cmsimport/datasourcefile/" + fileUrlPicker.UploadedFile.UniqueFileName);
        }

        public string GetExtension(FileOrUrlPickerModel fileUrlPicker)
        {
            if (!(fileUrlPicker.PickerType == "filePicker"))
            {
                return Path.GetExtension(fileUrlPicker.Url);
            }
            return Path.GetExtension(fileUrlPicker.UploadedFile.FileName);
        }

        public bool IsFilePicker(FileOrUrlPickerModel fileUrlPicker)
        {
            return fileUrlPicker.PickerType == "urlPicker";
        }

        public bool IsUrlPicker(FileOrUrlPickerModel fileUrlPicker)
        {
            return fileUrlPicker.PickerType == "urlPicker";
        }

        private string GetDataFromUrl(FileOrUrlPickerModel model)
        {
            string result = null;
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient();
                if (model.UrlRequiresAuthentication)
                {
                    NetworkCredential networkCredential = new NetworkCredential(model.AuthenticationUserName, model.AuthenticationUserPassword);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(networkCredential.UserName + ":" + networkCredential.Password)));
                }
                using HttpResponseMessage httpResponseMessage = httpClient.GetAsync(model.Url).Result;
                httpResponseMessage.EnsureSuccessStatusCode();
                using Stream stream = httpResponseMessage.Content.ReadAsStream();
                using StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
                result = streamReader.ReadToEnd();
            }
            catch
            {
            }
            return result;
        }

        private string GetDataFromDisk(FileOrUrlPickerModel model)
        {
            string text = null;
            using StreamReader streamReader = new StreamReader(GetFileOnDisk(model), Encoding.UTF8);
            return streamReader.ReadToEnd();
        }

        private bool UrlExists(FileOrUrlPickerModel model)
        {
            bool result = false;
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient();
                if (model.UrlRequiresAuthentication)
                {
                    NetworkCredential networkCredential = new NetworkCredential(model.AuthenticationUserName, model.AuthenticationUserPassword);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(networkCredential.UserName + ":" + networkCredential.Password)));
                }
                using HttpResponseMessage httpResponseMessage = httpClient.GetAsync(model.Url).Result;
                httpResponseMessage.EnsureSuccessStatusCode();
                result = httpResponseMessage.Content.Headers.ContentLength.GetValueOrDefault() > 0;
            }
            catch
            {
            }
            return result;
        }
    }
}
