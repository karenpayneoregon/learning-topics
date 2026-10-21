namespace EnvironmentApplication.Classes;

public static class StringExtensions
{
    public static string ToFormatted(this bool? sender, string ifTrue, string ifFalse, string ifNull)
    {
        if (!sender.HasValue) return ifNull;
        return sender.Value ? ifTrue : ifFalse;
    }

    /// <summary>
    /// Display SSN last four numbers e.g. xxx-xx-1234
    /// </summary>
    public static string MaskSsn(this string ssn, int digitsToShow = 4, char maskCharacter = 'X')
    {
        if (string.IsNullOrWhiteSpace(ssn))
        {
            return string.Empty;
        }

        if (ssn.Length != 9)
        {
            throw new ArgumentException("SSN invalid length");
        }

        const int ssnLength = 9;
        const string separator = "-";
        int maskLength = ssnLength - digitsToShow;
        
        int output = int.Parse(ssn.Replace(separator, string.Empty).Substring(maskLength, digitsToShow));

        string format = string.Empty;
        for (int index = 0; index < maskLength; index++) format += maskCharacter;
        for (int index = 0; index < digitsToShow; index++) format += "0";

        format = format.Insert(3, separator).Insert(6, separator);
        format = "{0:" + format + "}";

        return string.Format(format, output);
    }
}