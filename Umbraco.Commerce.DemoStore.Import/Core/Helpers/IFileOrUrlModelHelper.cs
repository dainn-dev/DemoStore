using Umbraco.Commerce.DemoStore.Import.Core.Models.ProviderModels;

namespace Umbraco.Commerce.DemoStore.Import.Core.Helpers
{
    public interface IFileOrUrlModelHelper
    {
        string GetDataFromFileOrUrl(FileOrUrlPickerModel model);

        string GetFileOnDisk(FileOrUrlPickerModel fileUrlPicker);

        bool FileExist(FileOrUrlPickerModel fileUrlPicker);

        string GetExtension(FileOrUrlPickerModel fileUrlPicker);

        bool IsUrlPicker(FileOrUrlPickerModel fileUrlPicker);

        bool IsFilePicker(FileOrUrlPickerModel fileUrlPicker);
    }
}
