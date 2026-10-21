using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EnvironmentApplication.TagHelpers
{
    /// <summary>
    /// Extremely simple example for creating a custom tag helper
    /// </summary>
    /// <example>
    ///     <code>
    ///         <color-label color="red">Should output red</color-label>
    ///     </code>
    /// </example>
    public class ColorLabelTagHelper : TagHelper
    {
        public string? Color { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "coloredlabel";
            var colorStyle = $"color:{Color}";
            output.Attributes.SetAttribute("style", colorStyle);
            output.PreContent.SetHtmlContent("<div class='border border-primary mt-2 ps-2 bg-primary text-center bg-opacity-25 fw-bolder'>");
            output.PostContent.SetHtmlContent("</div>");
        }
    }

    public class OEDBorderTagHelper : TagHelper
    {

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "oedborder";
            output.PreContent.SetHtmlContent("<div class='border border-primary mt-2 ps-2 bg-danger text-center bg-opacity-50 fw-bolder'>");
            output.PostContent.SetHtmlContent("</div>");
            base.Process(context, output);
        }
    }
}