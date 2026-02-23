# About

Basic `FluentValidation` Razor pages example

> **Note**
> 02/14/2026 added MockedData to load data and removed HasData from the DbContext.

> **Note**
> 11/2025 refactored away from `FluentValidation.AspNetCore` package


## Database

- Create (localdb)\\MSSQLLocalDB;Initial Catalog=FluentValidation1
- Populate using DatabaseScripts\Populate.sql

## Packages

- Add a reference to https://www.nuget.org/packages/FluentValidation/12.0.0?_src=template
- Add a reference to https://www.nuget.org/packages/FluentValidation.DependencyInjectionExtensions/12.0.0?_src=template


## Setup

- In Program.cs add the last line

```csharp
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

       builder.Services.AddFluentValidationAutoValidation();

```

- Add your model, in this case Person

```csharp
public partial class Person
{
    public int PersonId { get; set; }
    [Display(Name = "First")]
    public string FirstName { get; set; }
    [Display(Name = "Last")]
    public string LastName { get; set; }
    [Display(Name = "Email")]
    public string EmailAddress { get; set; }
}
```

- Create a validator e.g.

```csharp
public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress();
    }
}
```

- Add a private instance of the validator to the page, in this case the create page.

```csharp
private IValidator<Person> _validator;
```

- Setup in the page constructor

```csharp
public CreateModel(Data.Context context, IValidator<Person> validator)
{
    _context = context;
    _validator = validator;
}
```

Run the app, try creating a new Person with missing properties for, in this case Person.

:pushpin:  For practice, implement the above in the Edit Page.