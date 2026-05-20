#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PATBMS_Web.Data;
using PATBMS_Web.Models;

namespace PATBMS_Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            // Get stats for dashboard
            var totalPatients = _context.Patients.Count();
            var totalBeds = _context.Beds.Count();
            var availableBeds = _context.Beds.Count(b => b.Status == "Available");
            var occupiedBeds = _context.Beds.Count(b => b.Status == "Occupied");
            var totalWards = _context.Wards.Count();
            var recentPatients = _context.Patients.Take(5).ToList();
            var wards = _context.Wards.ToList();

            // Pass data to view
            ViewBag.TotalPatients = totalPatients;
            ViewBag.TotalBeds = totalBeds;
            ViewBag.AvailableBeds = availableBeds;
            ViewBag.OccupiedBeds = occupiedBeds;
            ViewBag.TotalWards = totalWards;
            ViewBag.OccupancyRate = totalBeds > 0 
                ? Math.Round((float)occupiedBeds / totalBeds * 100, 1) 
                : 0;
            ViewBag.RecentPatients = recentPatients;
            ViewBag.Wards = wards;
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");

            return View();
        }
    }
}