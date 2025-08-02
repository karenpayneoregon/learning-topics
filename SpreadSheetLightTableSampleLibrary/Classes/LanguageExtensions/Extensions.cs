using System.Data;
using FastMember;

namespace SpreadSheetLightTableSampleLibrary.Classes.LanguageExtensions;

public static class Extensions
{
    /// <summary>
    /// Converts an <see cref="IEnumerable{T}"/> to a <see cref="DataTable"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source collection.</typeparam>
    /// <param name="sender">The source collection to convert.</param>
    /// <param name="allowDbNull">A boolean value indicating whether to allow DBNull values in the resulting DataTable.</param>
    /// <returns>A <see cref="DataTable"/> representation of the source collection.</returns>
    public static DataTable ToDataTable<T>(this IEnumerable<T> sender, bool allowDbNull)
    {
        DataTable table = new(typeof(T).Name);
        using var reader = ObjectReader.Create(sender);
        table.Load(reader);

        if (!allowDbNull) return table;
        foreach (DataColumn column in table.Columns)
        {
            column.AllowDBNull = true;
        }

        return table;
    }

    /// <summary>
    /// Converts an <see cref="IEnumerable{T}"/> to a <see cref="DataTable"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source collection.</typeparam>
    /// <param name="sender">The source collection to convert.</param>
    /// <returns>A <see cref="DataTable"/> representation of the source collection.</returns>
    public static DataTable ToDataTable<T>(this IEnumerable<T> sender)
    {
        DataTable table = new(typeof(T).Name);
        using var reader = ObjectReader.Create(sender);
        table.Load(reader);
        return table;
    }
}