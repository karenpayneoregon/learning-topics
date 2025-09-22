#nullable disable
using FluentValidation;
using TablesVsDivsSample1App.Models;

namespace TablesVsDivsSample1App.Validators;

/// <summary>
/// Provides validation rules for the <see cref="Customers"/> model.
/// </summary>
public class CustomersValidator : AbstractValidator<Customers>
{
    public CustomersValidator()
    {
        RuleFor(x => x.Company).NotEmpty();
        RuleFor(x => x.ContactType).NotEmpty();
        RuleFor(x => x.ContactName).NotEmpty();
        RuleFor(x => x.Country).NotEmpty();
        RuleFor(x => x.JoinDate)
            .GreaterThanOrEqualTo(new DateOnly(1900, 1, 1))
            .WithMessage("Join Date must be on or after 01/01/1900.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage($"Join Date must be on or before {DateOnly.FromDateTime(DateTime.Today):MM/dd/yyyy}.");

    }
}