using System.Text.Json;
using System.Text.Json.Nodes;
using SingletonsApp.Models;

namespace SingletonsApp.Classes;

public static class TransactionInformationReader
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public static TransactionInformation? Load(string path = "appsettings.json")
    {
        JsonNode? root = JsonNode.Parse(File.ReadAllText(path));
        return root?["TransactionInformation"]?.Deserialize<TransactionInformation>(Options);
    }

    public static void Save(TransactionInformation information, string path = "appsettings.json")
    {
        ArgumentNullException.ThrowIfNull(information);

        JsonObject root = JsonNode.Parse(File.ReadAllText(path)) as JsonObject
                          ?? throw new JsonException("The settings file must contain a JSON object.");

        root["TransactionInformation"] = JsonSerializer.SerializeToNode(information, Options);
        File.WriteAllText(path, root.ToJsonString(Options));
    }
}