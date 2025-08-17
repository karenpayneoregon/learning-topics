using System.ComponentModel;
using System.Globalization;

namespace DateTimeWeekNumberSample.Classes;

#pragma warning disable CS8603, CS8765

/// <summary>
/// Provides functionality for converting objects to and from <see cref="Week"/> instances.
/// </summary>
/// <remarks>
/// This class extends <see cref="TypeConverter"/> to enable conversion between <see cref="string"/> representations
/// and <see cref="Week"/> objects. It is primarily used for parsing and formatting week-related data.
/// </remarks>
public class WeekConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is string input)
        {
            return Week.TryParse(input);
        }

        return base.ConvertFrom(context, culture, value);
    }
}