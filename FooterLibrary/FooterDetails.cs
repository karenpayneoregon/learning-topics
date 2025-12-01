#nullable disable
namespace FooterLibrary;

/// <summary>
/// For <see cref="AppFooterTagHelper"/> to render a footer section in an application.
/// </summary>
public class FooterDetails
{
    public string ApplicationName { get; set; }
    public string AuthorName { get; set; }
    public int CopyrightYear { get; set; }
}
