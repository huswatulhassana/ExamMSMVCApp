using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ExamMSAppMVC.Interface.Service;
using ExamMSAppMVC.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExamMSAppMVC.Models.DTOs;
using ExamMSAppMVC.Implementation.Services;

namespace ExamMSAppMVC.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly IExamService _examService;
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;

        public StudentController(IExamService examService, ICourseService courseService, IUserService userService)
        {
            _examService = examService;
            _courseService = courseService;
            _userService = userService;
        }

        // 1. Dashboard: Filter Exams by Course
        public async Task<IActionResult> Index(Guid? courseId)
        {
            var courseResponse = await _courseService.GetAllCoursesAsync();

            if (courseResponse != null && courseResponse.Status)
            {
                ViewBag.Courses = new SelectList(courseResponse.Data, "Id", "Name");
            }
            else
            {
                ViewBag.Courses = new SelectList(new List<CourseResponse>(), "Id", "Name");
            }

            // Fetch exams based on selected course
            if (courseId.HasValue && courseId != Guid.Empty)
            {
                var exams = await _examService.GetExamsByCourseIdAsync(courseId.Value);
                return View(exams);
            }

            return View(new List<Exam>());
        }

        // 2. Take Exam: Fetch the exam data to display questions
        [HttpGet]
        public async Task<IActionResult> TakeExam(Guid id)
        {
            var response = await _examService.GetExamByIdAsync(id);

            // 1. Check if the "Box" (Response) is successful
            if (response == null || !response.Status)
            {
                TempData["Error"] = response?.Message ?? "Exam not found.";
                return RedirectToAction(nameof(Index));
            }

            // 2. Access the "Item" (.Data) inside the box
            var exam = response.Data;

            // 3. Now you can access .Questions safely
            if (exam.Questions == null || !exam.Questions.Any())
            {
                TempData["Error"] = "This exam has no questions.";
                return RedirectToAction(nameof(Index));
            }

            return View(exam);
        }


        [HttpPost]
        public async Task<IActionResult> SubmitExam(Guid examId, Dictionary<Guid, string> answers)
        {
            // 1. Get Logged-in User ID
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid userId)) return RedirectToAction("Login", "Account");

            // 2. Find the Student Profile linked to this User
            var studentResponse = await _userService.GetStudentByUserIdAsync(userId);

            if (studentResponse == null || !studentResponse.Status)
            {
                // This is why you are ending up back at the Dashboard!
                TempData["Error"] = "Student profile not found. Please ensure you are registered as a student.";
                return RedirectToAction(nameof(Index));
            }

            // 3. Use the actual StudentId for the results table
            var response = await _examService.CalculateAndSaveResultAsync(examId, studentResponse.Data.Id, answers);

            if (response != null && response.Status)
            {
                // 4. Redirect to Results with the Result ID
                return RedirectToAction(nameof(Results), new { id = response.Data.Id });
            }

            TempData["Error"] = response?.Message ?? "Submission failed.";
            return RedirectToAction(nameof(Index));
        }        // 4. Results View
        [HttpGet]
        public async Task<IActionResult> Results(Guid id)
        {
            var response = await _examService.GetResultByIdAsync(id);

            if (response == null || !response.Status)
            {
                TempData["Error"] = "Result not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }
    }
}