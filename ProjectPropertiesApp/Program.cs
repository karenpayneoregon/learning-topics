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
        
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

}
