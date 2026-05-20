#nullable disable
using Microsoft.AspNetCore.Mvc;
using PATBMS_Web.Data;
using PATBMS_Web.Models;

namespace PATBMS_Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Find user by email and password
            var user = _context.Users.FirstOrDefault(u => 
                u.Email == email && u.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }

            // Store user info in session
            HttpContext.Session.SetString("UserID", user.UserID);
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("UserRole", user.Role);

            // Use UserFactory to create correct user type
            var factory = new UserFactory();
            user.Login();

            // Redirect based on role
            switch (user.Role.ToLower())
            {
                case "doctor":
                    return RedirectToAction("Index", "Dashboard");
                case "nurse":
                    return RedirectToAction("Index", "Dashboard");
                case "admin":
                    return RedirectToAction("Index", "Dashboard");
                case "allied":
                    return RedirectToAction("Index", "Dashboard");
                case "management":
                    return RedirectToAction("Index", "Dashboard");
                default:
                    return RedirectToAction("Index", "Dashboard");
            }
        }

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}