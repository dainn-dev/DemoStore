using Umbraco.Commerce.DemoStore.Import.Common.Repositories.MediaDefinition.Models;

namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.MediaDefinition
{
    public interface IMediaDefinitionRepository
    {
        List<MediaDefinitionModel> GetAll();

        MediaDefinitionModel Single(string alias);
    }
}
