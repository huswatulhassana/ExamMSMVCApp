using System.ComponentModel.DataAnnotations;

namespace ExamMSAppMVC.Models.DTOs.Auth
{
    public class RegisterRequest
    {
        internal string FirstName;
        internal string LastName;


        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Full Name must be more than 5 characters long")]

        [RegularExpression(@"^[a-zA-Z]+\s+[a-zA-Z]+.*$", ErrorMessage = "Please enter both First and Last name (separated by a space)")]
        public required string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Registration Number is required")]
        [RegularExpression(@"^EMS/\d{4}/\d{3}$", ErrorMessage = "Format must be EMS/Year/Number (e.g., EMS/2026/001)")]
        public required string RegistrationNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", 
            ErrorMessage = "Password must have uppercase, lowercase, a number, and a special character")]
        public required string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public required string ConfirmPassword { get; set; }
    }
}
// namespace ExamMSAppMVC.Models.DTOs.Auth
// {
//     public class RegisterRequest
//     {
//         internal string FirstName;
//         internal string LastName;
//         public required string FullName { get; set; }
//         public required string RegistrationNumber { get; set; }
//         public required string Email { get; set; }
//         public required string Password { get; set; }
//         public required string ConfirmPassword { get; set; }
//     }
// }
