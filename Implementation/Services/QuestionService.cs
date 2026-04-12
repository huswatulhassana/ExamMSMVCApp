using ExamMSAppMVC.Implementation.Repositories;
using ExamMSAppMVC.Interface.Repositories;
using ExamMSAppMVC.Interface.Service;
using ExamMSAppMVC.Models.Entities;
using ExamMSAppMVC.Models.DTOs;
namespace ExamMSAppMVC.Implementation.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepo _questionRepo;

        public QuestionService(IQuestionRepo questionRepo)
        {
            _questionRepo = questionRepo;
        }
        public async Task<BaseResponse<bool>> CreateQuestionAsync(QuestionRequest request)
        {
            // CHANGE THIS LINE from QuestionRequest to Question
            var question = new Question
            {
                CourseId = request.CourseId,
                Text = request.Text,
                OptionA = request.OptionA,
                OptionB = request.OptionB,
                OptionC = request.OptionC,
                OptionD = request.OptionD,
                CorrectOption = request.CorrectOption,
                Marks = request.Marks,
                ExamId = null
            };
            await _questionRepo.AddAsync(question);

            return new BaseResponse<bool> { Status = true, Message = "Question added to bank!" };
        }

        public async Task<BaseResponse<IEnumerable<Question>>> GetAllQuestionsAsync()
        {
            var questions = await _questionRepo.GetAllAsync();
            return new BaseResponse<IEnumerable<Question>>
            {
                Data = questions,
                Status = true
            };
        }
    }
}
