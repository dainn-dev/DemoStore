using Umbraco.Commerce.DemoStore.Import.Core.Models.ServiceModels;

namespace Umbraco.Commerce.DemoStore.Import.Core.Services
{
    public interface INodeService
    {
        NodeInfo GetNodeByName(string nodeName);

        NodeInfo GetNodeByName(string nodeName, Guid parent);
    }

}
