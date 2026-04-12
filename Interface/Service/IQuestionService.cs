using ExamMSAppMVC.Models.Entities;
using ExamMSAppMVC.Models.DTOs;

namespace ExamMSAppMVC.Interface.Service
{
    public interface IQuestionService
    {
        public Task<BaseResponse<bool>> CreateQuestionAsync(QuestionRequest question);
        Task<BaseResponse<IEnumerable<Question>>> GetAllQuestionsAsync();
    }
}
