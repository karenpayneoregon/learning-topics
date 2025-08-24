using CustomTagHelpersLibrary.Extensions;
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
    public WebsiteContext? Info { get; set; }

    /// <summary>
    /// Processes the specified <see cref="TagHelperContext"/> and modifies the output
    /// <see cref="TagHelperOutput"/> to generate an HTML section element containing website information.
    /// </summary>
    /// <param name="context">The context in which the <see cref="TagHelper"/> is executed.</param>
    /// <param name="output">The output to be written to the response.</param>
    /// <remarks>
    /// This method dynamically generates an HTML section element that displays details such as:
    /// <list type="bullet">
    /// <item><description>Version</description></item>
    /// <item><description>Copyright Year</description></item>
    /// <item><description>Approval Status</description></item>
    /// <item><description>Number of tags to show</description></item>
    /// </list>
    /// The information is retrieved from the <see cref="WebsiteContext"/> instance provided via the <see cref="WebsiteInformationTagHelper.Info"/> property.
    /// </remarks>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "section";
        output.Content.SetHtmlContent(
            $"""
             <ul><li><strong>Version:</strong> {Info!.Version}</li>
             <li><strong>Copyright Year:</strong> {Info!.CopyrightYear}</li>
             <li><strong>Approved:</strong> {Info.Approved.ToYesNo()}</li>
             <li><strong>Number of tags to show:</strong> {Info.TagsToShow}</li></ul>
             """);
        output.TagMode = TagMode.StartTagAndEndTag;
    }
}
