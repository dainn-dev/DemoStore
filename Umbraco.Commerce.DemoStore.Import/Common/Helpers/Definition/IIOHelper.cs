namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers.Definition
{
    public interface IIOHelper
    {
        string PluginUrl { get; }

        string UmbracoFolder { get; }

        string GetApplicationUrl();

        string FormatPluginUrl(string url);

        string FormatUmbracoUrl(string url);

        string FormatUrl(string url);

        string GetExtension(string url);

        string MapPathPluginUrl(string url);

        bool ParseRequest(string url);

        string MapPath(string path);

        string MapPathWebRoot(string path);
    }
}
