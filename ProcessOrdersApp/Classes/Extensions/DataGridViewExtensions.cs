using System.Diagnostics;
using System.Windows.Forms;

namespace ProcessOrdersApp.Classes.Extensions;


public static class DataGridViewExtensions
{

    /// <summary>
    /// Adjusts the column widths of the specified <see cref="DataGridView"/> to fit their content.
    /// </summary>
    /// <param name="source">The <see cref="DataGridView"/> whose columns will be adjusted.</param>
    /// <param name="sizable">
    /// A boolean value indicating whether the columns should remain resizable after adjustment.
    /// If <c>true</c>, the columns will be resizable; otherwise, they will have fixed widths.
    /// </param>
    /// <remarks>
    /// Columns with a data property name of "Image" or a value type of <see cref="System.Collections.ICollection"/> 
    /// are excluded from the auto-sizing process.
    /// </remarks>
    public static void ExpandColumns(this DataGridView source, bool sizable = true)
    {
        foreach (DataGridViewColumn col in source.Columns)
        {
            if (col.ValueType.Name == "ICollection`1") continue;
            if (col.DataPropertyName == "Image") continue;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        if (!sizable) return;

        for (int index = 0; index <= source.Columns.Count - 1; index++)
        {
            var temp = source.Columns[index];
            if (temp.DataPropertyName == "Image")
            {
                continue;
            }
            int columnWidth = source.Columns[index].Width;
            source.Columns[index].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            source.Columns[index].Width = columnWidth;
        }
    }
    /// <summary>
    /// Split on upper-cased letters
    /// </summary>
    /// <param name="source"></param>
    public static void FixHeaders(this DataGridView source)
    {

        for (int index = 0; index < source.Columns.Count; index++)
        {
            source.Columns[index].HeaderText = source.Columns[index].HeaderText.SplitCamelCase();
        }
    }

    /// <summary>
    /// Splits a camel-cased string into separate words by inserting spaces before each uppercase letter.
    /// </summary>
    /// <param name="input">The camel-cased string to be split.</param>
    /// <returns>
    /// A new string with spaces inserted before each uppercase letter in the input string. 
    /// If the input string is <c>null</c> or empty, the original input is returned.
    /// </returns>
    /// <example>
    /// For example, the input "CamelCaseString" will be transformed into "Camel Case String".
    /// </example>
    [DebuggerStepThrough]
    public static string SplitCamelCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        Span<char> result = stackalloc char[input.Length * 2];
        var resultIndex = 0;

        for (var index = 0; index < input.Length; index++)
        {
            var currentChar = input[index];

            if (index > 0 && char.IsUpper(currentChar))
            {
                result[resultIndex++] = ' ';
            }

            result[resultIndex++] = currentChar;
        }

        return result[..resultIndex].ToString();
    }

    /// <summary>
    /// Sets the background colors for the rows in the specified <see cref="DataGridView"/>.
    /// </summary>
    /// <param name="source">The <see cref="DataGridView"/> whose row background colors will be set.</param>
    /// <remarks>
    /// This method applies a white background color to even rows and a light gray background color to odd rows.
    /// </remarks>
    public static void SetBackgroundRowColors(this DataGridView source)
    {
        // Set the standard row color (even rows)
        source.RowsDefaultCellStyle.BackColor = Color.White;
        // Set the alternating row color (odd rows)
        source.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
    }
}