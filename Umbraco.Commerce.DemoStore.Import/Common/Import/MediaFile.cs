namespace Umbraco.Commerce.DemoStore.Import.Common.Import;
public class MediaFile
{
    public string FilePath { get; set; }

    public string OriginalUrl { get; set; }

    public string FileName
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(FilePath))
            {
                return Path.GetFileName(FilePath);
            }
            return string.Empty;
        }
    }
}
