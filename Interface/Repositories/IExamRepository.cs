using ExamMSAppMVC.Models.DTOs;
using ExamMSAppMVC.Models.Entities;

namespace ExamMSAppMVC.Interface.Repositories
{
    public interface IExamRepository : IBaseRepository<Exam>
    {
        Task<Exam?> GetExamWithQuestionsAsync(Guid id);
        Task<IEnumerable<Exam>> GetExamsByCourseIdAsync(Guid courseId);

        Task<bool> ExistsAsync(Guid id);
    }
}

