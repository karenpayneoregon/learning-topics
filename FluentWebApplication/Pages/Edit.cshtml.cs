using FluentValidation;
using FluentWebApplication.Data;
using FluentWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FluentWebApplication.Pages;

public class EditModel(Context context, IValidator<Person> validator) : PageModel
{
    [BindProperty]
    public Person Person { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || context.Person == null)
        {
            return NotFound();
        }

        var person =  await context.Person.FirstOrDefaultAsync(m => m.PersonId == id);
        if (person == null)
        {
            return NotFound();
        }
        Person = person;
        return Page();
    }

    /// <summary>
    /// Handles the HTTP POST request to update an existing <see cref="Person"/> entity.
    /// </summary>
    /// <returns>
    /// An <see cref="IActionResult"/> indicating the result of the operation:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// Returns the current page if the validation of the <see cref="Person"/> model fails.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// Redirects to the "List" page upon successful update of the <see cref="Person"/> entity.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// Returns a "NotFound" result if the <see cref="Person"/> entity does not exist.
    /// </description>
    /// </item>
    /// </list>
    /// </returns>
    /// <exception cref="DbUpdateConcurrencyException">
    /// Thrown if a concurrency conflict occurs while saving changes to the database.
    /// </exception>
    public async Task<IActionResult> OnPostAsync()
    {
        
        var result = await validator.ValidateAsync(Person);
        
        if (!result.IsValid)
        {
            result.AddToModelState(ModelState, nameof(Person));
            return Page();
        }

        context.Attach(Person).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PersonExists(Person.PersonId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./List");
    }

    private bool PersonExists(int id) 
        => (context.Person?.Any(e => e.PersonId == id)).GetValueOrDefault();
}