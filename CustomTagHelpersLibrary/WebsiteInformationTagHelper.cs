using CustomTagHelpersLibrary.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CustomTagHelpersLibrary;
/// <summary>
/// A custom TagHelper that generates an HTML section element containing website information.
/// </summary>
/// <remarks>
/// This TagHelper utilizes the <see cref="WebsiteContext"/> class to display
/// details such as version, copyright year, approval status, and the number of tags to show.
///
/// FROM Microsoft:
/// https://learn.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/authoring?view=aspnetcore-9.0
/// </remarks>
public class WebsiteInformationTagHelper : TagHelper
{
    public WebsiteContext Info { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "section";
        output.Content.SetHtmlContent(
            $@"<ul><li><strong>Version:</strong> {Info.Version}</li>
<li><strong>Copyright Year:</strong> {Info.CopyrightYear}</li>
<li><strong>Approved:</strong> {Info.Approved}</li>
<li><strong>Number of tags to show:</strong> {Info.TagsToShow}</li></ul>");
        output.TagMode = TagMode.StartTagAndEndTag;
    }
}
