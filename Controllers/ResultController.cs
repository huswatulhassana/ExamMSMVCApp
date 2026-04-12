using ExamMSAppMVC.Contracts;
using ExamMSAppMVC.Interface.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExamMSAppMVC.Controllers
{
    public class ResultController : Controller
    {
        private readonly IResultRepository _resultRepo;
        public ResultController(IResultRepository resultRepo)
        {
            _resultRepo = resultRepo;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminIndex()
        {
            var results = await _resultRepo.GetAllResultsWithDetailsAsync();
            return View(results);
        }


        [Authorize(Roles = "Student")]
        public async Task<IActionResult> MyResults()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userIdClaim, out Guid studentId))
            {
                var myResults = await _resultRepo.GetResultsByStudentIdAsync(studentId);
                return View(myResults);
            }
            return BadRequest();
        }
    }
}

