using HideShowPasswordSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spectre.Console;

// ReSharper disable ConvertConstructorToMemberInitializers

namespace HideShowPasswordSample.Pages;
public class IndexModel : PageModel
{

    [BindProperty]
    public required PasswordContainer PasswordContainer { get; set; }


    public void OnGet()
    {
        PasswordContainer = new PasswordContainer()
        {
            Password1 = "MySecretPassword1",
            Password2 = "MySecretPassword2",
            Password3 = "MySecretPassword3"
        };

       
    }

    public void OnPost()
    {
        AnsiConsole.MarkupLine(PasswordContainer.Password1 is not null
            ? $"[yellow]Password 1:[/] {PasswordContainer.Password1}"
            : "[red]Password 1 is null[/]");
        AnsiConsole.MarkupLine(PasswordContainer.Password2 is not null
            ? $"[yellow]Password 2:[/] {PasswordContainer.Password2}"
            : "[red]Password 2 is null[/]");
        AnsiConsole.MarkupLine(PasswordContainer.Password3 is not null
            ? $"[yellow]Password 3:[/] {PasswordContainer.Password3}"
            : "[red]Password 3 is null[/]");

    }
}
