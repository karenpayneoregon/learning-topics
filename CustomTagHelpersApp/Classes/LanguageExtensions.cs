using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CustomTagHelpersApp.Classes;

/// <summary>
/// Provides extension methods for working with enumerations and their metadata, 
/// such as retrieving numeric representations and display names.
/// </summary>
public static class LanguageExtensions
{
    /// <summary>
    /// Retrieves the actual numeric representation of the specified <see cref="WeekDays"/> enumeration value.
    /// </summary>
    /// <param name="sender">The <see cref="WeekDays"/> enumeration value for which the numeric representation is to be retrieved.</param>
    /// <returns>An <see cref="int"/> representing the numeric value of the specified <see cref="WeekDays"/> enumeration, incremented by 1.</returns>
    public static int Actual(this WeekDays sender) => (int)sender + 1;

    /// <summary>
    /// Retrieves the display name of the specified enumeration value, if a <see cref="DisplayAttribute"/> is applied.
    /// </summary>
    /// <param name="sender">The enumeration value whose display name is to be retrieved.</param>
    /// <returns>
    /// A <see cref="string"/> representing the display name of the enumeration value if a <see cref="DisplayAttribute"/> is defined;
    /// otherwise, the string representation of the enumeration value.
    /// </returns>
    public static string GetDisplayName(this Enum sender)
    {
        var member = sender.GetType().GetMember(sender.ToString())
            .FirstOrDefault();

        if (member != null)
        {
            var displayAttr = member.GetCustomAttribute<DisplayAttribute>();
            if (displayAttr != null)
            {
                return displayAttr.Name ?? sender.ToString();
            }
        }

        return sender.ToString();
    }
}