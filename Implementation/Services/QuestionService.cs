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
        public async Task<BaseResponse<bool>> CreateQuestionsAsync(List<QuestionRequest> requests)
{
    var questionsToSave = new List<Question>();
    foreach (var request in requests)
    {
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
        
        questionsToSave.Add(question);
    }
    await _questionRepo.AddRangeAsync(questionsToSave);

    return new BaseResponse<bool> { Status = true, Message = $"{questionsToSave.Count} questions added to bank!" };
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
