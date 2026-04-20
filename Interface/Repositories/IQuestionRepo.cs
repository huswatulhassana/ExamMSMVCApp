using ExamMSAppMVC.Models.Entities;

namespace ExamMSAppMVC.Interface.Repositories
{
    // Inherits all Base methods + adds GetByExamId
    public interface IQuestionRepo : IBaseRepository<Question>
    {
        Task AddRangeAsync(List<Question> questionsToSave);
        Task<IEnumerable<Question>> GetQuestionsByExamIdAsync(Guid examId);
        
    }
}