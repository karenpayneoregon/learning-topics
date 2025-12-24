#nullable disable

using FormattableStringBasics.Models;

namespace FormattableStringBasics.Classes;
/// <summary>
/// Provides extension methods for the <see cref="FormattableString"/> type, enabling additional
/// functionality such as retrieving and updating specific arguments for <see cref="Person"/>.
/// </summary>
public static class Extensions
{

    public static int Id(this FormattableString sender)
        => Convert.ToInt32(sender.GetArguments()[0].ToString());

    public static string FirstName(this FormattableString sender)
        => (string)sender.GetArguments()[1];

    public static string LastName(this FormattableString sender)
        => (string)sender.GetArguments()[2];

    public static DateOnly BirthDate(this FormattableString sender)
        => (DateOnly)sender.GetArguments()[3]!;

    public static void UpdateFirstName(this FormattableString sender, string value)
    {
        sender.GetArguments()[1] = value;
    }
    public static void UpdateLastName(this FormattableString sender, string value)
    {
        sender.GetArguments()[2] = value;
    }

    public static void UpdateBirthDate(this FormattableString sender, DateOnly value)
    {
        sender.GetArguments()[3] = value;
    }
}