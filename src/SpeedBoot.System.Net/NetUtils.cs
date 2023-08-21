// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Net;

public static class NetUtils
{
    public static string? GetIpV4()
    {
        string? ipv4 = null;
        var allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface networkInterface in allNetworkInterfaces)
        {
            if ((networkInterface.NetworkInterfaceType != NetworkInterfaceType.Ethernet &&
                 networkInterface.NetworkInterfaceType != NetworkInterfaceType.Wireless80211) ||
                networkInterface.Name.ToUpper().Contains("BLUETOOTH") || networkInterface.Name.ToUpper().Contains("VMWARE")) continue;
            ipv4 = GetIpv4StringFromNetworkAdapter(ipv4, networkInterface);
            if (ipv4 != null)
            {
                break;
            }
        }

        return ipv4;

        static string? GetIpv4StringFromNetworkAdapter(string? ipv4Address, NetworkInterface networkInterface)
        {
            foreach (var unicastAddress in networkInterface.GetIPProperties().UnicastAddresses)
            {
                if (unicastAddress.Address.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipv4Address = unicastAddress.Address.ToString();
                }
            }

            return ipv4Address;
        }
    }
}
