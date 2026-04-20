using ExamMSAppMVC.Contracts;
using ExamMSAppMVC.EMSDBcontext;
using ExamMSAppMVC.Interface.Repositories;
using ExamMSAppMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamMSAppMVC.Implementation.Repositories
{       public class QuestionRepository : BaseRepository<Question>, IQuestionRepo
{
    private readonly EMSDbContext _context; 
    public QuestionRepository(EMSDbContext context) : base(context) 
    { 
        _context = context;
    }

    public async Task AddRangeAsync(List<Question> questionsToSave)
    {
        await _context.Questions.AddRangeAsync(questionsToSave);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Question>> GetQuestionsByExamIdAsync(Guid examId)
    {
        return await _context.Questions
            .Where(q => q.ExamId == examId)
            .ToListAsync();
    }
}
    }
