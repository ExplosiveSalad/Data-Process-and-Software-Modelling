#nullable disable
using Microsoft.AspNetCore.Mvc;
using PATBMS_Web.Data;
using PATBMS_Web.Models;

namespace PATBMS_Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Patient/Register
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            // Only admin can register patients
            if (HttpContext.Session.GetString("UserRole") != "admin")
            {
                ViewBag.Error = "Access denied. Admin staff only.";
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        // POST: /Patient/Register
        [HttpPost]
        public IActionResult Register(string nhiNumber, string name, 
            string dateOfBirth, string address, string phoneNumber)
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            // Business rule - check if NHI already exists
            var existing = _context.Patients
                .FirstOrDefault(p => p.NHINumber == nhiNumber);

            if (existing != null)
            {
                ViewBag.Error = $"Patient with NHI {nhiNumber} already exists.";
                ViewBag.ExistingPatient = existing;
                return View();
            }

            // Create new patient
            var patientID = "P" + DateTime.Now.Ticks.ToString().Substring(10);
            var patient = new Patient(
                patientID, nhiNumber, name, 
                dateOfBirth, address, phoneNumber);

            _context.Patients.Add(patient);
            _context.SaveChanges();

            ViewBag.Success = $"Patient {name} registered successfully with ID {patientID}.";
            return View();
        }

        // GET: /Patient/Search
        public IActionResult Search()
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        // POST: /Patient/Search
        [HttpPost]
        public IActionResult Search(string nhiNumber)
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            var patient = _context.Patients
                .FirstOrDefault(p => p.NHINumber == nhiNumber);

            if (patient == null)
            {
                ViewBag.Error = $"No patient found with NHI number {nhiNumber}.";
                return View();
            }

            ViewBag.Patient = patient;
            return View();
        }

        // GET: /Patient/List
        public IActionResult List()
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            var patients = _context.Patients.ToList();
            return View(patients);
        }
    }
}