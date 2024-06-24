using Umbraco.Commerce.DemoStore.Import.Core.Composers.CollectionBuilders;

namespace Umbraco.Commerce.DemoStore.Import.Common.Extensions
{
    public static class CMSImportImportCompositionExtensions
    {
        public static CMSImportImportProviderCollectionBuilder CMSImportImportProviders(this IUmbracoBuilder builder)
        {
            return builder.WithCollectionBuilder<CMSImportImportProviderCollectionBuilder>();
        }

        public static CMSImportDataProviderCollectionBuilder CMSImportDataProviders(this IUmbracoBuilder builder)
        {
            return builder.WithCollectionBuilder<CMSImportDataProviderCollectionBuilder>();
        }

        public static CMSImportFieldProviderCollectionBuilder CMSImportFieldProviders(this IUmbracoBuilder builder)
        {
            return builder.WithCollectionBuilder<CMSImportFieldProviderCollectionBuilder>();
        }

        public static CMSImportUIValueConverterCollectionBuilder CMSImportUIValueConverters(this IUmbracoBuilder builder)
        {
            return builder.WithCollectionBuilder<CMSImportUIValueConverterCollectionBuilder>();
        }

        public static CMSImportAdvancedSettingsProviderCollectionBuilder CMSImportAdvancedSettingsProviders(this IUmbracoBuilder builder)
        {
            return builder.WithCollectionBuilder<CMSImportAdvancedSettingsProviderCollectionBuilder>();
        }

        public static void Add(this CMSImportImportProviderCollectionBuilder collection, IEnumerable<Type> types)
        {
            foreach (Type type in types)
            {
                collection.Add(type);
            }
        }

        public static void Add(this CMSImportDataProviderCollectionBuilder collection, IEnumerable<Type> types)
        {
            foreach (Type type in types)
            {
                collection.Add(type);
            }
        }

        public static void Add(this CMSImportFieldProviderCollectionBuilder collection, IEnumerable<Type> types)
        {
            foreach (Type type in types)
            {
                collection.Add(type);
            }
        }

        public static void Add(this CMSImportAdvancedSettingsProviderCollectionBuilder collection, IEnumerable<Type> types)
        {
            foreach (Type type in types)
            {
                collection.Add(type);
            }
        }

        public static void Add(this CMSImportUIValueConverterCollectionBuilder collection, IEnumerable<Type> types)
        {
            foreach (Type type in types)
            {
                collection.Add(type);
            }
        }
    }
}
