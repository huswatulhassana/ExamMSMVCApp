using ExamMSAppMVC.Models.Entities;

namespace ExamMSAppMVC.Interface.Repositories
{
    // Inherits all Base methods + adds GetByExamId
    public interface IQuestionRepo : IBaseRepository<Question>
    {
        Task<IEnumerable<Question>> GetQuestionsByExamIdAsync(Guid examId);
    }
}