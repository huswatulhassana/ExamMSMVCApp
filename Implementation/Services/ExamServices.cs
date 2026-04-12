using ExamMSAppMVC.Interface.Repositories;
using ExamMSAppMVC.Interface.Service;
using ExamMSAppMVC.Models.Entities;
using ExamMSAppMVC.Models.DTOs;
using System.Linq;
using ExamMSAppMVC.Implementation.Repositories;

namespace ExamMSAppMVC.Implementation.Services
{
    public class ExamService : IExamService
    {
        private readonly IQuestionRepo _questionRepo;
        private readonly IResultRepository _resultRepo;
        private readonly IExamRepository _examRepository;

        public ExamService(IQuestionRepo questionRepo, IResultRepository resultRepo, IExamRepository examRepository)
        {
            _questionRepo = questionRepo;
            _resultRepo = resultRepo;
            _examRepository = examRepository;
        }

        public async Task<BaseResponse<bool>> CreateExamAsync(ExamRequest request)
        {
            try
            {
                var exam = new Exam
                {
                    Id = Guid.NewGuid(),
                    Title = request.Title,
                    DurationInMinutes = request.Duration,
                    ScheduledDate = request.Date,
                    CourseId = request.CourseId,
                    // Questions list starts empty
                };

                await _examRepository.AddAsync(exam);

                return new BaseResponse<bool>
                {
                    Status = true,
                    Message = "Exam created successfully!"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Status = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<Exam>> GetExamByIdAsync(Guid id)
        {
            var exam = await _examRepository.GetExamWithQuestionsAsync(id);

            if (exam == null)
            {
                return new BaseResponse<Exam> { Status = false, Message = "Exam not found." };
            }

            return new BaseResponse<Exam> { Data = exam, Status = true };
        }

        public async Task<BaseResponse<IEnumerable<Question>>> GetQuestionsForCourseAsync(Guid courseId)
        {
            // 1. Fetch EVERYTHING from the repository first
            var allQuestions = await _questionRepo.GetAllAsync();

            // 2. Filter the results in memory using LINQ
            var filteredQuestions = allQuestions
                .Where(q => q.CourseId == courseId && q.ExamId == null)
                .ToList();

            return new BaseResponse<IEnumerable<Question>>
            {
                Data = filteredQuestions,
                Status = true
            };
        }
        public async Task<BaseResponse<bool>> AddQuestionsToExamAsync(Guid examId, List<Guid> questionIds)
        {
            try
            {
                foreach (var qId in questionIds)
                {
                    var question = await _questionRepo.GetByIdAsync(qId);
                    if (question != null)
                    {
                        question.ExamId = examId;

                        await _questionRepo.UpdateAsync(question);
                    }
                }
                await _questionRepo.SaveChangesAsync();

                return new BaseResponse<bool> { Status = true, Message = "Questions successfully linked to exam!" };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool> { Status = false, Message = $"Error: {ex.Message}" };
            }
        }
        public async Task<BaseResponse<IEnumerable<Exam>>> GetAllExamsAsync()
        {
            var exams = await _examRepository.GetAllAsync();

            return new BaseResponse<IEnumerable<Exam>>
            {
                Data = exams,
                Status = true,
                Message = "Exams retrieved successfully"
            };
        }

        public async Task<BaseResponse<bool>> UpdateExamAsync(Guid id, ExamRequest request)
        {
            var exam = await _examRepository.GetByIdAsync(id);
            if (exam == null) return new BaseResponse<bool> { Status = false, Message = "Exam not found" };

            exam.Title = request.Title;
            exam.DurationInMinutes = request.Duration;
            exam.ScheduledDate = request.Date;
            exam.CourseId = request.CourseId;

            await _examRepository.UpdateAsync(exam);
            return new BaseResponse<bool> { Status = true, Message = "Exam updated!" };
        }
        public async Task<IEnumerable<Exam>> GetExamsByCourseIdAsync(Guid courseId)
        {
            return await _examRepository.GetExamsByCourseIdAsync(courseId);
        }
        public async Task<BaseResponse<Result>> CalculateAndSaveResultAsync(Guid examId, Guid studentId, Dictionary<Guid, string> answers)
        {
            var exam = await _examRepository.GetExamWithQuestionsAsync(examId);

            if (exam == null)
            {
                return new BaseResponse<Result> { Status = false, Message = "Exam not found." };
            }

            double score = 0;
            int correctCount = 0; // New counter
            int wrongCount = 0;   // New counter

            // Calculate total possible marks
            double totalPossibleMarks = exam.Questions.Sum(x => x.Marks);

            // 3. Grading Logic
            foreach (var q in exam.Questions)
            {
                if (answers != null && answers.TryGetValue(q.Id, out string selectedOption))
                {
                    // Case-insensitive comparison
                    if (selectedOption.Trim().Equals(q.CorrectOption.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        score += q.Marks;
                        correctCount++; // Increment correct
                    }
                    else
                    {
                        wrongCount++; // Increment wrong (if answered but incorrect)
                    }
                }
                else
                {
                    // If the question wasn't even in the answers dictionary (skipped)
                    wrongCount++;
                }
            }

            // 4. Create Result Entity
            var result = new Result
            {
                Id = Guid.NewGuid(),
                ExamId = examId,
                StudentId = studentId,
                TotalScore = score,
                CorrectAnswers = correctCount, // Map to your Entity property
                WrongAnswers = wrongCount,     // Map to your Entity property
                Percentage = (totalPossibleMarks > 0) ? Math.Round((score / totalPossibleMarks) * 100, 2) : 0,
                DateTaken = DateTime.Now
            };

            // 5. Save to Database
            try
            {
                await _resultRepo.AddAsync(result);
                await _resultRepo.SaveChangesAsync();

                return new BaseResponse<Result>
                {
                    Status = true,
                    Message = "Exam submitted successfully!",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Result> { Status = false, Message = "Failed to save results." };
            }
        }
        public async Task<BaseResponse<Result>> GetResultByIdAsync(Guid id)
        {
            var result = await _resultRepo.GetByIdAsync(id);

            if (result == null)
            {
                return new BaseResponse<Result>
                {
                    Status = false,
                    Message = "Result not found."
                };
            }

            return new BaseResponse<Result>
            {
                Status = true,
                Data = result
            };
        }

    }
}

