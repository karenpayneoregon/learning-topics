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
[HtmlTargetElement("select-enum")]
public class SelectEnumTagHelper : TagHelper
{
    [HtmlAttributeName("selected-value")]
    public int SelectedValue { get; set; }

    [HtmlAttributeName("enum-type")]
    public Type EnumType { get; set; } = default!;

    public Func<string, string>? TextLocalizerDelegate { get; set; }

    // Map the HTML class attr to a safe C# name
    [HtmlAttributeName("class")]
    public string? AdditionalClasses { get; set; }

    [HtmlAttributeName("focus-ring-color")]
    public string? FocusRingColor { get; set; }

    [HtmlAttributeName("focus-ring-width")]
    public string? FocusRingWidth { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "select";

        // classes: add Bootstrap + focus helper, then merge user classes
        var classes = new List<string> { "form-select", "focus-ring" };
        if (!string.IsNullOrWhiteSpace(AdditionalClasses))
            classes.AddRange(AdditionalClasses.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        output.Attributes.SetAttribute("class", string.Join(' ', classes.Distinct(StringComparer.Ordinal)));

        // focus ring CSS vars (optional)
        var stylePieces = new List<string>();
        if (!string.IsNullOrWhiteSpace(FocusRingColor))
            stylePieces.Add($"--bs-focus-ring-color: {FocusRingColor}");
        if (!string.IsNullOrWhiteSpace(FocusRingWidth))
            stylePieces.Add($"--bs-focus-ring-width: {FocusRingWidth}");
        if (stylePieces.Count > 0)
        {
            var existingStyle = (output.Attributes["style"]?.Value?.ToString() ?? "").Trim().TrimEnd(';');
            var merged = string.IsNullOrEmpty(existingStyle) ? "" : existingStyle + "; ";
            merged += string.Join("; ", stylePieces) + ";";
            output.Attributes.SetAttribute("style", merged);
        }

        // options
        foreach (int e in Enum.GetValues(EnumType))
        {
            var option = new TagBuilder("option");
            option.Attributes.Add("value", e.ToString());

            var text = TextLocalizerDelegate is null
                ? GetEnumFieldDisplayName(e)
                : TextLocalizerDelegate(GetEnumFieldDisplayName(e));

            option.InnerHtml.Append(text);

            if (e == SelectedValue)
                option.Attributes.Add("selected", "selected");

            output.Content.AppendHtml(option);
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
}
