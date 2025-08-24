using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FooterLibrary
{
    /// <summary>
    /// Represents a custom TagHelper for rendering a footer element in an ASP.NET Core Razor view.
    /// </summary>
    /// <remarks>
    /// This TagHelper is designed to generate a footer element with customizable properties such as application name, author details, year, and CSS classes.
    /// It targets the <c>app-footer</c> HTML element and supports both normal and self-closing tag structures.
    /// </remarks>
    [HtmlTargetElement("app-footer", TagStructure = TagStructure.NormalOrSelfClosing)]
    public sealed class AppFooterTagHelper : TagHelper
    {
        public string AppName { get; set; } = "IsSectionDefinedApp";
        public bool IncludeAuthor { get; set; } = true;
        public string AuthorName { get; set; } = "Karen Payne";
        public int? Year { get; set; }
        public string Class { get; set; } = "footer border-top text-muted";
        public string ContainerClass { get; set; } = "container";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "footer";
            output.TagMode = TagMode.StartTagAndEndTag; // << important

            // classes
            output.Attributes.SetAttribute("class", Class);

            var year = Year ?? DateTime.UtcNow.Year;
            var authorHtml = IncludeAuthor
                ? $@" <span style=""margin-left: 5px;"">by <strong>{HtmlEncoder.Default.Encode(AuthorName)}</strong></span>"
                : string.Empty;

            // Only encode the free text parts; classes are literal
            var app = HtmlEncoder.Default.Encode(AppName);

            output.Content.SetHtmlContent(
                $@"<div class=""{ContainerClass}"">&copy; {year}{authorHtml} - {app}</div>"
            );
        }
    }
}