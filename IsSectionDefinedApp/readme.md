# About

Simple example of [IsSectionDefined](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razor.razorpage.issectiondefined?view=aspnetcore-7.0).

## 08-23-2025

Added `Helpers.KarenHtmlHelpers` for creating application footer

**site.css**

```css
.app-footer {
    position: absolute; /* you’re using the zero-extra-length approach */
    bottom: 0;
    left: 0;
    width: 100%;
    height: 40px;
    display: flex;
    align-items: center;
    padding-top: 5px;
}

.app-footer .container {
    height: 100%;
    /* optional: remove container padding if you want tight height control */
}
```

**KarenHtmlHelpers** class

```csharp
public static class KarenHtmlHelpers
{

    public static IHtmlContent Author(this IHtmlHelper htmlHelper)
        => new HtmlString(
            """
            <span style="margin-left: 5px;">
            by <strong>Karen Payne</strong>
            </span>
            """);

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
```

