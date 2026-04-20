using ExamMSAppMVC.Contracts;
using ExamMSAppMVC.Interface.Repositories;
using ExamMSAppMVC.Interface.Service;
using ExamMSAppMVC.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExamMSAppMVC.Models.DTOs;

namespace ExamMSAppMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuestionController : Controller
    {


        private readonly IQuestionService _questionService;
        private readonly ICourseService _courseService;

        public QuestionController(IQuestionService questionService, ICourseService courseService)
        {
            _questionService = questionService;
            _courseService = courseService;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _questionService.GetAllQuestionsAsync();
            var questions = response.Data.OrderBy(q => q.CourseId).ToList();

            return View(questions);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            ViewBag.Courses = courses.Data;
            return View();
        }
        [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(List<QuestionRequest> requests)
    {
        if (requests == null || !requests.Any()) return View();

        var result = await _questionService.CreateQuestionsAsync(requests);
        
        if (result.Status) return RedirectToAction(nameof(Index));

        return View(requests);
    }
                  
       public async Task<IActionResult> Edit(Guid id)
    {
        var response = await _questionService.GetAllQuestionsAsync();

        var question = response.Data.FirstOrDefault(q => q.Id == id);

        if (question == null) return NotFound();
        
        return View(question);
    }


[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(Guid id, Question question)
{
    if (id != question.Id) return NotFound();
    
    if (ModelState.IsValid)
    {
        return RedirectToAction(nameof(Index));
    }
    return View(question);
}
}
    }

