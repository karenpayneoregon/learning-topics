using DocumentFormat.OpenXml.Spreadsheet;
using Serilog;
using SpreadsheetLight;
using Color = System.Drawing.Color;
#pragma warning disable CS8602

namespace SpreadSheetLightTableSampleLibrary.Classes;

/// <summary>
/// Provides operations for creating and manipulating Excel files using the SpreadsheetLight library.
/// </summary>
/// <remarks>
/// This class includes methods for generating Excel reports, applying custom styles, and creating sortable headers.
/// It is designed to work with data tables and provides functionality to format and organize data within Excel files.
/// </remarks>
public class ExcelOperations
{
    /// <summary>
    /// Generates a customer view report in an Excel file from the provided <see cref="System.Data.DataTable"/>.
    /// </summary>
    /// <param name="table">The data table containing customer data to be included in the report.</param>
    /// <param name="excelFileName">The name of the Excel file to save the report to.</param>
    /// <remarks>
    /// The method modifies the column order and names in the provided data table to match the desired report format.
    /// It creates an Excel file using the SpreadsheetLight library, applies styling to the header row, 
    /// and ensures the header remains visible when scrolling.
    /// </remarks>
    /// <exception cref="Exception">
    /// Thrown if the Excel file cannot be saved, typically due to the file being open in another application.
    /// </exception>
    public static bool CreateCustomerViewReport(System.Data.DataTable table, string excelFileName)
    {

        try
        {
            // move from last column to first column
            table.Columns["Identifier"].SetOrdinal(0);

            // rename columns
            table.Columns["ContactFirstName"].ColumnName = "First Name";
            table.Columns["ContactLastName"].ColumnName = "Last Name";
            table.Columns["ContactType"].ColumnName = "Contact Type";
            table.Columns["GenderType"].ColumnName = "Gender";

            using var document = new SLDocument();

            // insert data into the first worksheet
            document.ImportDataTable(1, SLConvert.ToColumnIndex("A"), table, true);
            
            // set the header style
            var headerStyle = HeaderStyle(document);
            document.SetCellStyle(1, 1, 1, 5, headerStyle);

            // Give WorkSheet a meaningful name
            document.RenameWorksheet(SLDocument.DefaultFirstSheetName, Path.GetFileNameWithoutExtension(excelFileName));
            
            // ensure header is visible when scrolling down
            document.FreezePanes(1, 5);

            // Auto fit columns
            for (var columnIndex = 1; columnIndex < table.Columns.Count; columnIndex++)
            {
                document.AutoFitColumn(columnIndex);
            }

            CreateSortableHeader(document);
            document.SaveAs(excelFileName);

            return true;
        }
        catch (Exception exception)
        {
            Log.Error(exception, "Error creating customer view report in Excel file: {ExcelFileName}", excelFileName);
            return false;
        }

    }

    /// <summary>
    /// Creates a style for the header row in an Excel worksheet.
    /// </summary>
    /// <param name="document">An instance of the <see cref="SLDocument"/> representing the Excel document.</param>
    /// <returns>
    /// A <see cref="SLStyle"/> object that defines the formatting for the header row, 
    /// including bold text, white font color, and a light gray background with accent colors.
    /// </returns>
    /// <remarks>
    /// This method is used to define a consistent and visually appealing style for the header row in Excel worksheets.
    /// The style includes bold text, a white font color, and a patterned background with theme-based accent colors.
    /// </remarks>
    public static SLStyle HeaderStyle(SLDocument document)
    {


        SLStyle headerStyle = document.CreateStyle();

        headerStyle.Font.Bold = true;
        headerStyle.Font.FontColor = Color.White;

        headerStyle.Fill.SetPattern(
            PatternValues.LightGray,
            SLThemeColorIndexValues.Accent1Color,
            SLThemeColorIndexValues.Accent5Color);

        return headerStyle;
    }

    /// <summary>
    /// Configures the headers of an Excel worksheet to be sortable.
    /// </summary>
    /// <param name="document">
    /// An instance of <see cref="SLDocument"/> representing the Excel worksheet to be modified.
    /// </param>
    /// <remarks>
    /// This method retrieves the column headers from the first row of the worksheet, 
    /// sets them as sortable headers, and applies a table structure with a total row.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the provided <paramref name="document"/> is null.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the worksheet does not contain valid data or headers.
    /// </exception>
    public static void CreateSortableHeader(SLDocument document)
    {

        SLWorksheetStatistics? statistics = document.GetWorksheetStatistics();
        var lastColIndex = statistics.EndColumnIndex;

        string lastColLetter = SLConvert.ToColumnName(lastColIndex);
        List<string> columnNames = [];

        var headers = Headers(document, columnNames);

        // Set headers
        for (var index = 0; index < headers.Length; index++)
            document.SetCellValue(1, index + 1, headers[index]);

        var table = document.CreateTable("A1", $"{lastColLetter}{statistics.EndRowIndex + 1}");

        table.HasTotalRow = true;

        document.InsertTable(table);

    }

    /// <summary>
    /// Extracts headers from the first row of the given spreadsheet document.
    /// </summary>
    /// <param name="document">
    /// The spreadsheet document from which to extract headers. Cannot be null.
    /// </param>
    /// <param name="columnNames">
    /// A list to store the extracted column names. Cannot be null.
    /// </param>
    /// <returns>
    /// An array of strings representing the headers extracted from the first row of the spreadsheet.
    /// </returns>
    /// <remarks>
    /// This method iterates through the first row of the spreadsheet document, extracting non-empty cell values
    /// as headers until an empty cell is encountered.
    /// </remarks>
    private static string[] Headers(SLDocument document, List<string> columnNames)
    {
        var colIndex = 1;

        // Loop through the first row to get column headers
        while (true)
        {
            var value = document.GetCellValueAsString(1, colIndex);

            if (string.IsNullOrWhiteSpace(value)) break;

            columnNames.Add(value);
            colIndex++;
        }

        var headers = columnNames.ToArray();
        return headers;
    }

}
