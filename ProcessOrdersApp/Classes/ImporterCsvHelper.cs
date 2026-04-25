using CsvHelper;
using CsvHelper.Configuration;
using ProcessOrdersApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessOrdersApp.Classes;
using Serilog;


namespace ProcessOrdersApp.Classes;
internal class ImporterCsvHelper
{

    public static void Execute(string filePath = "output.csv")
    {
            
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<OrdersResultsMap>();
        var records = csv.GetRecords<OrdersResults>().ToList();
        
    }

    public static List<OrdersResults> Execute1(string filePath = "output.csv")
    {
        var records = new List<OrdersResults>();

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            BadDataFound = args =>
            {
                Console.WriteLine($"Bad data on row {args.Context.Parser.Row}");
                Console.WriteLine(args.RawRecord);
            },
            MissingFieldFound = null,
            HeaderValidated = null
        };

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, config);

        csv.Context.RegisterClassMap<OrdersResultsMap>();

        while (csv.Read())
        {
            try
            {
                var record = csv.GetRecord<OrdersResults>();
                if (record != null)
                {
                    records.Add(record);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Skipping row {csv.Context.Parser.Row}: {ex.Message}");
            }
        }
        
        Log.Information("Finished processing CSV file: {FilePath}", filePath);

        return records;
    }

}


public sealed class OrdersResultsMap : ClassMap<OrdersResults>
{
    public bool Process { get; set; } = false;
    public OrdersResultsMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(m => m.Process).Ignore();
    }

}
