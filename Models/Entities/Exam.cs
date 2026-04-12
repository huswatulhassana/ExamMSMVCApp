using ExamMSAppMVC.Contracts.Entities;
namespace ExamMSAppMVC.Models.Entities
{
    public class Exam : BaseEntity
    {
        public required string Title { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime ScheduledDate { get; set; }

        public Guid CourseId { get; set; }
        public Course? Course { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}