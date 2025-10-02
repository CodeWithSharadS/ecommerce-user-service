using eCommerce.Core.DTO;
using FluentValidation;

namespace eCommerce.Core.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(t => t.Email)
            .NotEmpty().WithMessage("Email is required!")
            .EmailAddress().WithMessage("Invalid email!");

        RuleFor(t => t.Password)
            .NotEmpty().WithMessage("Password is required!");

        RuleFor(t => t.PersonName)
            .NotEmpty().WithMessage("Person name is required!")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters long!")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters!")
            .Matches("^[a-zA-Z ]+$").WithMessage("Name can only contain letters and spaces!");

        RuleFor(t => t.Gender)
            .IsInEnum().WithMessage("Please select a valid gender!");
    }
}
