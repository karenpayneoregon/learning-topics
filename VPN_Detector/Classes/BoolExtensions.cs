namespace VPN_Detector.Classes;

public static class BoolExtensions
{
    public static string ToYesNo(this bool value) => value ? "Yes" : "No";
    public static string IsConnected(this bool value) => value ? "Active" : "None detected";
}