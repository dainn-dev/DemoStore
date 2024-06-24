using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Commerce.DemoStore.Import.Core.Models.ServiceModels;
using Umbraco.Commerce.DemoStore.Import.Core.Services;

namespace Umbraco.Commerce.DemoStore.Import.Common.Services
{
    public class NodeService : INodeService
    {
        private readonly IScopeProvider _scopeProvider;

        public NodeService(IScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;
        }

        public NodeInfo GetNodeByName(string nodeName)
        {
            NodeInfo nodeInfo = null;
            if (string.IsNullOrEmpty(nodeName))
            {
                return null;
            }
            using IScope scope = _scopeProvider.CreateScope();
            nodeInfo = scope.Database.Query<NodeInfo>("Select id as NodeId, [text] as NodeName,[uniqueId] as NodeGuid from UmbracoNode where [text] = @0 order by path", new object[1] { nodeName }).FirstOrDefault();
            scope.Complete();
            return nodeInfo;
        }

        public NodeInfo GetNodeByName(string nodeName, Guid parent)
        {
            int num = -1;
            NodeInfo nodeInfo = null;
            using IScope scope = _scopeProvider.CreateScope();
            num = scope.Database.ExecuteScalar<int>("SELECT Id from UmbracoNode where uniqueId = @0", new object[1] { parent });
            nodeInfo = scope.Database.Query<NodeInfo>("Select id as NodeId, [text] as NodeName,[uniqueId] as NodeGuid from UmbracoNode where [text] = @0 and path like @1 order by path", new object[2]
            {
            nodeName,
            $"%,{num},%"
            }).FirstOrDefault();
            scope.Complete();
            return nodeInfo;
        }
    }
}
