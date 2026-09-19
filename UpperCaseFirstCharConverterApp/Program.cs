using Spectre.Console;
using System.Text.Json;
using UpperCaseFirstCharConverterApp.Classes;
using UpperCaseFirstCharConverterApp.JsonConverters;
using UpperCaseFirstCharConverterApp.Models;
using static UpperCaseFirstCharConverterApp.Classes.SpectreConsoleHelpers;

namespace UpperCaseFirstCharConverterApp;
internal partial class Program
{
    private static void Main(string[] args)
    {
        
        //Samples.GlobalExample();
        Samples.PropertiesExample();

        ExitPrompt();
    }

 

}
