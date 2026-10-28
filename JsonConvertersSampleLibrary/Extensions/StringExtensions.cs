#pragma warning disable CA1847
using JsonConvertersSampleLibrary.Classes;
using System.Text.RegularExpressions;

namespace JsonConvertersSampleLibrary.Extensions;
public static partial class StringExtensions
{

    /// <summary>
    /// Masks a Social Security Number (SSN) by replacing a specified number of digits with a mask character.
    /// </summary>
    /// <param name="ssn">The Social Security Number to be masked.</param>
    /// <param name="digitsToShow">The number of digits to show at the end of the SSN. Default is 4.</param>
    /// <param name="maskCharacter">The character to use for masking the SSN. Default is 'X'.</param>
    /// <returns>A masked version of the SSN.</returns>
    /// <exception cref="ArgumentException">Thrown when the SSN is invalid, either due to incorrect length or non-numeric characters.</exception>
    public static string MaskSsn(this string ssn, int digitsToShow = 4, char maskCharacter = 'X')
    {
        if (string.IsNullOrWhiteSpace(ssn)) return string.Empty;
        if (ssn.Contains("-")) ssn = ssn.Replace("-", string.Empty);
        if (ssn.Length != 9) throw new ArgumentException("SSN invalid length");
        if (ssn.IsNotInteger()) throw new ArgumentException("SSN not valid");

        const int ssnLength = 9;
        const string separator = "-";

        int maskLength = ssnLength - digitsToShow;
        int output = int.Parse(ssn.Replace(separator, string.Empty)
            .Substring(maskLength, digitsToShow));

        var format = string.Empty;
        for (var index = 0; index < maskLength; index++)
        {
            format += maskCharacter;
        }

        for (var index = 0; index < digitsToShow; index++)
        {
            format += "0";
        }

        format = format.Insert(3, separator).Insert(6, separator);
        format = $"{{0:{format}}}";

        return string.Format(format, output);

    }

    /// <summary>
    /// Masks a credit card number by replacing all but the last four digits with a specified mask character.
    /// </summary>
    /// <param name="sender">The credit card number to be masked.</param>
    /// <param name="maskCharacter">The character to use for masking the credit card number. Default is 'X'.</param>
    /// <returns>A masked version of the credit card number, with all but the last four digits replaced by the mask character.</returns>
    /// <remarks>
    /// The method uses a regular expression to identify and mask the credit card number.
    /// </remarks>
    public static string MaskCreditCardNumber(this string sender, char maskCharacter = 'X')
    {

        if (string.IsNullOrEmpty(sender))
        {
            return sender;
        }

        return CreditCardMaskRegEx().Replace(sender, match =>
        {
            var digits = string.Concat(match.Value.Where(char.IsDigit));

            return digits.Length is 16 or 15
                ? new string(maskCharacter, digits.Length - 4) + digits[^4..]
                : match.Value;
        });
    }

    /// <summary>
    /// Masks a credit card number by replacing all but the last four digits with a specified mask character.
    /// </summary>
    /// <param name="creditCardNumber">The credit card number to be masked.</param>
    /// <param name="maskCharacter">The character to use for masking the credit card number. Default is 'X'.</param>
    /// <returns>A masked version of the credit card number, with only the last four digits visible.</returns>
    /// <exception cref="ArgumentException">Thrown when the credit card number length is not 14 or 15 digits.</exception>
    public static string MaskCreditCard(this string creditCardNumber, char maskCharacter = 'X')
    {
        // Check if credit card length is 14 or 15
        if (creditCardNumber.Length is 14 or 15)
        {
            // Mask all but the last 4 digits
            return new string(maskCharacter, creditCardNumber.Length - 4) + "-" + creditCardNumber[^4..]; 
        }
        else
        {
            // Throw an exception if the credit card number length is not 14 or 15 is also an option
            return "";
        }
    }

    public static string CreditCardShowLastFourDigits(this string creditCardNumber)
    {
        CreditCard cc = new CreditCard();
        var text = creditCardNumber;
        cc.ReplaceAllButLastFourWithStars(ref text);
        return text;
    }


    [GeneratedRegex("[0-9][0-9 ]{13,}[0-9]")]
    private static partial Regex CreditCardMaskRegEx();

}
