using System.Data;
using System.Text;
using Dapper;
using kp.Dapper.Handlers;
using Microsoft.Data.SqlClient;
using ProcessOrdersApp.Classes.Configuration;
using ProcessOrdersApp.Models;

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
}