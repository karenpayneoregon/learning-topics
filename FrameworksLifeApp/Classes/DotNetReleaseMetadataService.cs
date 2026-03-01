using FrameworksLifeApp.Interfaces;
using FrameworksLifeApp.Models;
using System.Text.Json;

namespace FrameworksLifeApp.Classes;

/// <summary>
/// Provides functionality to retrieve and process metadata about .NET release channels.
/// </summary>
/// <remarks>
/// This service fetches release metadata from a remote source, processes the data, and provides
/// information about .NET channels, including their version, support phase, end-of-life date,
/// and other related details. It also enriches the data with additional information from
/// channel-specific metadata files.
/// </remarks>
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
                    var channelRoot = channelDoc.RootElement;

                    info.ReleaseType = GetString(channelRoot, "release-type");        // "lts" or "sts" typically
                    info.LatestSdk = GetString(channelRoot, "latest-sdk");            // e.g. "8.0.4xx"
                    info.LatestRuntime = GetString(channelRoot, "latest-runtime");    // e.g. "8.0.xx"
                    info.LatestAspNetCoreRuntime = GetString(channelRoot, "latest-aspnetcore-runtime"); // if present
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

    /// <summary>
    /// Asynchronously retrieves a JSON document from a specified relative or absolute URI.
    /// </summary>
    /// <param name="relativeOrAbsolute">
    /// The URI of the JSON resource to retrieve. This can be a relative path (resolved using the <see cref="HttpClient.BaseAddress"/>)
    /// or an absolute URL.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the parsed <see cref="JsonDocument"/> 
    /// representing the JSON content of the retrieved resource.
    /// </returns>
    /// <exception cref="HttpRequestException">
    /// Thrown when the HTTP request fails or the response indicates an unsuccessful status code.
    /// </exception>
    /// <exception cref="JsonException">
    /// Thrown when the JSON content cannot be parsed into a <see cref="JsonDocument"/>.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Thrown when the operation is canceled via the provided <paramref name="cancellationToken"/>.
    /// </exception>
    private async Task<JsonDocument> GetJsonAsync(string relativeOrAbsolute, CancellationToken cancellationToken)
    {
        // HttpClient has BaseAddress; relative paths work. Absolute URLs also work.
        using var resp = await _http.GetAsync(relativeOrAbsolute, cancellationToken);
        resp.EnsureSuccessStatusCode();

        await using var stream = await resp.Content.ReadAsStreamAsync(cancellationToken);
        return await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Retrieves the value of a specified property from a <see cref="JsonElement"/> as a string.
    /// </summary>
    /// <param name="obj">The <see cref="JsonElement"/> containing the property.</param>
    /// <param name="name">The name of the property to retrieve.</param>
    /// <returns>
    /// The string representation of the property's value if it exists and is valid; otherwise, <c>null</c>.
    /// </returns>
    /// <remarks>
    /// This method handles various JSON value kinds, including strings, numbers, booleans, and raw text.
    /// If the property does not exist or its value is not retrievable, the method returns <c>null</c>.
    /// </remarks>
    private static string? GetString(JsonElement obj, string name)
    {
        if (!obj.TryGetProperty(name, out var je)) return null;
        return je.ValueKind switch
        {
            JsonValueKind.String => je.GetString(),
            JsonValueKind.Number => je.GetRawText(),
            JsonValueKind.True => "true",
            JsonValueKind.False => "false",
            _ => je.GetRawText()
        };
    }

    /// <summary>
    /// Retrieves the value of a specified property from a <see cref="JsonElement"/> as a <see cref="DateTime"/>.
    /// </summary>
    /// <param name="obj">The <see cref="JsonElement"/> containing the property.</param>
    /// <param name="name">The name of the property to retrieve.</param>
    /// <returns>
    /// A <see cref="DateTime"/> representing the property's value if it exists and is valid; otherwise, <c>null</c>.
    /// </returns>
    /// <remarks>
    /// This method attempts to parse the value of the specified property as a <see cref="DateTime"/>.
    /// If the property does not exist, is empty, or cannot be parsed, the method returns <c>null</c>.
    /// </remarks>
    private static DateTime? GetDate(JsonElement obj, string name)
    {
        var value = GetString(obj, name);
        if (string.IsNullOrWhiteSpace(value)) return null;
        return DateTime.TryParse(value, out var dt) ? dt.Date : null;
    }
}