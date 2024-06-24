namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.Definitions.Models
{
    public class SimpleDefinitionModel
    {
        public Guid Id { get; set; }

        public Guid ParentId { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public string ImportProvider { get; set; }
    }
}
