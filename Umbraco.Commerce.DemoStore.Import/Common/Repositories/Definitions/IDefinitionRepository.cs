using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.Definitions.Models;

namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.Definitions
{
    public interface IDefinitionRepository
    {
        IEnumerable<SimpleDefinitionModel> GetAll();

        IEnumerable<SimpleDefinitionModel> Children(string importProvider);

        IEnumerable<SimpleDefinitionModel> Children(Guid parentId, bool showRoot = false);

        void Save(StateDefinitionModel model);

        StateDefinitionModel Single(Guid id);

        StateDefinitionModel GetParent(Guid id);

        void Delete(Guid id);
    }

}
