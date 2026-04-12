using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamMSAppMVC.Models.DTOs.Auth
{
    public class LoginResponds
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = default!;

        public string FullName { get; set; } = default!;

        public string RoleName { get; set; } = default!;

        public string RegistrationNumber { get; set; } = default!;

    }
}