// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Microsoft.Azure.Management.Network.Fluent
{
    using Management.Network.Fluent.Models;
    using System.Threading.Tasks;
    using Resource.Fluent.Core;
    using System.Threading;
    using Management.Network;

    /// <summary>
    /// Implementation for NetworkSecurityGroups.
    /// </summary>
    public partial class NetworkSecurityGroupsImpl  :
        GroupableResources<
            INetworkSecurityGroup,
            NetworkSecurityGroupImpl,
            NetworkSecurityGroupInner,
            INetworkSecurityGroupsOperations,
            NetworkManager>,
        INetworkSecurityGroups
    {
        internal  NetworkSecurityGroupsImpl (
            INetworkSecurityGroupsOperations innerCollection, 
            NetworkManager networkManager) : base(innerCollection, networkManager)
        {
        }

        internal PagedList<INetworkSecurityGroup> List ()
        {
            var pagedList = new PagedList<NetworkSecurityGroupInner>(InnerCollection.ListAll(), (string nextPageLink) =>
            {
                return InnerCollection.ListAllNext(nextPageLink);
            });

            return WrapList(pagedList);
        }

        internal PagedList<INetworkSecurityGroup> ListByGroup (string groupName)
        {
            var pagedList = new PagedList<NetworkSecurityGroupInner>(InnerCollection.List(groupName), (string nextPageLink) =>
            {
                return InnerCollection.ListNext(nextPageLink);
            });

            return WrapList(pagedList);
        }

        internal NetworkSecurityGroupImpl Define (string name)
        {
            return WrapModel(name);
        }

        public override Task DeleteByGroupAsync(string groupName, string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            return InnerCollection.DeleteAsync(groupName, name, cancellationToken);
        }

        public override async Task<INetworkSecurityGroup> GetByGroupAsync(string groupName, string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = await InnerCollection.GetAsync(groupName, name, null, cancellationToken);
            return WrapModel(data);
        }

        override protected NetworkSecurityGroupImpl WrapModel (string name)
        {
            NetworkSecurityGroupInner inner = new NetworkSecurityGroupInner();
            return new NetworkSecurityGroupImpl(name, inner, InnerCollection, Manager);
        }

        //$TODO: return NetworkSecurityGroupImpl
        override protected INetworkSecurityGroup WrapModel (NetworkSecurityGroupInner inner)
        {
            return new NetworkSecurityGroupImpl(inner.Name, inner, InnerCollection, Manager);
        }
    }
}