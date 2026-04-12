using System;

namespace ExamMSAppMVC.Contracts.Entities
{
    public class BaseUser : BaseEntity
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }

    }
}
