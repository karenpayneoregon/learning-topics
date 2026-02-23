using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FluentWebApplication.Models;
using FluentValidation;
using FluentValidation.Results;
using FluentWebApplication.Classes;

#pragma warning disable CS8618

namespace FluentWebApplication.Pages;

public class CreateModel(Data.Context context, IValidator<Person> validator) : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Person Person { get; set; } = null!;

    public async Task<IActionResult> OnPostAsync()
    {
        ValidationResult result = await validator.ValidateAsync(Person);
        if (!result.IsValid)
        {

            result.AddToModelState(ModelState, nameof(Person));
            return Page();
  
        }


        context.Person.Add(Person);
        await context.SaveChangesAsync();

        return RedirectToPage("./List");
    }
}