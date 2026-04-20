using ExamMSAppMVC.Models.Entities;
using ExamMSAppMVC.Models.DTOs;

namespace ExamMSAppMVC.Interface.Service
{
    public interface IQuestionService
    {
        Task<BaseResponse<bool>> CreateQuestionsAsync(List<QuestionRequest> requests);
        Task<BaseResponse<IEnumerable<Question>>> GetAllQuestionsAsync();
    }
}
