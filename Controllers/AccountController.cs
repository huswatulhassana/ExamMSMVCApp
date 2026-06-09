using ExamMSAppMVC.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using ExamMSAppMVC.Interface.Service;
using ExamMSAppMVC.Models.DTOs.Auth;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ExamMSAppMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid) return View(request);

            var response = await _userService.LoginAsync(request);

            if (response.Status && response.Data != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, response.Data.Id.ToString()),
                    new Claim(ClaimTypes.Email, response.Data.Email),
                    new Claim(ClaimTypes.Name, response.Data.FullName),
                    new Claim(ClaimTypes.Role, response.Data.RoleName)
                };

                // Use the default scheme name consistently
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                TempData["Message"] = response.Message;

                if (response.Data.RoleName.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (response.Data.RoleName.Equals("Student", StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("Index", "Student");
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return View(request);
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid) return View(request);

            var response = await _userService.RegisterMemberAsync(request);

            if (response.Status)
            {
                TempData["Message"] = "Registration Successful! Please login.";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", response.Message);
            return View(request);
        }

        public async Task<IActionResult> Logout()
        {
            // Must match the Login scheme name
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}