using ExamMSAppMVC.Contracts;
using ExamMSAppMVC.EMSDBcontext;
using ExamMSAppMVC.Interface.Repositories;
using ExamMSAppMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamMSAppMVC.Implementation.Repositories
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepo
    {
        public QuestionRepository(EMSDbContext context) : base(context) { }

        public async Task<IEnumerable<Question>> GetQuestionsByExamIdAsync(Guid examId)
        {
            return await _context.Questions
                .Where(q => q.ExamId == examId)
                .ToListAsync();
        }
    }
}