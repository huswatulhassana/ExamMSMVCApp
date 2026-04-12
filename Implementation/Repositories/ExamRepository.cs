using ExamMSAppMVC.Contracts;
using ExamMSAppMVC.Models.Entities;
using ExamMSAppMVC.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using ExamMSAppMVC.EMSDBcontext;
using ExamMSAppMVC.Models.DTOs;

namespace ExamMSAppMVC.Implementation.Repositories
{
    public class ExamRepository : BaseRepository<Exam>, IExamRepository
    {
        public ExamRepository(EMSDbContext context) : base(context) { }

        public async Task<Exam?> GetExamWithQuestionsAsync(Guid id)
        {
            return await _context.Exams
                .Include(e => e.Questions)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<IEnumerable<Exam>> GetAllExamsAsync()
          => await _context.Exams.Include(e => e.Questions).ToListAsync();

        public async Task<IEnumerable<Exam>> GetExamsByCourseIdAsync(Guid courseId)
        {
            return await _context.Exams
              .Include(e => e.Questions)
              .Where(e => e.CourseId == courseId)
              .ToListAsync();
        }
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Students.AnyAsync(s => s.Id == id);
        }
        public async Task<Student?> GetByUserIdAsync(Guid userId)
        {
            return await _context.Students
           .FirstOrDefaultAsync(s => s.UserId == userId);
        }

    }
}


