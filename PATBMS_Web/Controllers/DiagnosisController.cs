#nullable disable
using Microsoft.AspNetCore.Mvc;
using PATBMS_Web.Data;
using PATBMS_Web.Models;

namespace PATBMS_Web.Controllers
{
    public class DiagnosisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiagnosisController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            if (HttpContext.Session.GetString("UserRole") != "doctor")
                return RedirectToAction("Index", "Dashboard");

            var patients = _context.Patients
                .Where(p => p.AdmissionStatus == "Admitted" || 
                            p.AdmissionStatus == "Triaged")
                .ToList();

            ViewBag.Patients = patients;
            return View();
        }

        [HttpPost]
        public IActionResult Create(string patientID, string diagnosis, 
            string treatmentPlan)
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            var patient = _context.Patients.Find(patientID);
            if (patient == null)
            {
                TempData["Error"] = "Patient not found.";
                return RedirectToAction("Create");
            }

            var diagnosisID = "D" + DateTime.Now.Ticks.ToString().Substring(10);
            var record = new DiagnosisRecord(
                diagnosisID,
                diagnosis,
                treatmentPlan,
                DateTime.Now.ToString("dd/MM/yyyy"));

            _context.DiagnosisRecords.Add(record);
            _context.SaveChanges();

            // Notify admin via notification
            NotificationController.CreateNotification(
                _context,
                $"Diagnosis for {patient.Name} submitted by " +
                $"{HttpContext.Session.GetString("UserName")} — " +
                $"sent to Admin for processing.");

            TempData["Success"] = 
                $"Diagnosis recorded for {patient.Name} " +
                $"and sent to Administrative Staff.";
            return RedirectToAction("Create");
        }

        public IActionResult List()
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            var records = _context.DiagnosisRecords.ToList();
            return View(records);
        }
    }
}