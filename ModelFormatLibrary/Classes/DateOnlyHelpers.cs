namespace ModelFormatLibrary.Classes;
internal static class DateOnlyHelpers
{
    /// <summary>
    /// Calculates the age based on the provided date of birth.
    /// </summary>
    /// <param name="dateOfBirth">The date of birth as a <see cref="DateOnly"/>.</param>
    /// <returns>The calculated age as an <see cref="int"/>.</returns>
    /// <remarks>
    /// This method calculates the age using the current date and the provided date of birth.
    /// Note that this calculation does not account for leap years.
    /// </remarks>
    public static int GetAge(this DateOnly dateOfBirth)
    {
        var (nYear, nMonth, nDay) = DateTime.Today;

        var a = (nYear * 100 + nMonth) * 100 + nDay;
        var (bYear, bMonth, bDay) = dateOfBirth;
        var b = (bYear * 100 + bMonth) * 100 + bDay;

        return (a - b) / 10000;
    }

}