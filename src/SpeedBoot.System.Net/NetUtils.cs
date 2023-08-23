// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Net;

public static class NetUtils
{
    /// <summary>
    /// Returns a collection of all ipv4 addresses
    /// 返回所有 ipv4 地址集合
    /// </summary>
    /// <returns>返回网络接口状态与 ipv4 地址的集合</returns>
    public static List<(OperationalStatus Status, string Address)> GetIpV4List() => GetAssignIpAddressList(AddressFamily.InterNetwork);

    /// <summary>
    /// Returns a collection of all ipv6 addresses
    /// 返回所有 ipv6 地址集合
    /// </summary>
    /// <returns>返回网络接口状态与 ipv6 地址的集合</returns>
    public static List<(OperationalStatus Status, string Address)> GetIpV6List() => GetAssignIpAddressList(AddressFamily.InterNetworkV6);

    /// <summary>
    /// If there are multiple network drivers, according to the state of the network interface, return the ipv4 record of the first network interface. Returns null if no network interface is available
    /// 如果存在多个网络驱动器，则依据网络接口状态，返回第一条网络接口的 ipv4 记录。如果没有可用的网络接口，则返回 null
    /// </summary>
    /// <returns>Return the obtained ipv4 address 返回获取到的 ipv4 地址</returns>
    public static string? GetIpV4() => GetIp(GetAssignIpAddressList(AddressFamily.InterNetwork));

    /// <summary>
    /// If there are multiple network drivers, according to the state of the network interface, return the ipv6 record of the first network interface. Returns null if no network interface is available
    /// 如果存在多个网络驱动器，则依据网络接口状态，返回第一条网络接口的 ipv6 记录。如果没有可用的网络接口，则返回 null
    /// </summary>
    /// <returns>Return the obtained ipv6 address 返回获取到的 ipv6 地址</returns>
    public static string? GetIpV6() => GetIp(GetAssignIpAddressList(AddressFamily.InterNetworkV6));

    private static string? GetIp(List<(OperationalStatus Status, string Address)> ipAddressList)
        => ipAddressList.OrderBy(ip => ip.Status).Select(ip => ip.Address).FirstOrDefault();

    private static List<(OperationalStatus Status, string Address)> GetAssignIpAddressList(AddressFamily addressFamily)
    {
        var allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

        return (from networkInterface in allNetworkInterfaces
            where networkInterface.NetworkInterfaceType is NetworkInterfaceType.Ethernet or NetworkInterfaceType.Wireless80211 &&
                  !networkInterface.Name.ToUpper().Contains("BLUETOOTH") && !networkInterface.Name.ToUpper().Contains("VMWARE")
            let ip = GetResultStringFromNetworkAdapter(networkInterface)
            where ip != null
            select (networkInterface.OperationalStatus, ip)).ToList();

        string? GetResultStringFromNetworkAdapter(NetworkInterface networkInterface)
        {
            string? ipAddress = null;
            foreach (var unicastAddress in networkInterface.GetIPProperties().UnicastAddresses)
            {
                if (unicastAddress.Address.AddressFamily == addressFamily)
                {
                    ipAddress = unicastAddress.Address.ToString();
                }
            }

            return ipAddress;
        }
    }
}
