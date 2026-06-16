using System.Net.NetworkInformation;

namespace VPN_Detector.Classes;

public static class VpnDetector
{
    public static (bool connected, string vpnName) IsVpnConnected()
    {
        try
        {
            NetworkInterface[] list = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var ni in list)
            {
                if (ni is { OperationalStatus: OperationalStatus.Up, NetworkInterfaceType: NetworkInterfaceType.Ppp }) // PPP often indicates VPN
                {
                    // Additional check: sometimes VPN interfaces have names like "VPN", "Secure", etc.
                    string name = ni.Name.ToLower();
                    string description = ni.Description.ToLower();
                    if (name.Contains("vpn") || name.Contains("_common_") || description.Contains("vpn"))
                    {
                        return (true, ni.Name);
                    }
                }
            }

            return (false, "VPN not initialize or undetectable");
        }
        catch (Exception e)
        {
            return (false, $"Failed to find the adapter {e.Message}");
        }
    }
}