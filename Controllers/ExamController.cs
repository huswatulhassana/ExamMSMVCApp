using ExamMSAppMVC.Contracts;
using ExamMSAppMVC.Interface.Repositories;
using ExamMSAppMVC.Interface.Service;
using ExamMSAppMVC.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExamMSAppMVC.Models.DTOs;
using System.Security.Claims;

namespace ExamMSAppMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ExamController(IExamService examService, ICourseService courseService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = await examService.GetAllExamsAsync();
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var courses = await courseService.GetAllCoursesAsync();
            ViewBag.Courses = courses.Data;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExamRequest request)
        {
            // THIS is where we use CreateExamAsync
            var result = await examService.CreateExamAsync(request);
            if (result.Status) return RedirectToAction(nameof(Index));

            var courses = await courseService.GetAllCoursesAsync();
            ViewBag.Courses = courses.Data;
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> ManageQuestions(Guid id)
        {
            var exam = await examService.GetExamByIdAsync(id);
            var allQuestions = await examService.GetQuestionsForCourseAsync(exam.Data.CourseId);

            ViewBag.ExamId = id;
            ViewBag.ExamTitle = exam.Data.Title;
            return View(allQuestions.Data);
        }

        [HttpPost]
        public async Task<IActionResult> SaveSelectedQuestions(Guid examId, List<Guid> selectedQuestions)
        {
            var result = await examService.AddQuestionsToExamAsync(examId, selectedQuestions);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await examService.GetExamByIdAsync(id);
            if (!response.Status) return NotFound();

            var courses = await courseService.GetAllCoursesAsync();
            ViewBag.Courses = courses.Data;

            // Map Entity to DTO for the view
            var request = new ExamRequest
            {
                Title = response.Data.Title,
                Duration = response.Data.DurationInMinutes,
                Date = response.Data.ScheduledDate,
                CourseId = response.Data.CourseId
            };

            ViewData["ExamId"] = id;
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ExamRequest request)
        {
            var result = await examService.UpdateExamAsync(id, request);
            if (result.Status) return RedirectToAction(nameof(Index));

            return View(request);
        }
    }
}


