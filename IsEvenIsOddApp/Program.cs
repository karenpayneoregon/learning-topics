using System.Numerics;
using CommonHelpersLibrary;

namespace IsEvenIsOddApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int intValue = 11;
            long longValue = 11L;
            decimal decimalValue = 12M;
            double doubleValue = 13D;
            float floatValue = 14F;
            BigInteger bigInteger = new(15);

            Console.WriteLine(intValue.IsEven());       // True
            Console.WriteLine(longValue.IsOdd());       // True
            Console.WriteLine(decimalValue.IsEven());   // True
            Console.WriteLine(doubleValue.IsOdd());     // True
            Console.WriteLine(floatValue.IsEven());     // True
            Console.WriteLine(bigInteger.IsOdd());      // True

            Console.ReadLine();
        }
    }
}
