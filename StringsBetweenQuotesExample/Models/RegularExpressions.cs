using System.ComponentModel;
using System.Text.Json.Serialization;

namespace StringsBetweenQuotesExample.Models;

/// <summary>
/// Represents a model for handling regular expressions with a configurable timeout.
/// In this case <see cref="Timeout"/> is read from appsettings.json
/// </summary>
public class RegularExpressions
{
    /// <summary>
    /// Gets or sets the timeout value for regular expressions.
    /// </summary>
    /// <value>
    /// A <see cref="TimeSpan"/> representing the maximum time allowed for a regular expression match to execute.
    /// </value>
    /// <remarks>
    /// This property is decorated with a <see cref="JsonConverterAttribute"/> that specifies the use of <see cref="TimeSpanConverter"/> 
    /// for JSON serialization and deserialization.
    /// </remarks>
    [JsonConverter(typeof(TimeSpanConverter))]
    public TimeSpan Timeout { get; set; }
}

public class ConnectionStrings
{
    public string MainConnection { get; set; }
}
public class HelpDesk
{
    public string Phone { get; set; }
    public string Email { get; set; }
}

public class Layout
{
    public string Header { get; set; }
    public string Title { get; set; }
    public string Footer { get; set; }
}
public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; }
    public RegularExpressions RegularExpressions { get; set; }
    public HelpDesk HelpDesk { get; set; }
    public Layout Layout { get; set; }
}
