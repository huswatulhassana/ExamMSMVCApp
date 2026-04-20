using ExamMSAppMVC.Models.Entities;
using ExamMSAppMVC.Models.DTOs;

namespace ExamMSAppMVC.Interface.Service
{
    public interface IExamService
    {
        Task<BaseResponse<bool>> CreateExamAsync(ExamRequest request);
        Task<BaseResponse<Exam>> GetExamByIdAsync(Guid id);
        Task<BaseResponse<IEnumerable<Question>>> GetQuestionsForCourseAsync(Guid courseId);
        Task<BaseResponse<bool>> AddQuestionsToExamAsync(Guid examId, List<Guid> questionIds);
        Task<BaseResponse<IEnumerable<Exam>>> GetAllExamsAsync();
        Task<BaseResponse<bool>> UpdateExamAsync(Guid id, ExamRequest request);
        Task<IEnumerable<Exam>> GetExamsByCourseIdAsync(Guid courseId);
        Task<BaseResponse<Result>> CalculateAndSaveResultAsync(Guid examId, Guid studentId, Dictionary<Guid, string> answers);
        Task<BaseResponse<Result>> GetResultByIdAsync(Guid id);
        Task<BaseResponse<IEnumerable<ResultDTO>>> GetAdminResultsAsync();

    }
}
