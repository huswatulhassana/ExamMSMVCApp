using ExamMSAppMVC.Contracts.Entities;

namespace ExamMSAppMVC.Models.Entities
{
    public class Result : BaseEntity
    {
        public Exam? Exam { get; set; }
        public Guid ExamId { get; set; }
        public double TotalScore { get; set; }
        public DateTime DateTaken { get; set; } = DateTime.Now;
        public double Percentage { get; set; }
        public Guid StudentId { get; set; }
        public Student? Student { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public int TotalQuestions { get; set; }


    }
}
