using FluentWebApplication.Data;
using FluentWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FluentWebApplication.Pages;
public class IndexModel(Context context) : PageModel
{




    public void OnGet()
    {

    }

    private void UpdateEmailAddresses()
    {
        var people = context.Person.ToList();
        foreach (var (index, person) in people.Index())
        {
            person.EmailAddress = index % 2 == 0 ? 
                $"{person.LastName}{person.FirstName}@gmail.com" : 
                $"{person.FirstName}{person.LastName}@yahoo.com";
        }

        var count = context.SaveChanges();
    }
}
