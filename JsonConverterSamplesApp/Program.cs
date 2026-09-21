using JsonConverterSamplesApp.Classes.Core;
using JsonConverterSamplesApp.Models;
using Spectre.Console;
using System.Text.Json;
using JsonConverterSamplesApp.Classes;

namespace JsonConverterSamplesApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        Samples.DoubleSerializationStringFormatExample();
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    
}