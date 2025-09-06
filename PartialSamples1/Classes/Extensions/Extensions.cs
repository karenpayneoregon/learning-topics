#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialSamples1.Classes.Extensions;

public static partial class Extensions
{
    public static partial string MaskSsn(this string ssn, int digitsToShow = 4, char maskCharacter = 'X');
    public static bool IsInteger(this string sender)
    {
        foreach (var c in sender)
            if (c is < '0' or > '9') return false;

        return true;
    }

    public static bool IsNotInteger(this string sender)
        => sender.IsInteger() == false;

    public static string RemoveStartAndEndQuotes(this string sender)
        => (sender?.Length ?? 0) < 2
            ? sender
            : sender!.Length > 1 && (sender[0] == '"' && sender[^1] == '"' || sender[0] == '\'' && sender[^1] == '\'')
                ? sender.Substring(1, sender.Length - 2)
                : sender;
}
