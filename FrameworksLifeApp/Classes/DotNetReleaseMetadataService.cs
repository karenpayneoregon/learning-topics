using FrameworksLifeApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FrameworksLifeApp.Classes;
public sealed class DotNetReleaseMetadataService : IDotNetReleaseMetadataService
{
    private readonly HttpClient _http;

    public DotNetReleaseMetadataService(HttpClient http) => _http = http;

    public async Task<IReadOnlyList<DotNetChannelInfo>> GetChannelsAsync(CancellationToken cancellationToken = default)
    {
        using var indexDoc = await GetJsonAsync("releases-index.json", cancellationToken);

        // releases-index.json shape:
        // { "releases-index": [ { "channel-version": "8.0", "support-phase": "...", "eol-date": "...", "releases.json": "releases-index.json??" } ... ] }
        var root = indexDoc.RootElement;
        if (!root.TryGetProperty("releases-index", out var arr) || arr.ValueKind != JsonValueKind.Array)
            throw new InvalidOperationException("Unexpected releases-index.json shape (missing 'releases-index').");

        var channels = new List<DotNetChannelInfo>();

        foreach (var item in arr.EnumerateArray())
        {
            var channelVersion = GetString(item, "channel-version");
            if (string.IsNullOrWhiteSpace(channelVersion))
                continue;

            // Keep .NET Core/5+ channels (2.0+). Excludes classic .NET Framework.
            if (!Version.TryParse(channelVersion, out var v) || v.Major < 2)
                continue;

            var info = new DotNetChannelInfo
            {
                ChannelVersion = channelVersion,
                SupportPhase = GetString(item, "support-phase"),
                EolDate = GetDate(item, "eol-date"),
                ReleasesJsonPath = GetString(item, "releases.json") // usually a relative path like "releases/net8.0/releases.json"
            };

            // Enrich from channel releases.json (release-type + latest versions)
            if (!string.IsNullOrWhiteSpace(info.ReleasesJsonPath))
            {
                try
                {
                    using var channelDoc = await GetJsonAsync(info.ReleasesJsonPath!, cancellationToken);
                    var croot = channelDoc.RootElement;

                    info.ReleaseType = GetString(croot, "release-type");        // "lts" or "sts" typically
                    info.LatestSdk = GetString(croot, "latest-sdk");            // e.g. "8.0.4xx"
                    info.LatestRuntime = GetString(croot, "latest-runtime");    // e.g. "8.0.xx"
                    info.LatestAspNetCoreRuntime = GetString(croot, "latest-aspnetcore-runtime"); // if present
                }
                catch
                {
                    // If Microsoft changes shape or a channel file is missing, keep index fields and move on.
                }
            }

            channels.Add(info);
        }

        return channels;
    }

    private async Task<JsonDocument> GetJsonAsync(string relativeOrAbsolute, CancellationToken cancellationToken)
    {
        // HttpClient has BaseAddress; relative paths work. Absolute URLs also work.
        using var resp = await _http.GetAsync(relativeOrAbsolute, cancellationToken);
        resp.EnsureSuccessStatusCode();

        await using var stream = await resp.Content.ReadAsStreamAsync(cancellationToken);
        return await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);
    }

    private static string? GetString(JsonElement obj, string name)
    {
        if (!obj.TryGetProperty(name, out var p)) return null;
        return p.ValueKind switch
        {
            JsonValueKind.String => p.GetString(),
            JsonValueKind.Number => p.GetRawText(),
            JsonValueKind.True => "true",
            JsonValueKind.False => "false",
            _ => p.GetRawText()
        };
    }

    private static DateTime? GetDate(JsonElement obj, string name)
    {
        var s = GetString(obj, name);
        if (string.IsNullOrWhiteSpace(s)) return null;
        return DateTime.TryParse(s, out var dt) ? dt.Date : null;
    }
}

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
}