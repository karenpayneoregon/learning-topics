#nullable disable
using FluentValidation;

namespace FluentWebApplication.Models;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithName("First name").WithMessage("First name is required");
        RuleFor(x => x.LastName).NotEmpty().WithName("Last name").WithMessage("Last name is required");
        RuleFor(x => x.EmailAddress).NotEmpty().WithName("Email").WithMessage("Email is required").EmailAddress();
    }
}