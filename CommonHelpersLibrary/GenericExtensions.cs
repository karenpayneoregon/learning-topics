using System.Numerics;

namespace CommonHelpersLibrary;

public static class GenericExtensions
{
  
    public static bool IsEven<T>(this T sender) where T : INumber<T>
        => sender % T.CreateChecked(2) == T.Zero;

    public static bool IsOdd<T>(this T sender) where T : INumber<T>
        => sender % T.CreateChecked(2) != T.Zero;
}