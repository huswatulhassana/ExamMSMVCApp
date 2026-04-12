using System.ComponentModel.DataAnnotations;

namespace ExamMSAppMVC.Models.DTOs.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Registration Number is required")]
        public string RegistrationNumber { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}