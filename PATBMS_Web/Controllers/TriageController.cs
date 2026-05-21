#nullable disable
using Microsoft.AspNetCore.Mvc;
using PATBMS_Web.Data;
using PATBMS_Web.Models;

namespace PATBMS_Web.Controllers
{
    public class TriageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TriageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Triage/Index
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            var role = HttpContext.Session.GetString("UserRole");
            if (role != "nurse" && role != "doctor")
            {
                return RedirectToAction("Index", "Dashboard");
            }

            var patients = _context.Patients.ToList();
            return View(patients);
        }

        // POST: /Triage/Perform
        [HttpPost]
        public IActionResult Perform(string patientID, string symptoms, 
            string vitals, int atsCategory)
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            // Business rule - ATS must be between 1 and 5
            if (atsCategory < 1 || atsCategory > 5)
            {
                TempData["Error"] = "Invalid ATS Category. Must be between 1 and 5.";
                return RedirectToAction("Index");
            }

            var patient = _context.Patients.Find(patientID);
            if (patient == null)
            {
                TempData["Error"] = "Patient not found.";
                return RedirectToAction("Index");
            }

            // Create triage assessment
            var triageID = "T" + DateTime.Now.Ticks.ToString().Substring(10);
            var triage = new TriageAssessment(
                triageID,
                atsCategory,
                symptoms,
                vitals,
                DateTime.Now.ToString("dd/MM/yyyy"));

            _context.TriageAssessments.Add(triage);

            // Update patient admission status
            patient.AdmissionStatus = "Triaged";
            _context.SaveChanges();

            // Notify observers via BedManager
            var bedManager = BedManager.GetInstance();
            bedManager.NotifyObservers(
                $"Patient {patient.Name} triaged with ATS Category {atsCategory}");

            TempData["Success"] = 
                $"Triage completed for {patient.Name}. ATS Category: {atsCategory}";
            return RedirectToAction("Index");
        }

        // GET: /Triage/Review
        public IActionResult Review()
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            var role = HttpContext.Session.GetString("UserRole");
            if (role != "doctor")
                return RedirectToAction("Index", "Dashboard");

            var triages = _context.TriageAssessments.ToList();
            return View(triages);
        }
    }
}