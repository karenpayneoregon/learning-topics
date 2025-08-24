namespace CustomTagHelpersLibrary.Models;
/// <summary>
/// Represents the context of a website, providing metadata and configuration details.
/// </summary>
/// <remarks>
/// This class is used to encapsulate information such as the website's version, copyright year,
/// approval status, and the number of tags to display. It is commonly utilized in conjunction
/// with custom TagHelpers, such as <see cref="WebsiteInformationTagHelper"/>,
/// to render website-related information dynamically in Razor views.
///
/// FROM Microsoft:
/// https://learn.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/authoring?view=aspnetcore-9.0
/// 
/// </remarks>
public class WebsiteContext
{
    public Version? Version { get; set; }
    public int CopyrightYear { get; set; }
    public bool Approved { get; set; }
    public int TagsToShow { get; set; }
}
