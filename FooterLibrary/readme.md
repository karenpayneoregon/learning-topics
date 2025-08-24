# About

This project provides a reusable footer component for ASP.NET Core applications using Tag Helpers. It allows developers to easily add a consistent footer across their web applications with customizable properties such as application name, author details, year, and CSS classes.

## _ViewImports.cshtml

Add

```csharp
@addTagHelper *, FooterLibrary 
```

Add &lt;app-footer /> as shown below.

## _Layout.cshtml

```html
    <div class="container">
        <main role="main" class="pb-3">
            @RenderBody()
        </main>
    </div>

  
    <app-footer />
```

## site.css

Add the following CSS to your site.css file.

```css
.footer {
    position: absolute;
    bottom: 0;
    width: 100%;
    white-space: nowrap;
    line-height: 60px;
}
```
