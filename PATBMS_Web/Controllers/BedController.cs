#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PATBMS_Web.Data;
using PATBMS_Web.Models;

namespace PATBMS_Web.Controllers
{
    public class BedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BedController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Bed/Dashboard
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            var wards = _context.Wards.ToList();
            var beds = _context.Beds.ToList();

            ViewBag.Wards = wards;
            ViewBag.Beds = beds;
            ViewBag.TotalBeds = beds.Count;
            ViewBag.AvailableBeds = beds.Count(b => b.Status == "Available");
            ViewBag.OccupiedBeds = beds.Count(b => b.Status == "Occupied");
            ViewBag.AllocatedBeds = beds.Count(b => b.Status == "Allocated");

            return View();
        }

        // GET: /Bed/Allocate
        public IActionResult Allocate()
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            var role = HttpContext.Session.GetString("UserRole");
            if (role != "doctor" && role != "nurse")
                return RedirectToAction("Index", "Dashboard");

            var patients = _context.Patients
                .Where(p => p.AdmissionStatus == "Triaged")
                .ToList();
            var wards = _context.Wards.ToList();

            ViewBag.Patients = patients;
            ViewBag.Wards = wards;

            return View();
        }

        // POST: /Bed/Allocate
        [HttpPost]
        public IActionResult Allocate(string patientID, int atsCategory)
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            var patient = _context.Patients.Find(patientID);
            if (patient == null)
            {
                TempData["Error"] = "Patient not found.";
                return RedirectToAction("Allocate");
            }

            // Business rule - validate ATS
            if (atsCategory < 1 || atsCategory > 5)
            {
                TempData["Error"] = "Invalid ATS category.";
                return RedirectToAction("Allocate");
            }

            // Get all beds directly from database
            List<Bed> availableBeds;

            if (atsCategory <= 2)
            {
                // Urgent - try high dependency first then any available
                availableBeds = _context.Beds
                    .Where(b => b.Status == "Available" && b.BedType == "High Dependency")
                    .ToList();

                if (!availableBeds.Any())
                {
                    availableBeds = _context.Beds
                        .Where(b => b.Status == "Available")
                        .ToList();
                }

                TempData["StrategyUsed"] = "Urgent Allocation Strategy (ATS 1-2)";
            }
            else
            {
                // Standard - find any available standard bed
                availableBeds = _context.Beds
                    .Where(b => b.Status == "Available" && b.BedType == "Standard")
                    .ToList();

                if (!availableBeds.Any())
                {
                    availableBeds = _context.Beds
                        .Where(b => b.Status == "Available")
                        .ToList();
                }

                TempData["StrategyUsed"] = "Standard Allocation Strategy (ATS 3-5)";
            }

            if (!availableBeds.Any())
            {
                TempData["Error"] = "No suitable beds available. Please request transfer.";
                return RedirectToAction("Allocate");
            }

            // Take the first available bed
            var allocatedBed = availableBeds.First();
            allocatedBed.Status = "Occupied";
            patient.BedID = allocatedBed.BedID;

            // Save allocation record
            var allocationID = "BA" + DateTime.Now.Ticks.ToString().Substring(10);
            var allocation = new BedAllocation(
                allocationID,
                DateTime.Now.ToString("dd/MM/yyyy"),
                HttpContext.Session.GetString("UserName"));

            _context.BedAllocations.Add(allocation);

            // Update patient status
            patient.AdmissionStatus = "Admitted";
            _context.SaveChanges();

            // Notify observers via BedManager
            var bedManager = BedManager.GetInstance();
            bedManager.NotifyObservers(
                $"Bed {allocatedBed.BedID} allocated to {patient.Name}");
            bedManager.CheckOccupancyThreshold("W001");

            // Save notification to database
            NotificationController.CreateNotification(
                _context,
                $"Bed {allocatedBed.BedID} allocated to {patient.Name} " +
                $"using {TempData["StrategyUsed"]}");

            TempData["Success"] =
                $"Bed {allocatedBed.BedID} allocated to {patient.Name} " +
                $"using {TempData["StrategyUsed"]}";

            return RedirectToAction("Dashboard");
        }
    }
}