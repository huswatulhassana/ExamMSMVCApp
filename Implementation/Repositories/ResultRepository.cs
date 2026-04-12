using ExamMSAppMVC.Contracts.Entities;
using ExamMSAppMVC.Models.Entities;
using ExamMSAppMVC.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using ExamMSAppMVC.EMSDBcontext;

namespace ExamMSAppMVC.Implementation.Repositories
{
    public class ResultRepository : BaseRepository<Result>, IResultRepository
    {
        public ResultRepository(EMSDbContext context) : base(context) { }

        public async Task<IEnumerable<Result>> GetAllResultsWithDetailsAsync()
        {
            return await _context.Results
                .Include(r => r.Exam)
                .Include(r => r.Student)
                .ToListAsync();
        }

        public async Task<IEnumerable<Result>> GetResultsByStudentIdAsync(Guid studentId)
        {
            return await _context.Results
                .Include(r => r.Exam)
                .Where(r => r.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Result>> GetResultsByStudentAsync(Guid studentId)
        {
            return await _context.Results
                .Include(r => r.Exam)
                .Where(r => r.StudentId == studentId)
                .OrderByDescending(r => r.DateTaken)
                .ToListAsync();
        }
    }
}
