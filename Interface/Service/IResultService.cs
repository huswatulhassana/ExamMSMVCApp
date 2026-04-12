using ExamMSAppMVC.Models.Entities;

namespace ExamMSAppMVC.Interface.Service
{
    public interface IResultService
    {
        Task<IEnumerable<Result>> GetStudentHistoryAsync(Guid studentId);
        Task<IEnumerable<Result>> GetExamStatisticsAsync(Guid examId);
        Task<Result?> GetResultDetailsAsync(Guid resultId);
    }
}