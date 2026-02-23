using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FluentWebApplication.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

public static class Extensions
{
    public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState, string prefix)
    {

        if (result.IsValid) return;

        foreach (var error in result.Errors)
        {
            var key = string.IsNullOrEmpty(prefix)
                ? error.PropertyName
                : string.IsNullOrEmpty(error.PropertyName)
                    ? prefix
                    : $"{prefix}.{error.PropertyName}";

            modelState.AddModelError(key, error.ErrorMessage);
        }
    }
}