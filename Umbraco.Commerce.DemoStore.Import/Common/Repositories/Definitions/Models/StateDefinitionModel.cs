using Umbraco.Commerce.DemoStore.Import.Core.State;

namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.Definitions.Models
{
    public class StateDefinitionModel
    {
        public Guid Id { get; set; }

        public Guid ParentId { get; set; }

        public bool AllowChildDefinition { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public ImportState ImportState { get; set; }

        public string ImportProvider { get; set; }
    }

}
