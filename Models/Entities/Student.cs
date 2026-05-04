using System.ComponentModel.DataAnnotations.Schema;
using ExamMSAppMVC.Contracts.Entities;

namespace ExamMSAppMVC.Models.Entities
{
    public class Student : BaseEntity
    {
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        public required string RegistrationNumber { get; set; }
        public ICollection<Result> Results { get; set; } = new List<Result>();
    }
}


