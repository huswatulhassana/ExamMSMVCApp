using FluentValidation;

namespace ExamMSAppMVC.Models.DTOs.Auth.Validator
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.RegistrationNumber)
                .NotEmpty().WithMessage("Registration Number is required.")
                .MinimumLength(5).WithMessage("Invalid Registration Number format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}

