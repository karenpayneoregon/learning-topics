#nullable disable
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CustomTagHelpersLibrary;


/*
This TH was written by Rick Strahl
https://github.com/dpaquette/TagHelperSamples/blob/master/TagHelperSamples/src/TagHelperSamples.Bootstrap/TextEntry.cs

Modified by Karen Payne to add Bootstrap classes, margin support, and id attributes

Usage:
- In this case margin-top and margin-bottom are optional 
- margin values are 0 to 5 only for Bootstrap spacing classes

    <text-entry id="firstName"  
               margin-top="3"
               value="@Model.FirstName" 
               label-text="First name"
               placeholder="Enter your first name.">
   </text-entry>


*/

/// <summary>
/// A custom Razor TagHelper that generates a Bootstrap-styled text input field with optional label and spacing.
/// </summary>
/// <remarks>
/// This TagHelper is designed to simplify the creation of text input fields in Razor views by incorporating Bootstrap classes 
/// and additional customization options such as margins, placeholders, and labels. 
/// </remarks>
/// <example>
/// Example usage:
/// <code>
/// <text-entry id="firstName"  
///             margin-top="3"
///             value="@Model.FirstName" 
///             label-text="First name"
///             placeholder="Enter your first name.">
/// </text-entry>
/// </code>
/// </example>
[HtmlTargetElement("text-entry")]
public class TextEntryTagHelper : TagHelper
{
    /// <summary>
    /// Gets or sets the text for the label associated with the text input field.
    /// </summary>
    /// <remarks>
    /// This property specifies the text to be displayed as the label for the text input field. 
    /// It is rendered as the content of the <c>&lt;label&gt;</c> element in the generated HTML.
    /// </remarks>
    /// <example>
    /// Example usage:
    /// <code>
    /// <text-entry id="firstName"  
    ///             label-text="First name"
    ///             value="@Model.FirstName"
    ///             placeholder="Enter your first name.">
    /// </text-entry>
    /// </code>
    /// </example>
    [HtmlAttributeName("label-text")]
    public string LabelText { get; set; }

    [HtmlAttributeName("value")]
    public string Value { get; set; }

    [HtmlAttributeName("placeholder")]
    public string PlaceHolder { get; set; }

    [HtmlAttributeName("textbox-class")]
    public string TextBoxClass { get; set; } = "form-control";

    [HtmlAttributeName("for")]
    public ModelExpression For { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the text input field.
    /// </summary>
    /// <remarks>
    /// This property is used to set the <c>id</c> attribute of the generated HTML input element, 
    /// which allows for unique identification and association with a corresponding label.
    /// </remarks>
    /// <example>
    /// Example usage:
    /// <code>
    /// <text-entry id="firstName"  
    ///             margin-top="3"
    ///             value="@Model.FirstName" 
    ///             label-text="First name"
    ///             placeholder="Enter your first name.">
    /// </text-entry>
    /// </code>
    /// </example>
    [HtmlAttributeName("id")]
    public string Id { get; set; }

    [HtmlAttributeName("type")]
    public string Type { get; set; } = "text";

    /// <summary>
    /// Gets or sets the top margin for the text entry element, using Bootstrap spacing classes.
    /// </summary>
    /// <remarks>
    /// The value must be between 0 and 5, corresponding to Bootstrap's spacing utility classes (e.g., <c>mt-0</c> to <c>mt-5</c>).
    /// If not specified, no top margin is applied.
    /// </remarks>
    /// <example>
    /// Example usage:
    /// <code>
    /// <text-entry id="firstName"  
    ///             margin-top="3"
    ///             value="@Model.FirstName" 
    ///             label-text="First name"
    ///             placeholder="Enter your first name.">
    /// </text-entry>
    /// </code>
    /// </example>
    [HtmlAttributeName("margin-top")]
    public int? MarginTop { get; set; }

    /// <summary>
    /// Gets or sets the bottom margin for the text entry field, using Bootstrap spacing classes.
    /// </summary>
    /// <remarks>
    /// The value must be between 0 and 5, corresponding to Bootstrap's spacing utility classes (e.g., <c>mb-0</c>, <c>mb-1</c>, ..., <c>mb-5</c>).
    /// If not specified, no bottom margin will be applied.
    /// </remarks>
    /// <example>
    /// Example usage:
    /// <code>
    /// <text-entry id="lastName"  
    ///             margin-bottom="2"
    ///             value="@Model.LastName" 
    ///             label-text="Last name"
    ///             placeholder="Enter your last name.">
    /// </text-entry>
    /// </code>
    /// </example>
    [HtmlAttributeName("margin-bottom")]
    public int? MarginBottom { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (string.IsNullOrEmpty(Value) && For != null)
        {
            Value = For.Model?.ToString();
        }

        output.TagName = "div";

        var cssClass = "form-group";

        if (MarginTop is >= 0 and <= 5)
            cssClass += $" mt-{MarginTop.Value}";

        if (MarginBottom is >= 0 and <= 5)
            cssClass += $" mb-{MarginBottom.Value}";

        output.Attributes.SetAttribute("class", cssClass);

        var writer = new System.IO.StringWriter();
        CreateLabelTagBuilder().WriteTo(writer, HtmlEncoder.Default);
        CreateInputTagBuilder().WriteTo(writer, HtmlEncoder.Default);

        output.Content.SetHtmlContent(writer.ToString());
    }

    private TagBuilder CreateLabelTagBuilder()
    {
        var labelBuilder = new TagBuilder("label");
        labelBuilder.Attributes.Add("for", Id);
        labelBuilder.InnerHtml.Append(LabelText);
        return labelBuilder;
    }

    private TagBuilder CreateInputTagBuilder()
    {
        var inputBuilder = new TagBuilder("input");

        inputBuilder.Attributes.Add("id", Id);
        inputBuilder.Attributes.Add("name", Id);
        inputBuilder.AddCssClass(TextBoxClass);
        inputBuilder.Attributes.Add("value", Value);

        if (!string.IsNullOrEmpty(PlaceHolder))
        {
            inputBuilder.Attributes.Add("placeholder", PlaceHolder);
        }

        return inputBuilder;
    }
}
