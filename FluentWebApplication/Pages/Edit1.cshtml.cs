using FluentValidation;
using FluentWebApplication.Data;
using FluentWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using FluentWebApplication.Classes;

namespace FluentWebApplication.Pages
{
    public class Edit1Model(Context context, IValidator<Person> validator) : PageModel
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


        public async Task<IActionResult> OnPostAsync()
        {
            ValidationResult result = await validator.ValidateAsync(Person);
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

        private bool PersonExists(int id) => context.Person.Any(e => e.PersonId == id);
    }
}
