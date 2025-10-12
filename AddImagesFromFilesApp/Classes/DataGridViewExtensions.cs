namespace AddImagesFromFilesApp.Classes;

public static class DataGridViewExtensions
{
    /// <summary>
    /// Adjusts the column widths of a <see cref="DataGridView"/> to fit their content.
    /// </summary>
    /// <param name="source">The <see cref="DataGridView"/> whose columns will be adjusted.</param>
    /// <param name="sizable">
    /// If <see langword="true"/>, the columns will retain their calculated widths after resizing.
    /// If <see langword="false"/>, the columns will remain automatically resizable.
    /// </param>
    public static void ExpandColumns(this DataGridView source, bool sizable = false)
    {
        foreach (DataGridViewColumn col in source.Columns)
        {
            if (col.ValueType.Name != "ICollection`1")
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        if (!sizable) return;

        for (int index = 0; index <= source.Columns.Count - 1; index++)
        {
            int columnWidth = source.Columns[index].Width;

            source.Columns[index].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            // Set Width to calculated AutoSize value:
            source.Columns[index].Width = columnWidth;
        }


    }


}