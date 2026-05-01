using System;
using ProjectPropertiesApp.Classes.Core;
using ProjectPropertiesLibrary;
using Spectre.Console;

namespace ProjectPropertiesApp;
internal partial class Program
{
    static void Main(string[] args)
    {

        ShowDetails();
        
        
        //const string webAddress = "https://secure.some.address/index?key=F20184418231.37&lang=E";
        //var uri = new Uri(webAddress);
        //var queryParameters = uri.GetQueryParameters();

        //var table = new Table().RoundedBorder().BorderColor(Color.Green).Title("[green bold]Query Parameters[/]");

        //table.AddColumn("[yellow bold]Key[/]");
        //table.AddColumn("[yellow bold]Value[/]");

        //foreach (var kvp in queryParameters)
        //{
        //    table.AddRow($"[cyan]{kvp.Key}[/]", $"[white]{kvp.Value}[/]");
        //}

        //AnsiConsole.Write(table);
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }


}
public static class UriExtensions
{
    public static Dictionary<string, string> GetQueryParameters(this Uri uri)
    {
        var queryParameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        if (uri == null || string.IsNullOrEmpty(uri.Query))
        {
            return queryParameters;
        }

        var query = uri.Query.TrimStart('?');
        var pairs = query.Split('&', StringSplitOptions.RemoveEmptyEntries);

        foreach (var pair in pairs)
        {
            var kvp = pair.Split('=', 2);
            var key = Uri.UnescapeDataString(kvp[0]);
            var value = kvp.Length > 1 ? Uri.UnescapeDataString(kvp[1]) : string.Empty;
            queryParameters[key] = value;
        }

        return queryParameters;
    }
}