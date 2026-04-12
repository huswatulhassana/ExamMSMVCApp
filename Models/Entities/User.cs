using ExamMSAppMVC.Contracts.Entities;
using ExamMSAppMVC.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace ExamMSAppMVC.Models
{

    public class User : BaseUser
    {
        public required string Email { get; set; }
        public required string HashPassword { get; set; }
        public bool EmailConfirmed { get; set; }
        public Guid RoleId { get; set; }
#pragma warning restore CS8618
        public Role? Role { get; set; }
        public required string RegistrationNumber { get; set; }
        public ICollection<Result> Results { get; set; } = new List<Result>();
        public Student? StudentProfile { get; set; }


        public void ChangePassword(string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentException("Password cannot be empty");
            }

            var hasher = new PasswordHasher<User>();
            this.HashPassword = hasher.HashPassword(this, newPassword);
        }
    }
}
