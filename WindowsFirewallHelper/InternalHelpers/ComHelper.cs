using System;

namespace WindowsFirewallHelper.InternalHelpers
{
    // ReSharper disable once HollowTypeName
    internal static class ComHelper
    {
        public static bool IsSupported<T>()
        {
            if (!typeof(T).IsInterface)
            {
                throw new ArgumentException("Invalid generic type passed.", nameof(T));
            }

            return Type.GetTypeFromCLSID(typeof(T).GUID, false) != null;
        }
    }
}