using Microsoft.Extensions.Configuration;
using NodaTime.Text;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace StringsBetweenQuotesExample;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        AnsiConsole.MarkupLine("");
        Console.Title = "Code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }



}

public class RegularExpressions
{
    public TimeSpan Timeout { get; set; }
}

public class WorkingDemo
{
    public static void Set()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var settings = config.GetSection(nameof(RegularExpressions)).Get<RegularExpressions>();
        AppDomain.CurrentDomain.SetData(nameof(RegularExpressions.Timeout), settings.Timeout);
    }
    public static TimeSpan? Get() => (TimeSpan?)AppDomain.CurrentDomain.GetData(nameof(RegularExpressions.Timeout));
}
