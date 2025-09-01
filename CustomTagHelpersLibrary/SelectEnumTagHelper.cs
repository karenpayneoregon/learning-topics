using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace CustomTagHelpersLibrary;
/// <summary>
/// creates a dropdown list from custom enum with supports for localization
/// </summary>
/// <remarks>
/// https://github.com/LazZiya/TagHelpers/blob/master/LazZiya.TagHelpers/SelectEnumTagHelper.cs
/// </remarks>
public class SelectEnumTagHelper : TagHelper
{
    private readonly ILogger _log;

    public int SelectedValue { get; set; }

    /// <summary>typeof(MyEnum)</summary>
    public Type EnumType { get; set; }

    /// <summary>A delegate for getting localized value.</summary>
    public Func<string, string>? TextLocalizerDelegate { get; set; }

    /// <summary>Optional extra CSS classes (merged). Example: "w-auto my-1".</summary>
    public string? @class { get; set; } // maps to "class" attribute in Razor

    /// <summary>
    /// Optional focus ring overrides. Example:
    /// FocusRingColor="rgba(var(--bs-success-rgb), .25)" FocusRingWidth=".25rem"
    /// </summary>
    public string? FocusRingColor { get; set; }
    public string? FocusRingWidth { get; set; }

    public SelectEnumTagHelper(ILogger<SelectEnumTagHelper> log) => _log = log;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "select";

        // ---- merge classes: ensure Bootstrap 5.3 focus ring + form styling ----
        var existingClass = output.Attributes.FirstOrDefault(a => a.Name == "class")?.Value?.ToString();
        var classParts = new[]
        {
                "form-select",     // Bootstrap select styling
                "focus-ring",      // Bootstrap 5.3 focus helper
                existingClass,
                @class
            }
        .Where(s => !string.IsNullOrWhiteSpace(s))
        .SelectMany(s => s!.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        .Distinct(StringComparer.Ordinal)
        .ToArray();

        output.Attributes.SetAttribute("class", string.Join(' ', classParts));

        // ---- merge optional focus ring CSS variable overrides ----
        var existingStyle = output.Attributes.FirstOrDefault(a => a.Name == "style")?.Value?.ToString();
        var styleParts = new List<string>();
        if (!string.IsNullOrWhiteSpace(FocusRingColor))
            styleParts.Add($"--bs-focus-ring-color: {FocusRingColor}");
        if (!string.IsNullOrWhiteSpace(FocusRingWidth))
            styleParts.Add($"--bs-focus-ring-width: {FocusRingWidth}");

        if (styleParts.Count > 0)
        {
            var mergedStyle = (existingStyle is null || existingStyle.Trim().Length == 0)
                ? string.Join("; ", styleParts) + ";"
                : existingStyle.Trim().TrimEnd(';') + "; " + string.Join("; ", styleParts) + ";";
            output.Attributes.SetAttribute("style", mergedStyle);
        }

        // ---- build options ----
        foreach (int e in Enum.GetValues(EnumType))
        {
            var op = new TagBuilder("option");
            op.Attributes.Add("value", $"{e}");

            var displayText = TextLocalizerDelegate is null
                ? GetEnumFieldDisplayName(e)
                : GetEnumFieldLocalizedDisplayName(e);

            op.InnerHtml.Append(displayText);

            if (e == SelectedValue)
                op.Attributes.Add("selected", "selected");

            output.Content.AppendHtml(op);
        }
    }

    private string GetEnumFieldDisplayName(int value)
    {
        var fieldName = Enum.GetName(EnumType, value)!;
        var displayName = EnumType.GetField(fieldName)!
            .GetCustomAttributes(false)
            .OfType<DisplayAttribute>()
            .SingleOrDefault()?.Name;

        return displayName ?? fieldName;
    }

    private string GetEnumFieldLocalizedDisplayName(int value)
        => TextLocalizerDelegate!(GetEnumFieldDisplayName(value));
}