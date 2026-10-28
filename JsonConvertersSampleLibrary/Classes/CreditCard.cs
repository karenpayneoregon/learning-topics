using System.Text;
using System.Text.RegularExpressions;

namespace JsonConvertersSampleLibrary.Classes;

/// <summary>
/// Provides methods for identifying and obscuring credit card numbers within a block of text.
/// </summary>
/// <remarks>
/// Taken from
/// https://github.com/guyellis/CreditCard/blob/master/Company.CreditCard/CreditCard.cs
/// Karen Payne has made changes for upgrading from .NET Framework to NET8.
/// 
/// </remarks>
public  class CreditCard
{
    /// <summary>
    /// Credit cards must be at least 13 digits long. If this restriction changes
    /// and credit cards with less than 13 digits are available then change this
    /// value.
    /// </summary>
    const int minCreditCardLength = 13;
    /// <summary>
    /// Credit cards can be a maximum of 16 digits long. Increase this number if 
    /// longer credit card numbers become available.
    /// </summary>
    const int maxCreditCardLength = 16;
    /// <summary>
    /// When we are validating if a number is a credit card we also look at the 
    /// characters on either side of it. If the number of characters next to the
    /// credit card are hexadecimal then this is probably a GUID or other machine
    /// generated number and not a credit card.
    /// </summary>
    const int numberOfAdjacentHexCharacters = 4;

    /// <summary>
    /// Given a block of text which may or may not contain credit card numbers, this
    /// function replaces the credit card numbers with the specified character leaving
    /// only the last N digits exposed.
    /// </summary>
    /// <param name="text">ref string - a block of text that may or may not contain credit card numbers. Credit card numbers are obscurred.</param>
    /// <param name="replaceChar">char - the character to replace the credit card numbers with</param>
    /// <param name="exceptLast">int - the number of digits to leave exposed at the end of the card, can be zero</param>
    /// <returns>bool - true if numbers were obscured false if the text wasn't modified</returns>
    public bool ReplacePartOfNumbers(ref string text, char replaceChar, int exceptLast)
    {
        if (string.IsNullOrEmpty(text))
        {
            return false;
        }
        bool textChanged = false;
        List<Capture> allCaptures = CreditCardNumbersInText(text);
        if (allCaptures.Count > 0)
        {
            StringBuilder newValue = new StringBuilder(text); // Need to convert to StringBuilder so we can directly index into it.
            foreach (Capture c in allCaptures)
            {
                int index = c.Index + c.Length - 1; // Get index of last digit of credit card
                int digitCount = 0; // digitCount allows us to leave some digits exposed at end of credit card
                while (index >= c.Index) // loop for length of credit card
                {
                    digitCount += char.IsDigit(newValue[index]) ? 1 : 0; // There might be spaces or dashes so only count digits
                    if (digitCount > exceptLast) // Only if we've already left the number of digits exposed that have been requested then start blatting the numbers.
                    {
                        newValue[index] = replaceChar;
                    }

                    index--; // move towards beginning of number
                }

                textChanged = true; // set flag
            }

            text = newValue.ToString(); // convert StringBuilder back to (ref) text var.
        }
        return textChanged;
    }

    /// <summary>
    /// Replaces credit card numbers in a block of text with * except for the last 4.
    /// </summary>
    /// <param name="text">ref string - a block of text that may or may not contain credit card numbers</param>
    /// <returns>bool - true if numbers were obscured false if the text wasn't modified</returns>
    public bool ReplaceAllButLastFourWithStars(ref string text)
    {
        return ReplacePartOfNumbers(ref text, '*', 4);
    }

    /// <summary>
    /// Replaces credit card numbers in a block of text with *.
    /// </summary>
    /// <param name="text">ref string - a block of text that may or may not contain credit card numbers</param>
    /// <returns>bool - true if numbers were obscured false if the text wasn't modified</returns>
    public bool ReplaceAllWithStars(ref string text) => ReplacePartOfNumbers(ref text, '*', 0);

