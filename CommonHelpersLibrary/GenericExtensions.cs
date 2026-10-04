using System.Numerics;

namespace CommonHelpersLibrary;

public static class GenericExtensions
{
  
    extension<T>(T sender) where T : INumber<T>
    {
        public bool IsEven() => sender % T.CreateChecked(2) == T.Zero;
        public bool IsOdd() => sender % T.CreateChecked(2) != T.Zero;
    }

}