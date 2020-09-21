using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using WindowsFirewallHelper.Addresses;

namespace WindowsFirewallHelper.InternalHelpers
{
    // ReSharper disable once HollowTypeName
    internal static class AddressHelper
    {
        public static string AddressesToString(IAddress[] rules)
        {
            var addresses = rules.Select(r => r.ToString()).ToArray();

            return addresses.Any(s => s == "*") ? "*" : string.Join(",", addresses);
        }

        // ReSharper disable once MethodNameNotMeaningful
        public static IPAddress Max(IPAddress val1, IPAddress val2)
        {
            if (val1.AddressFamily != val2.AddressFamily)
            {
                throw new ArgumentException("Addresses of different family can not be compared.");
            }

            var bytes1 = val1.GetAddressBytes();
            var bytes2 = val2.GetAddressBytes();

            for (var i = 0; i < bytes1.Length; i++)
            {
                if (bytes1[i] > bytes2[i])
                {
                    return val1;
                }

                if (bytes2[i] > bytes1[i])
                {
                    return val2;
                }
            }

            return val1;
        }

        // ReSharper disable once MethodNameNotMeaningful
        public static IPAddress Min(IPAddress val1, IPAddress val2)
        {
            if (val1.AddressFamily != val2.AddressFamily)
            {
                throw new ArgumentException("Addresses of different family can not be compared.");
            }

            var bytes1 = val1.GetAddressBytes();
            var bytes2 = val2.GetAddressBytes();

            for (var i = 0; i < bytes1.Length; i++)
            {
                if (bytes1[i] < bytes2[i])
                {
                    return val1;
                }

                if (bytes2[i] < bytes1[i])
                {
                    return val2;
                }
            }

            return val1;
        }

        // ReSharper disable once ExcessiveIndentation
        public static IAddress[] StringToAddresses(string str)
        {
            var remoteAddresses = new List<IAddress>();

            foreach (var remoteAddress in str.Split(','))
            {
                if (SpecialAddress.TryParse(remoteAddress, out var specialAddress))
                {
                    remoteAddresses.Add(specialAddress);
                }
                else if (SingleIP.TryParse(remoteAddress, out SingleIP ip))
                {
                    remoteAddresses.Add(ip);
                }
                else if (IPRange.TryParse(remoteAddress, out var range))
                {
                    remoteAddresses.Add(range);
                }
                else if (NetworkAddress.TryParse(remoteAddress, out var network))
                {
                    remoteAddresses.Add(network);
                }
            }

            return remoteAddresses.ToArray();
        }
    }
}