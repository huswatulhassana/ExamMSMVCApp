namespace ExamMSAppMVC.Models.DTOs
{
    public class QuestionRequest
    {
        public Guid CourseId { get; set; }
        public required string Text { get; set; }
        public required string OptionA { get; set; }
        public required string OptionB { get; set; }
        public required string OptionC { get; set; }
        public required string OptionD { get; set; }
        public required string CorrectOption { get; set; }
        public int Marks { get; set; }
    }
}