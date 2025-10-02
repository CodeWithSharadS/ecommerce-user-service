using eCommerce.Core.DTO;
using FluentValidation;

namespace eCommerce.Core;
public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(t => t.Email)
            .NotEmpty().WithMessage("Email is required!")
            .EmailAddress().WithMessage("Invalid email!");

        RuleFor(t => t.Password)
            .NotEmpty().WithMessage("Password is required!");
    }
}
