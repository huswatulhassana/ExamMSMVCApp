using ExamMSAppMVC.EMSDBcontext;
using ExamMSAppMVC.Interface.Repositories;
using ExamMSAppMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamMSAppMVC.Implementation.Repositories
{

    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {

        public CourseRepository(EMSDbContext context) : base(context)
        {
        }

        public async Task<Course?> GetByCodeAsync(string courseCode)
        {
            return await _context.Courses
                .FirstOrDefaultAsync(c => c.CourseCode == courseCode);
        }

        public async Task<IEnumerable<Course>> GetAllWithExamsAsync()
        {
            return await _context.Courses
                .Include(c => c.Exams)
                .ToListAsync();
        }
    }
}