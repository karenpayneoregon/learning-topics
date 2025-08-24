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
    /// <summary>Author’s display name.</summary>
    public string Name { get; set; } = "Jane Payne";

    /// <summary>Inline left margin (e.g., "5px"). Empty/null omits the style.</summary>
    public string? MarginLeft { get; set; } = "5px";

    /// <summary>Outer tag to render (default: span).</summary>
    public string Tag { get; set; } = "span";

    /// <summary>Optional CSS classes on the outer tag.</summary>
    public string? Class { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = Tag;
        output.TagMode = TagMode.StartTagAndEndTag;

        if (!string.IsNullOrWhiteSpace(Class))
            output.Attributes.SetAttribute("class", Class);

        if (!string.IsNullOrWhiteSpace(MarginLeft))
            output.Attributes.SetAttribute("style", $"margin-left: {MarginLeft};");

        var encodedName = HtmlEncoder.Default.Encode(Name);
        output.Content.SetHtmlContent($"by <strong>{encodedName}</strong>");
    }
}