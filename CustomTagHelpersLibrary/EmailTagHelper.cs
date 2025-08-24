using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CustomTagHelpersLibrary;

/// <summary>
/// Represents a custom tag helper that generates an email link (&lt;a> tag) with a "mailto:" href attribute.
/// </summary>
/// <example>
///     <code>
///         <email address="sales@@someCompany.com">Contact us</email>
///     </code>
/// </example>
public class EmailTagHelper : TagHelper
{
    public string Address { get; set; } = null!;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "a";
        output.Attributes.SetAttribute("href", $"mailto:{Address}");
    }
}
