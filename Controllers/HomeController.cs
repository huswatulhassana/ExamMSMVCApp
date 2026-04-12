using Microsoft.AspNetCore.Mvc;

namespace ExamMSAppMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
