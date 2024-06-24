using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.ElementDefinition.ImportModel;

namespace Umbraco.Commerce.DemoStore.Import.Common.Extensions.ImportExtensions;

public static class ElementImportModelExtensions
{
    public static void AddElement(this BlockListImportModel model, Guid contentTypeKey, string udi, Dictionary<string, object> mapping)
    {
        model.BlocklistLayout.BlockListUdis.Add(new BlockListUdi
        {
            ContentUdi = udi.ToString()
        });
        model.ContentData.Add(ConvertMapping(contentTypeKey, udi, mapping));
    }

    public static void AddElement(this BlockGridImportModel model, Guid contentTypeKey, string udi, Dictionary<string, object> mapping)
    {
        model.BlockGridLayout.BlockListUdis.Add(new BlockListUdi
        {
            ContentUdi = udi.ToString()
        });
        model.BlockGridLayout.BlockGridItems.Add(new BlockGridLayoutItem
        {
            ContentUdi = udi.ToString()
        });
        model.ContentData.Add(ConvertMapping(contentTypeKey, udi, mapping));
    }

    private static Dictionary<string, object> ConvertMapping(Guid contentTypeKey, string udi, Dictionary<string, object> mapping)
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object>
    {
        { "contentTypeKey", contentTypeKey },
        { "udi", udi }
    };
        foreach (KeyValuePair<string, object> item in mapping)
        {
            dictionary.Add(item.Key, item.Value);
        }
        return dictionary;
    }
}
