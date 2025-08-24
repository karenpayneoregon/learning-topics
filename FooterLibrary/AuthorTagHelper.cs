using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FooterLibrary;

/// <summary>
/// Represents a custom Razor TagHelper for rendering an "author" HTML element.
/// </summary>
/// <remarks>
/// This TagHelper allows customization of the author element's tag name, CSS class, margin, 
/// and content. By default, it renders a "span" element with the author's name.
/// </remarks>
[HtmlTargetElement("author", TagStructure = TagStructure.NormalOrSelfClosing)]
public sealed class AuthorTagHelper : TagHelper
{
    /// <summary>
    /// Text for the author’s name. Defaults to "Karen Payne".
    /// </summary>
    public string Name { get; set; } = "Karen Payne";

    /// <summary>
    /// CSS margin-left value (e.g., "5px"). Empty to omit inline style.
    /// </summary>
    public string? MarginLeft { get; set; } = "5px";

    /// <summary>
    /// Set an outer tag name. Defaults to "span".
    /// </summary>
    public string Tag { get; set; } = "span";

    /// <summary>
    /// Optional CSS classes applied to the outer tag.
    /// </summary>
    public string? Class { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = Tag; // span by default
        if (!string.IsNullOrWhiteSpace(Class))
            output.Attributes.SetAttribute("class", Class);

        if (!string.IsNullOrWhiteSpace(MarginLeft))
            output.Attributes.SetAttribute("style", $"margin-left: {MarginLeft};");

        var encodedName = HtmlEncoder.Default.Encode(Name);
        output.Content.SetHtmlContent($@"by <strong>{encodedName}</strong>");
    }
}