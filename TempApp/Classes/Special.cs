using System.Reflection;

namespace TempApp.Classes;
public class Special
{
    public static string GetCopyright()
    {
        var attr = Assembly
            .GetExecutingAssembly()
            .GetCustomAttribute<AssemblyCopyrightAttribute>();
        return attr?.Copyright ?? "No copyright information found.";
    }
}
