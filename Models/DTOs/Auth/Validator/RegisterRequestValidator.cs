using FluentValidation;
using ExamMSAppMVC.Models.DTOs.Auth;

namespace ExamMSAppMVC.Models.DTOs.Auth.Validator
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Name is required.");


            RuleFor(x => x.RegistrationNumber)
            .NotEmpty().WithMessage("Reg Number is required.");


            RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid email format.")
            .Must(email => string.IsNullOrEmpty(email) || email == email.Trim())
            .WithMessage("Email cannot contain leading or trailing whitespace.");

            RuleFor(x => x.Password)
            .MinimumLength(6).WithMessage("Password must be 6+ chars.");

            RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match.");
        }
    }
}
