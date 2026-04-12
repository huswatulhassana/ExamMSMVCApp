namespace ExamMSAppMVC.Models.DTOs
{
    public class ExamRequest
    {
        public required string Title { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }
        public Guid CourseId { get; set; }
    }
}