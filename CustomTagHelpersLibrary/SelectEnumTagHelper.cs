using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel.DataAnnotations;


namespace CustomTagHelpersLibrary;
/// <summary>
/// creates a dropdown list from custom enum with supports for localization
/// </summary>
/// <remarks>
/// Original source 
/// https://github.com/LazZiya/TagHelpers/blob/master/LazZiya.TagHelpers/SelectEnumTagHelper.cs
///
/// Karen Payne, 09/01/2025
/// Modified to add Bootstrap classes, focus ring support, and id attributes
/// </remarks>
[HtmlTargetElement("select-enum")]
public class SelectEnumTagHelper : TagHelper
{
    [HtmlAttributeName("selected-value")]
    public int SelectedValue { get; set; }

    [HtmlAttributeName("enum-type")]
    public Type EnumType { get; set; } = null!;

    public Func<string, string>? TextLocalizerDelegate { get; set; }

    // Map the HTML class attribute
    [HtmlAttributeName("class")]
    public string? AdditionalClasses { get; set; }

    [HtmlAttributeName("focus-ring-color")]
    public string? FocusRingColor { get; set; }

    [HtmlAttributeName("focus-ring-width")]
    public string? FocusRingWidth { get; set; }

    // New property for id
    [HtmlAttributeName("id")]
    public string? Id { get; set; }

    // Optional: support setting name explicitly
    [HtmlAttributeName("name")]
    public string? Name { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "select";

        // classes: add Bootstrap + focus helper, then merge user classes
        var classes = new List<string> { "form-select", "focus-ring" };
        if (!string.IsNullOrWhiteSpace(AdditionalClasses))
            classes.AddRange(AdditionalClasses.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        output.Attributes.SetAttribute("class", string.Join(' ', classes.Distinct(StringComparer.Ordinal)));

        // id
        if (!string.IsNullOrWhiteSpace(Id))
            output.Attributes.SetAttribute("id", Id);

        // name
        if (!string.IsNullOrWhiteSpace(Name))
            output.Attributes.SetAttribute("name", Name);

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

        // build <option> list
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

    /// <summary>
    /// Retrieves the display name of an enumeration field based on its value.
    /// </summary>
    /// <param name="value">The integer value of the enumeration field.</param>
    /// <returns>
    /// The display name of the enumeration field if a <see cref="DisplayAttribute"/> is defined; 
    /// otherwise, the name of the enumeration field.
    /// </returns>
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


