using CommonHelpersLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonHelpersLibrary
{
    public static partial class StringExtensions
    {    /// <param name="sender">The input string.</param>
        extension(string sender)
        {


            /// <summary>
            /// Converts the first character of the given string to uppercase.
            /// </summary>
            /// <returns>
            /// A new string with the first character converted to uppercase 
            /// </returns>
            public string CapitalizeFirstLetter()
                => string.IsNullOrEmpty(sender) ?
                    sender :
                    $"{char.ToUpper(sender[0])}{sender[1..].ToLower()}";

            /// <summary>
            /// Replaces the last occurrence of a specified string within the given string.
            /// </summary>
            /// <param name="find">The string to find case-sensitive.</param>
            /// <param name="replace">The string to replace the found string with.</param>
            /// <returns>A new string with the last occurrence of the specified string replaced.</returns>
            public string ReplaceLast(string find, string replace)
            {
                int index = sender.LastIndexOf(find, StringComparison.OrdinalIgnoreCase);

                return index == -1 ?
                    sender :
                    sender.Remove(index, find.Length).Insert(index, replace);
            }

            /// <summary>
            /// Trims the last character from the given string.
            /// </summary>
            /// <returns>A new string with the last character removed, or the original string if it is null or whitespace.</returns>
            public string TrimLastCharacter()
                => string.IsNullOrWhiteSpace(sender) ?
                    sender :
                    sender[..^1];



            /// <summary>
            /// Analyzes the characters in the string and returns an ordered collection of their occurrences.
            /// </summary>
            /// <returns>
            /// An <see cref="IOrderedEnumerable{T}"/> of <see cref="OccurrencesItem"/> objects, 
            /// where each object represents a character, its occurrence count, and its Unicode code.
            /// </returns>
            public IOrderedEnumerable<OccurrencesItem> Occurrences() =>
                (sender.ToCharArray()
                    .GroupBy(chr => chr)
                    .Select(grp => new OccurrencesItem
                    {
                        Character = grp.Key,
                        Occurrences = grp.Count(),
                        Code = Convert.ToInt32((int)grp.Key)
                    }))
                .ToList()
                .OrderBy(item => item.Character.ToString());


        }


    }
}
