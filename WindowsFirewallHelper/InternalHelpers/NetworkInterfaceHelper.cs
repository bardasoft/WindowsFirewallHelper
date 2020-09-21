using System;
using System.Linq;
using System.Net.NetworkInformation;
using Vanara.Extensions;

namespace WindowsFirewallHelper.InternalHelpers
{
    // ReSharper disable once HollowTypeName
    internal static class NetworkInterfaceHelper
    {
        private const NetworkInterfaceTypes All = NetworkInterfaceTypes.Lan | NetworkInterfaceTypes.RemoteAccess | NetworkInterfaceTypes.Wireless;

        public static string[] InterfacesToString(NetworkInterface[] interfaces) =>
            interfaces.Length == 0 ? null : interfaces.Select(i => i.Name).ToArray();

        // ReSharper disable once FlagArgument
        public static string InterfaceTypesToString(NetworkInterfaceTypes types) =>
            (types & All) == All ? "All" : string.Join(",", types.GetFlags().Select(f => f.ToString()).ToArray());

        public static NetworkInterface[] StringToInterfaces(string[] str) =>
            str is null
                ? (new NetworkInterface[0])
                : NetworkInterface.GetAllNetworkInterfaces().Join(str, i => i.Name.Trim(), k => k.Trim(), (i, k) => i, StringComparer.OrdinalIgnoreCase).ToArray();

        // ReSharper disable once ExcessiveIndentation
        public static NetworkInterfaceTypes StringToInterfaceTypes(string str)
        {
            try
            {
                var tstr = str?.Trim();
                return string.IsNullOrEmpty(tstr) || tstr.Equals("All", StringComparison.OrdinalIgnoreCase)
                    ? All
                    : (NetworkInterfaceTypes)Enum.Parse(typeof(NetworkInterfaceTypes), str, true);
            }
            catch
            {
                return All;
            }
        }
    }
}