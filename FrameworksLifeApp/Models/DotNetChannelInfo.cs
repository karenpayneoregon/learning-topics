namespace FrameworksLifeApp.Models;

/// <summary>
/// Represents metadata about a specific .NET release channel.
/// </summary>
/// <remarks>
/// This class encapsulates information about a .NET release channel, including its version,
/// support phase, end-of-life date, and paths to related metadata files. It also provides
/// details about the latest SDK, runtime, and ASP.NET Core runtime versions for the channel.
/// </remarks>
public sealed class DotNetChannelInfo
{
    public string ChannelVersion { get; init; } = string.Empty;

    // From releases-index.json
    public string? SupportPhase { get; set; }
    public DateTime? EolDate { get; set; }
    public string? ReleasesJsonPath { get; set; }

    // From channel releases.json
    public string? ReleaseType { get; set; } // lts / sts
    public string? LatestSdk { get; set; }
    public string? LatestRuntime { get; set; }
    public string? LatestAspNetCoreRuntime { get; set; }

    public override string ToString() => SupportPhase ?? "(unknown)";

}