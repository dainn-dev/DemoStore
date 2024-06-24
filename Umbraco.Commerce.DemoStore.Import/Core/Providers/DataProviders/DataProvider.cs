using System.Data;
using Umbraco.Commerce.DemoStore.Import.Core.Helpers;
using Umbraco.Commerce.DemoStore.Import.Core.Extensions;
using Umbraco.Commerce.DemoStore.Import.Core.Models.ProviderModels;
using Umbraco.Commerce.DemoStore.Import.Core.Models.Validation;

namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.DataProviders
{
    public abstract class DataProvider
    {
        public virtual string DefaultPrimaryKeyField => string.Empty;

        public virtual string DefaultForeignKeyField => string.Empty;

        public virtual string DefaultRecursiveForeignKeyField => string.Empty;

        public virtual bool SupportRecursiveImports => true;

        public string Alias { get; private set; }

        public string Name { get; private set; }

        public string Title { get; set; }

        public string Intro { get; set; }

        public List<string> AllowedContentTypes { get; protected set; }

        public virtual bool IsSupported => true;

        public int SortOrder { get; private set; }

        public virtual bool StoreImportStateGuidOnDataSourceKey => true;

        protected DataProvider()
        {
            DataProviderAttribute dataProviderAttribute = AttributeHelper.GetAttributes<DataProviderAttribute>(this, attributeRequired: true).FirstOrDefault();
            if (dataProviderAttribute != null)
            {
                Alias = dataProviderAttribute.Alias;
                Name = dataProviderAttribute.Name;
                Title = dataProviderAttribute.Title;
                Intro = dataProviderAttribute.Intro;
                SortOrder = dataProviderAttribute.SortOrder;
                AllowedContentTypes = new List<string>();
                AllowedContentTypes.AddRange(dataProviderAttribute.ContentType.ToList());
            }
        }

        public virtual IProviderOptions CopyParentOptions(IProviderOptions parentProvider)
        {
            return GetDataProviderOptions();
        }

        public abstract IDataReader GetData(IProviderOptions dataProviderOptions, bool onlyFirstRow = false);

        public IEnumerable<string> GetColumnNames(IProviderOptions dataProviderOptions)
        {
            List<string> list = new List<string>();
            list.Add(string.Empty);
            IDataReader data = GetData(dataProviderOptions, onlyFirstRow: true);
            for (int i = 0; i < data.FieldCount; i++)
            {
                string name = data.GetName(i);
                if (list.Contains(name))
                {
                    throw new DuplicateNameException(name);
                }
                list.Add(name);
            }
            data.Dispose();
            return list;
        }

        public virtual IEnumerable<ProviderConfirmOption> ParseConfirmOptions(IEnumerable<ProviderConfirmOption> confirmOptions)
        {
            return confirmOptions;
        }

        public virtual ValidationResult Validate(IProviderOptions providerOptions)
        {
            return new ValidationResult
            {
                IsValid = true
            };
        }

        public virtual bool CanGoNextStep(IProviderOptions providerOptions)
        {
            return true;
        }

        public abstract IProviderOptions GetDataProviderOptions();
    }
}
