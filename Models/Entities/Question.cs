using System.ComponentModel.DataAnnotations.Schema;
using ExamMSAppMVC.Contracts.Entities;
namespace ExamMSAppMVC.Models.Entities
{
    public class Question : BaseEntity // Or whatever your base class is
    {
        // Link to the Course
        public Guid CourseId { get; set; }

        // Navigation Property (Optional but recommended)
        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public string Text { get; set; } = string.Empty;
        public string OptionA { get; set; } = string.Empty;
        public string OptionB { get; set; } = string.Empty;
        public string OptionC { get; set; } = string.Empty;
        public string OptionD { get; set; } = string.Empty;
        public string CorrectOption { get; set; } = string.Empty;
        public int Marks { get; set; }
        public Exam Exam { get; set; }
        public Guid? ExamId { get; set; }
    }
}