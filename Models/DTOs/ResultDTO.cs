namespace ExamMSAppMVC.Models.DTOs
{
    public class ResultDTO
    {
        public string RegistrationNumber { get; set; }
        public string StudentName { get; set; }
        public string ExamTitle { get; set; }
        public double Score { get; set; }
        public DateTime DateTaken { get; set; }
        public string Status { get; set; }
    }
}