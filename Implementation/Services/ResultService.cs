using ExamMSAppMVC.Contracts;
using ExamMSAppMVC.Interface.Repositories;
using ExamMSAppMVC.Interface.Service;
using ExamMSAppMVC.Models.Entities;

namespace ExamMSAppMVC.Implementation.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepo;

        public ResultService(IResultRepository resultRepo)
        {
            _resultRepo = resultRepo;
        }

        public async Task<IEnumerable<Result>> GetStudentHistoryAsync(Guid studentId)
        {
            return await _resultRepo.GetResultsByStudentAsync(studentId);
        }

        public async Task<IEnumerable<Result>> GetExamStatisticsAsync(Guid examId)
        {
            var allResults = await _resultRepo.GetAllAsync();
            return allResults.Where(r => r.ExamId == examId);
        }

        public async Task<Result?> GetResultDetailsAsync(Guid resultId)
        {
            return await _resultRepo.GetByIdAsync(resultId);
        }
    }
}