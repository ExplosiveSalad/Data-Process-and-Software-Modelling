#nullable disable
using Microsoft.AspNetCore.Mvc;
using PATBMS_Web.Data;
using PATBMS_Web.Models;

namespace PATBMS_Web.Controllers
{
    public class HandoverController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HandoverController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            var patients = _context.Patients
                .Where(p => p.AdmissionStatus == "Admitted" ||
                            p.AdmissionStatus == "Triaged")
                .ToList();

            var departments = new List<string>
            {
                "ECC", "ADU", "Surgical Unit",
                "Te Henga Ward", "Piha Ward",
                "Titirangi Ward", "Mental Health Unit"
            };

            ViewBag.Patients = patients;
            ViewBag.Departments = departments;
            return View();
        }

        [HttpPost]
        public IActionResult Create(string patientID, string clinicalStatus,
            string outstandingTasks, string specialNeeds,
            string sendingDepartment, string receivingDepartment)
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            var patient = _context.Patients.Find(patientID);
            if (patient == null)
            {
                TempData["Error"] = "Patient not found.";
                return RedirectToAction("Create");
            }

            var handoverID = "H" + DateTime.Now.Ticks.ToString().Substring(10);
            var handover = new HandoverRecord(
                handoverID, clinicalStatus, outstandingTasks,
                specialNeeds, sendingDepartment, receivingDepartment);

            _context.HandoverRecords.Add(handover);
            _context.SaveChanges();

            var bedManager = BedManager.GetInstance();
            bedManager.NotifyObservers(
                $"Handover for {patient.Name} sent from " +
                $"{sendingDepartment} to {receivingDepartment}");

            NotificationController.CreateNotification(
            _context,
            $"Handover for {patient.Name} sent from " +
            $"{sendingDepartment} to {receivingDepartment}");

            TempData["Success"] =
                $"Handover for {patient.Name} sent to {receivingDepartment}.";
            return RedirectToAction("Create");
        }

        public IActionResult List()
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            var handovers = _context.HandoverRecords.ToList();
            return View(handovers);
        }
    }
}