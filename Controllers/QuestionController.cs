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
        public async Task<IActionResult> Create(QuestionRequest request)
        {
            var result = await _questionService.CreateQuestionAsync(request);
            if (result.Status) return RedirectToAction("Index", "Admin");

            return View(request);
        }
    }
}