    /// <summary>
    /// Finds the locations of any valid credit card numbers in a block of text.
    /// </summary>
    /// <param name="text">string - a block of text that may or may not contain credit card numbers</param>
    /// <returns>A list of Capture objects. Each capture object holds the location, length and text of a credit card number in the block of text.</returns>
    public List<Capture> CreditCardNumbersInText(string text)
    {
        // regexString below needs to look like this: @"(((?:\d[^\da-zA-Z\n<>]*){13,16})\d*)"
        // but be flexible to changing the min and max lengths of a credit card.
        string regexString = @"(?:(?<![\d])|^)" +
                             $@"(((?:\d[^\da-zA-Z(\n<>,\.]*){{{minCreditCardLength},{maxCreditCardLength}}}))" +
                             @"(?:(?![\d])|$)";

        Regex cardFind = new Regex(regexString, RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
        MatchCollection mc = cardFind.Matches(text);
        List<Capture> allCaptures = new List<Capture>();

        if (mc.Count > 0)
        {
            List<Capture> guidList = GetGUIDList(text);
            foreach (Match m in mc)
            {
                // The reg-ex pattern above creates 3 groups.
                // The third group (index 2) is the series of characters that could
                // be a credit card. If this series of characters in the third group
                // is exactly 16 characters long then we know that they are continuous
                // digits without anything between them. If this is the case then we
                // need to make sure that the capture that we're going to work with 
                // is the same length otherwise we know that we just have a series of
                // random numbers.
                Capture c1 = m.Groups[0].Captures[0];
                Capture c3 = m.Groups[2].Captures[0];
                if (c3.Length == maxCreditCardLength && c1.Length != c3.Length)
                {
                    continue;
                }

                // Now check that all the numbers that we've found that look like credit cards really
                // are valid credit cards.
                string creditCard = TrimNonDigits(c1.Value);
                if (PassesCreditCardSanityCheck(creditCard) && PassesLuhnTest(creditCard) && NotInGuid(c1.Index, creditCard.Length, guidList) && NotSurroundedByHex(c1, text, numberOfAdjacentHexCharacters))
                {
                    allCaptures.Add(c1);
                }
            }
        }

        return allCaptures;
    }

    /// <summary>
    /// Removes any characters from the ends of a string that aren't digits.
    /// </summary>
    /// <param name="text">text which may have trailing or leading non-digits</param>
    /// <returns></returns>
    protected string TrimNonDigits(string text)
    {
        int start = 0, end = text.Length - 1;
        while (text.Length > start && !char.IsDigit(text, start))
        {
            start++;
        }
        if (start != end)
        {
            while (end > start && !char.IsDigit(text, end))
            {
                end--;
            }
        }

        text = text.Substring(start, 1 + end - start);
        return text;
    }


    /// <summary>
    /// Determines if a given credit card number is not part of any GUIDs found in the text.
    /// </summary>
    /// <param name="ccIndex">The starting index of the credit card number in the text.</param>
    /// <param name="ccLength">The length of the credit card number.</param>
    /// <param name="guidList">A list of captures representing GUIDs found in the text.</param>
    /// <returns>
    /// <c>true</c> if the credit card number is not within any GUIDs; otherwise, <c>false</c>.
    /// </returns>
    protected bool NotInGuid(int ccIndex, int ccLength, List<Capture> guidList)
    {
        // Strategy in this function is to find all GUID's in the block of text and then see
        // if the given capture is in one of them.
        foreach (Capture guidCapture in guidList)
        {
            if (guidCapture.Index <= ccIndex && (guidCapture.Index + guidCapture.Length) >= (ccIndex + ccLength))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Checks to see if the given capture is bounded by a collection of hexadecimal digits
    /// which would indicate that it's not a credit card but a machine generated key of some
    /// sort.
    /// </summary>
    /// <param name="capture">Capture - the location and length of a suspected credit card</param>
    /// <param name="text">string - the body of text that holds a suspected credit card</param>
    /// <param name="numCharToCheck">int - the number of characters to check on each side of the credit card</param>
    /// <returns>bool - true if the capture (credit card) is not surround by hex characters</returns>
    private bool NotSurroundedByHex(Capture capture, string text, int numCharToCheck)
    {
        int beforeIndex = Math.Max(capture.Index - numCharToCheck, 0);
        int beforeLength = Math.Min(numCharToCheck, capture.Index);
        int afterIndex = capture.Index + capture.Length;
        int afterLength = Math.Min(text.Length - afterIndex, numCharToCheck);

        string beforeCC = text.Substring(beforeIndex, beforeLength);
        string afterCC = text.Substring(afterIndex, afterLength);

        int beforeCount = CountConsecutiveHexChar(Reverse(beforeCC));
        int afterCount = CountConsecutiveHexChar(afterCC);

        return ((beforeCount + afterCount) < numCharToCheck);
    }

    /// <summary>
    /// Reverses a string of text.
    /// </summary>
    /// <param name="text">string - the string to be reversed</param>
    /// <returns>string - a reversed string</returns>
    protected string Reverse(string text)
    {
        char[] charArray = text.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    /// <summary>
    /// Checks to see if a character is a valid hexadecimal character.
    /// </summary>
    /// <param name="c">char - character to check</param>
    /// <returns>bool - true if character is a valid hex character (0-9,a-f,A-F)</returns>
    protected bool CharIsHex(char c)
    {
        return c is >= '0' and <= '9' or >= 'a' and <= 'f' or >= 'A' and <= 'F';
    }

    /// <summary>
    /// Counts the number of consecutive hex characters at the start of a string.
    /// </summary>
    /// <param name="text">string - a string of text to check</param>
    /// <returns>int - the number of hex characters at the start of the string</returns>
    protected int CountConsecutiveHexChar(string text)
    {
        int consecutiveHex = 0;
        foreach (char c in text.ToLower())
        {
            if (!CharIsHex(c))
            {
                break;
            }
            consecutiveHex++;
        }
        return consecutiveHex;
    }

    /// <summary>
    /// Find all GUIDs in a block of text and return them as a list of captures
    /// </summary>
    /// <param name="text">string - block of text that may contain GUIDs</param>
    /// <returns>List of Capture's</returns>
    protected List<Capture> GetGUIDList(string text)
    {
        string regexString = @"(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})";

        Regex guidFind = new(regexString, RegexOptions.Multiline);
        MatchCollection mc = guidFind.Matches(text);
        List<Capture> allCaptures = [];

        foreach (Match m in mc)
        {
            Capture c1 = m.Groups[0].Captures[0];
            allCaptures.Add(c1);
        }

        return allCaptures;
    }

    /// <summary>
    /// This function provides basic checks against the structure of a string
    /// to see if it is obviously not a credit card.
    /// </summary>
    /// <param name="cardNumber">string - possible credit card number</param>
    /// <returns>bool - true if this could be a credit card or false otherwise</returns>
    protected bool PassesCreditCardSanityCheck(string cardNumber)
    {
        try
        {
            cardNumber = GetCleanNumber(cardNumber);
            if (cardNumber.StartsWith("0"))
            {
                return false;
            }
            if (cardNumber.Length is < minCreditCardLength or > maxCreditCardLength)
            {
                return false;
            }

            foreach (char c in cardNumber.ToCharArray())
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return PassesCardType(cardNumber);

        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// This function examines the first few characters of a credit card and based on the
    /// length of the credit card determines if it's valid. e.g. a 13 digit credit card
    /// must start with 4.
    /// </summary>
    /// <param name="cardNumber"></param>
    /// <returns></returns>
    protected bool PassesCardType(string cardNumber)
    {
        string[][] lookup =
        [
            ["4"], // Only visa have 13 digit cards and these must start with 4
            ["30", "36", "38"], // Only Diners Club and Carte Blanche have 14 digits
            ["2014", "2149", "34", "37", "2131", "1800", "8699"], // Amex, enRoute, and some JCB have 15 digits
            ["51", "52", "53", "54", "55", "6011", "3", "4"] // 16 digit cards are VISA, Mastercard, JCB
        ];

        int arrayNum = cardNumber.Length - minCreditCardLength;
        for (int i = 0; i < lookup[arrayNum].GetLength(0); i++)
        {
            if (cardNumber.StartsWith(lookup[arrayNum][i]))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Removes dashes and spaces from string.
    /// </summary>
    /// <param name="cardNumber">string - a credit card with (or without) dashes and spaces</param>
    /// <returns>string - a credit card with the dashes and/or spaces removed</returns>
    private string GetCleanNumber(string cardNumber) => cardNumber.Replace("-", "").Replace(" ", "");

    /// <summary>
    /// Does checksum calculation for Luhn's test and returns true if passes Luhn's test.
    /// </summary>
    /// <param name="cardNumber">string - A collection of digits separated by spaces or dashes or not-separated</param>
    /// <returns>bool - true if passes Luhn otherwise false</returns>
    public bool PassesLuhnTest(string cardNumber)
    {
        try
        {
            //Clean the card number-remove dashes and spaces
            cardNumber = GetCleanNumber(cardNumber);

            //Convert card number into digits array
            int[] digits = new int[cardNumber.Length];
            for (int len = 0; len < cardNumber.Length; len++)
            {
                digits[len] = Int32.Parse(cardNumber[len].ToString());
            }

            //Luhn Algorithm
            //Adapted from code available on Wikipedia at
            //http://en.wikipedia.org/wiki/Luhn_algorithm
            int total = 0;
            bool even = false;
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int current = digits[i];
                if (even)
                {
                    current *= 2;
                    if (current > 9)
                    {
                        current -= 9; // cast out nines
                    }
                }
                total += current;
                even = !even;
            }

            //If Mod 10 equals 0, the number is good and this will return true
            return total % 10 == 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
}