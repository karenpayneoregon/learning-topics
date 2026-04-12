using System.Data;
using System.Text;
using Dapper;
using kp.Dapper.Handlers;
using Microsoft.Data.SqlClient;
using ProcessOrdersApp.Classes.Configuration;
using ProcessOrdersApp.Models;
using Serilog;

namespace ProcessOrdersApp.Classes;

public static class OrdersCsvExporter
{
    /// <summary>
    /// Exports order data to a CSV file.
    /// </summary>
    /// <param name="outputFilePath">
    /// The full file path where the CSV file will be created or overwritten.
    /// </param>
    /// <remarks>
    /// This method retrieves order data from the database, formats it, and writes it to a CSV file.
    /// The output includes details such as order dates, shipping information, and customer company names.
    /// </remarks>
    public static void ExportOrdersToCsv(string outputFilePath)
    {
        // Allow Dapper to map DateOnly and TimeOnly types
        SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
        SqlMapper.AddTypeHandler(new SqlTimeOnlyTypeHandler());
        
        using IDbConnection connection = new SqlConnection(DataConnections.Instance.MainConnection);

        IEnumerable<OrdersResults> results = connection.Query<OrdersResults>(SqlStatements.GetOrdersBetweenDates);

        using var writer = new StreamWriter(outputFilePath, false, Encoding.UTF8);

        foreach (var row in results)
        {
            writer.WriteLine(string.Join(",",
                row.OrderID,
                FormatDate(row.OrderDate),
                FormatDate(row.RequiredDate),
                FormatDate(row.ShippedDate),
                Escape(row.ShipAddress),
                Escape(row.ShipCity),
                Escape(row.ShipPostalCode),
                Escape(row.ShipCountry),
                Escape(row.CompanyName)
            ));
        }
    }

    /// <summary>
    /// Formats a <see cref="DateOnly"/> value as a string in the "yyyy-MM-dd" format.
    /// </summary>
    /// <param name="date">The <see cref="DateOnly"/> value to format.</param>
    /// <returns>
    /// A string representation of the date in "yyyy-MM-dd" format, or an empty string
    /// if the date is the default value.
    /// </returns>
    private static string FormatDate(DateOnly date)
        => date == default ? string.Empty : date.ToString("yyyy-MM-dd");

    /// <summary>
    /// Escapes a string for safe inclusion in a CSV file.
    /// </summary>
    /// <param name="value">The string value to escape. Can be <c>null</c> or empty.</param>
    /// <returns>
    /// A properly escaped string suitable for CSV formatting. If the input is <c>null</c> 
    /// or consists only of whitespace, an empty string is returned. If the input contains 
    /// special characters such as double quotes, commas, or newlines, it is escaped 
    /// according to CSV standards.
    /// </returns>
    private static string Escape(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        // Standard CSV escaping
        if (value.Contains('"') || value.Contains(',') || value.Contains('\n'))
        {
            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }

        return value;
    }

    /// <summary>
    /// Removes rows from a CSV file based on a list of order IDs.
    /// </summary>
    /// <param name="filePath">
    /// The full file path of the CSV file to process.
    /// </param>
    /// <param name="idsToRemove">
    /// A list of order IDs to identify rows to be removed from the file.
    /// </param>
    /// <param name="hasHeader">
    /// A boolean indicating whether the CSV file contains a header row. Defaults to <c>false</c>.
    /// </param>
    /// <returns>
    /// <c>true</c> if the operation succeeds; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="FileNotFoundException">
    /// Thrown if the specified file does not exist.
    /// </exception>
    /// <remarks>
    /// This method processes the specified CSV file, removing rows where the first column matches
    /// any of the IDs in the <paramref name="idsToRemove"/> list. If <paramref name="hasHeader"/> is <c>true</c>,
    /// the header row is preserved.
    /// </remarks>
    public static bool RemoveRowsByOrderId(string filePath, List<int> idsToRemove, bool hasHeader = false)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("File not found.", filePath);

        var idSet = new HashSet<int>(idsToRemove);

        var tempFile = Path.GetTempFileName();

        try
        {
            using (var reader = new StreamReader(filePath))
            using (var writer = new StreamWriter(tempFile))
            {
                bool isFirstLine = true;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Preserve header if needed
                    if (hasHeader && isFirstLine)
                    {
                        writer.WriteLine(line);
                        isFirstLine = false;
                        continue;
                    }

                    isFirstLine = false;

                    var parts = line.Split(',');

                    if (parts.Length == 0)
                        continue;

                    if (int.TryParse(parts[0], out int id))
                    {
                        if (idSet.Contains(id))
                            continue; // skip row
                    }

                    writer.WriteLine(line);
                }
            }

            // Replace original file safely
            File.Copy(tempFile, filePath, true);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error processing file: {FilePath}", filePath);
            return false;
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }

        return true;
    }
}