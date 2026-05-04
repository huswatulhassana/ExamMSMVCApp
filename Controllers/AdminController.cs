using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExamMSAppMVC.Interface.Service;
using ExamMSAppMVC.Models.Entities;
using ExamMSAppMVC.Models.DTOs;

namespace ExamMSAppMVC.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController(ICourseService courseService, IExamService examService) : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Courses()
        {
            var response = await courseService.GetAllCoursesAsync();
            var courseList = response.Data.Select(c => new CourseResponse
            {
                Id = c.Id,
                Name = c.Name,
                CourseCode = c.CourseCode,
            }).ToList();

            return View(courseList);
        }

        [HttpGet]
        public IActionResult CreateCourse() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse(CourseRequest request)
        {
            var result = await courseService.CreateCourseAsync(request);
            if (result.Status)
            {
                return RedirectToAction(nameof(Courses));
            }
            ViewBag.Error = result.Message;
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> EditCourse(Guid id)
        {
            var response = await courseService.GetCourseByIdAsync(id);
            if (response.Status)
            {
                return View(response.Data);
            }
            return RedirectToAction(nameof(Courses));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourse(Guid id, CourseRequest request)
        {
            var result = await courseService.UpdateCourseAsync(id, request);
            if (result.Status) return RedirectToAction(nameof(Courses));

            ViewBag.Error = result.Message;
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var result = await courseService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Courses));
        }
        public async Task<IActionResult> AllResults()
{
    var response = await examService.GetAdminResultsAsync();

    if (response == null || response.Data == null)
    {
        return View(new List<ResultDTO>()); 
    }

    return View(response.Data); 
}
    }
}