using ExamMSAppMVC.Models.Entities;

namespace ExamMSAppMVC.Interface.Repositories
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        Task<Course?> GetByCodeAsync(string courseCode);
        Task<IEnumerable<Course>> GetAllWithExamsAsync();
    }
}