﻿using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace FieldKeywordSample;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "Field code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
