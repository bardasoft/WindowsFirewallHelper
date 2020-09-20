using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using static Vanara.PInvoke.FirewallApi;
using WindowsFirewallHelper.InternalHelpers.Collections;

namespace WindowsFirewallHelper.Collections
{
    internal class FirewallLegacyServiceCollection :
        ComNativeCollectionBase<INetFwServices, INetFwService, NET_FW_SERVICE_TYPE>
    {
        public FirewallLegacyServiceCollection(INetFwServices servicesCollection) :
            base(servicesCollection)
        {
        }


        /// <inheritdoc />
        public override bool IsReadOnly { get; } = true;

        /// <inheritdoc />
        protected override NET_FW_SERVICE_TYPE GetCollectionKey(INetFwService managed)
        {
            return managed.Type;
        }

        /// <inheritdoc />
        protected override void InternalAdd(INetFwService native)
        {
            throw new InvalidOperationException();
        }

        /// <inheritdoc />
        protected override int InternalCount()
        {
            return NativeEnumerable.Count;
        }

        /// <inheritdoc />
        protected override INetFwService InternalItem(NET_FW_SERVICE_TYPE key)
        {
            try
            {
                return NativeEnumerable.Item(key);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        /// <inheritdoc />
        protected override void InternalRemove(NET_FW_SERVICE_TYPE key)
        {
            throw new InvalidOperationException();
        }
    }
}