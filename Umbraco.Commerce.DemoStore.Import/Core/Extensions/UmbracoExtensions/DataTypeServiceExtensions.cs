using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Commerce.DemoStore.Import.Core.Extensions.UmbracoExtensions
{
    public static class DataTypeServiceExtensions
    {
        public static T GetConfiguration<T>(this IDataTypeService dataTypeService, Guid datatypeKey)
        {
            return ConfigurationEditor.ConfigurationAs<T>(dataTypeService.GetDataType(datatypeKey).Configuration);
        }

        public static string GetAlias(this IDataTypeService dataTypeService, Guid datatypeKey)
        {
            return dataTypeService.GetDataType(datatypeKey).EditorAlias ?? string.Empty;
        }
    }
}
