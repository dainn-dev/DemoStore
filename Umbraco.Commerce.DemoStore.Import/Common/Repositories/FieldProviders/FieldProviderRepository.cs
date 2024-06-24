using Umbraco.Commerce.DemoStore.Import.Core.Providers.FieldProviders;

namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.FieldProviders
{
    public class FieldProviderRepository : IFieldProviderRepository, IRepository<IFieldProvider, string>
    {
        private readonly IFieldProviderCollection _fieldProviderCollection;

        public FieldProviderRepository(IFieldProviderCollection fieldProviderCollection)
        {
            _fieldProviderCollection = fieldProviderCollection;
        }

        public IFieldProvider Single(string key)
        {
            throw new NotImplementedException("Can't resolve a single provider, use getAll Or GetByPropertyEditorAlias");
        }

        public IEnumerable<IFieldProvider> GetAll()
        {
            return from p in _fieldProviderCollection.GetAll()
                   orderby p.Prio
                   select p.FieldProvider;
        }

        public IEnumerable<IFieldProvider> GetByPropertyEditorAlias(string propertyEditorAlias)
        {
            return from p in _fieldProviderCollection.GetAll()
                   where p.PropertyEditorAlias.Equals(propertyEditorAlias, StringComparison.InvariantCultureIgnoreCase)
                   orderby p.Prio
                   select p.FieldProvider;
        }
    }
}
