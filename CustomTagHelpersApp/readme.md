# TagHelpers example usage

- AppFooterTagHelper
- WebsiteInformationTagHelper
- ColorLabelTagHelper

## AppFooterTagHelper

```html
<app-footer app-name="Payroll System" author-name="John Doe" />

<app-footer app-name="Payroll System" include-author="false" />

<app-footer app-name="Payroll System" author-name="John Doe" year="2030" />
```

## _ViewImports.cshtml
```csharp
@addTagHelper *, FooterLibrary
@addTagHelper *, CustomTagHelpersLibrary
```

### site.css
```css
.footer {
    position: absolute;
    bottom: 0;
    width: 100%;
    white-space: nowrap;
    line-height: 60px;
}
```

## Project References

```xml
<ItemGroup>
  <ProjectReference include="..\CustomTagHelpersLibrary\CustomTagHelpersLibrary.csproj" />
  <ProjectReference include="..\FooterLibrary\FooterLibrary.csproj" />
</ItemGroup>
```