using SpreadsheetLight;

namespace SpreadSheetLightTableSampleLibrary.Classes;
public class WorkSheetUtilities
{
    /// <summary>
    /// Does a worksheet exists in an Excel file
    /// </summary>
    /// <param name="document">Instance of a <see cref="SLDocument"/></param>
    /// <param name="sheetName">Sheet name to determine if it exists in <see cref="document"/></param>
    /// <returns></returns>
    public static bool SheetExists(SLDocument document, string sheetName)
        => document.GetSheetNames(false).Any((name)
            => string.Equals(name, sheetName, StringComparison.CurrentCultureIgnoreCase));

    /// <summary>
    /// Get all sheet names in an Excel file
    /// </summary>
    /// <param name="fileName">Existing Excel file without a password</param>
    /// <returns>Sheet names in ordinal order</returns>
    /// <remarks>
    /// Both OleDb and automation return sheet names in A-Z order
    /// </remarks>
    public static List<string> SheetNames(string fileName)
    {
        using var document = new SLDocument(fileName);
        return document.GetSheetNames(false);
    }
}
