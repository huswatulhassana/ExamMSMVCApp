using ExamMSAppMVC.Models.Entities;

namespace ExamMSAppMVC.Interface.Repositories
{

    public interface IResultRepository : IBaseRepository<Result>
    {
        Task<IEnumerable<Result>> GetAllResultsWithDetailsAsync();
        Task<IEnumerable<Result>> GetResultsByStudentIdAsync(Guid studentId);
        Task<IEnumerable<Result>> GetResultsByStudentAsync(Guid studentId);
    }
}