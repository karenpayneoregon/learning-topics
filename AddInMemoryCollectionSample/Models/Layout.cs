namespace AddInMemoryCollectionSample.Models;

/// <summary>
/// Represents a layout model containing header, title, and footer information read from appsettings.json.
/// </summary>
public class Layout
{
    public string Header { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Footer { get; set; } = string.Empty;
}