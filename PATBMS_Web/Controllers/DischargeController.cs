#nullable disable
using Microsoft.AspNetCore.Mvc;
using PATBMS_Web.Data;
using PATBMS_Web.Models;

namespace PATBMS_Web.Controllers
{
    public class DischargeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DischargeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Discharge/Index
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            var role = HttpContext.Session.GetString("UserRole");
            if (role != "nurse" && role != "doctor")
                return RedirectToAction("Index", "Dashboard");

            var patients = _context.Patients
                .Where(p => p.AdmissionStatus == "Admitted")
                .ToList();

            return View(patients);
        }

        // POST: /Discharge/Record
        [HttpPost]
        public IActionResult Record(string patientID, string dischargeReason)
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            var patient = _context.Patients.Find(patientID);
            if (patient == null)
            {
                TempData["Error"] = "Patient not found.";
                return RedirectToAction("Index");
            }

            // Create discharge record
            var dischargeID = "DIS" + DateTime.Now.Ticks.ToString().Substring(10);
            var discharge = new Discharge(
                dischargeID,
                DateTime.Now.ToString("dd/MM/yyyy"),
                dischargeReason);

            _context.Discharges.Add(discharge);

            // Find the patient's bed and mark as available
            if (patient.BedID != null)
            {
                var bed = _context.Beds.Find(patient.BedID);
                if (bed != null)
                {
                    bed.Status = "Available";
                }
                patient.BedID = null;
            }

            // Update patient status
            patient.AdmissionStatus = "Discharged";

            // Notify observers
            var bedManager = BedManager.GetInstance();
            bedManager.NotifyObservers(
                $"Patient {patient.Name} discharged. Bed now available.");

            NotificationController.CreateNotification(
            _context,
            $"Patient {patient.Name} discharged. Bed now available.");

            _context.SaveChanges();

            TempData["Success"] =
                $"Patient {patient.Name} discharged successfully. Bed marked as available.";
            return RedirectToAction("Index");
        }

        // GET: /Discharge/Confirm
        public IActionResult Confirm()
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            var patients = _context.Patients
                .Where(p => p.AdmissionStatus == "Admitted")
                .ToList();

            return View(patients);
        }

        // POST: /Discharge/ConfirmReadiness
        [HttpPost]
        public IActionResult ConfirmReadiness(string patientID)
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            var patient = _context.Patients.Find(patientID);
            if (patient == null)
            {
                TempData["Error"] = "Patient not found.";
                return RedirectToAction("Confirm");
            }

            patient.AdmissionStatus = "Pending Discharge";
            _context.SaveChanges();

            TempData["Success"] =
                $"Discharge readiness confirmed for {patient.Name}.";
            return RedirectToAction("Confirm");
        }
    }
}