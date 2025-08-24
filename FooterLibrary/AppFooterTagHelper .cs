using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FooterLibrary;

/// <summary>
/// Represents a custom Razor TagHelper for rendering a footer element in an ASP.NET Core application.
/// </summary>
/// <remarks>
/// This TagHelper generates a footer element with customizable properties such as application name, author name, year, and CSS classes.
/// It supports both normal and self-closing tag structures and is targeted for the "app-footer" HTML element.
///
/// In the example below, the TagHelper is used to create a footer for a
/// "Payroll System" application, optionally including the author's name and the second
/// specifies the author's name and uses the application name.
///
/// Also, year is configurable, defaulting to the current year if not provided.
/// </remarks>
/// <example>
///     <code>
///         <app-footer app-name="Payroll System" include-author="false" />
///         <app-footer app-name="Payroll System" author-name="John Doe" year="2030" />
///         <app-footer app-name="Payroll System" author-name="John Doe" />
///     </code>
/// </example>
[HtmlTargetElement("app-footer", TagStructure = TagStructure.NormalOrSelfClosing)]
public sealed class AppFooterTagHelper : TagHelper
{
    private const string DefaultAppName = "TODO"; // Replace "TODO" with your default application name
    private const string DefaultAuthorName = "Karen Payne";

    public string? AppName { get; set; }
    public bool IncludeAuthor { get; set; } = true;  // defaults to true
    public string? AuthorName { get; set; }
    public int? Year { get; set; }
    public string Class { get; set; } = "footer border-top text-muted";
    public string ContainerClass { get; set; } = "container";

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "footer";
        output.TagMode = TagMode.StartTagAndEndTag;

        // classes
        output.Attributes.SetAttribute("class", Class);

        var year = Year ?? DateTime.UtcNow.Year;

        // Resolve AuthorName fallback
        var author = string.IsNullOrWhiteSpace(AuthorName) ? DefaultAuthorName : AuthorName;
        var authorHtml = IncludeAuthor
            ? $" <span style=\"margin-left: 5px;\">by <strong>{HtmlEncoder.Default.Encode(author)}</strong></span>"
            : string.Empty;

        // Resolve AppName fallback
        var app = HtmlEncoder.Default.Encode(
            string.IsNullOrWhiteSpace(AppName) ? DefaultAppName : AppName
        );

        output.Content.SetHtmlContent(
            $"<div class=\"{ContainerClass}\">&copy; {year}{authorHtml} - {app}</div>"
        );
    }
}