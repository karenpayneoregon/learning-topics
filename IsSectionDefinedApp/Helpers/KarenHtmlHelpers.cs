using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IsSectionDefinedApp.Helpers;

/// <summary>
/// Provides custom HTML helper methods for rendering specific HTML content in the IsSectionDefinedApp application.
/// </summary>
public static class KarenHtmlHelpers
{

    /// <summary>
    /// Generates an HTML content snippet that displays the author's name.
    /// </summary>
    /// <param name="htmlHelper">
    /// An instance of <see cref="IHtmlHelper"/> used to render the HTML content.
    /// </param>
    /// <returns>
    /// An <see cref="IHtmlContent"/> object containing the HTML markup for the author's name.
    /// </returns>
    public static IHtmlContent Author(this IHtmlHelper htmlHelper)
        => new HtmlString(
            """
            <span style="margin-left: 5px;">
            by <strong>Karen Payne</strong>
            </span>
            """);
    /// <summary>
    /// Generates an HTML footer for the application, including the author's name and the current year.
    /// </summary>
    /// <param name="htmlHelper">
    /// An instance of <see cref="IHtmlHelper"/> used to render the HTML content.
    /// </param>
    /// <returns>
    /// An <see cref="IHtmlContent"/> object containing the HTML markup for the application footer.
    /// </returns>
    public static IHtmlContent AppFooter(this IHtmlHelper htmlHelper)
        => new HtmlString(
            $"""
            <footer class="app-footer border-top text-muted">
                <div class="container">
                    &copy; {DateTime.UtcNow.Year}{htmlHelper.Author()} - IsSectionDefinedApp
                </div>
            </footer>
            """);
}