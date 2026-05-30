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

    if (atsCategory < 1 || atsCategory > 5)
    {
        TempData["Error"] = "Invalid ATS category.";
        return RedirectToAction("Allocate");
    }

    List<Bed> availableBeds;
    string targetWardID;
    string strategyUsed;

    if (atsCategory <= 2)
    {
        // ATS 1-2 — Emergency ward (Titirangi W003)
        // Try High Dependency in Emergency first
        targetWardID = "W003";
        availableBeds = _context.Beds
            .Where(b => b.Status == "Available" 
                && b.BedType == "High Dependency" 
                && b.WardID == targetWardID)
            .ToList();

        // Fall back to any available bed in Emergency
        if (!availableBeds.Any())
        {
            availableBeds = _context.Beds
                .Where(b => b.Status == "Available" 
                    && b.WardID == targetWardID)
                .ToList();
        }

        // Last resort — any High Dependency bed anywhere
        if (!availableBeds.Any())
        {
            availableBeds = _context.Beds
                .Where(b => b.Status == "Available" 
                    && b.BedType == "High Dependency")
                .ToList();
        }

        strategyUsed = "Urgent Allocation Strategy (ATS 1-2) — Emergency Ward";
    }
    else if (atsCategory <= 4)
    {
        // ATS 3-4 — Surgical ward (Piha W002)
        targetWardID = "W002";
        availableBeds = _context.Beds
            .Where(b => b.Status == "Available" 
                && b.WardID == targetWardID)
            .ToList();

        // Fall back to General Medicine
        if (!availableBeds.Any())
        {
            availableBeds = _context.Beds
                .Where(b => b.Status == "Available" 
                    && b.WardID == "W001")
                .ToList();
        }

        strategyUsed = "Standard Allocation Strategy (ATS 3-4) — Surgical Ward";
    }
    else
    {
        // ATS 5 — General Medicine (Te Henga W001)
        targetWardID = "W001";
        availableBeds = _context.Beds
            .Where(b => b.Status == "Available" 
                && b.WardID == targetWardID)
            .ToList();

        strategyUsed = "Standard Allocation Strategy (ATS 5) — General Medicine Ward";
    }

    if (!availableBeds.Any())
    {
        TempData["Error"] = "No suitable beds available. Please request transfer.";
        return RedirectToAction("Allocate");
    }

    var allocatedBed = availableBeds.First();
    allocatedBed.Status = "Occupied";
    patient.BedID = allocatedBed.BedID;
    patient.AdmissionStatus = "Admitted";

    var allocationID = "BA" + DateTime.Now.Ticks.ToString().Substring(10);
    var allocation = new BedAllocation(
        allocationID,
        DateTime.Now.ToString("dd/MM/yyyy"),
        HttpContext.Session.GetString("UserName"));

    _context.BedAllocations.Add(allocation);
    _context.SaveChanges();

    var bedManager = BedManager.GetInstance();
    bedManager.NotifyObservers(
        $"Bed {allocatedBed.BedID} allocated to {patient.Name}");
    bedManager.CheckOccupancyThreshold(allocatedBed.WardID);

    NotificationController.CreateNotification(
        _context,
        $"Bed {allocatedBed.BedID} in ward {allocatedBed.WardID} " +
        $"allocated to {patient.Name} using {strategyUsed}");

    TempData["Success"] =
        $"Bed {allocatedBed.BedID} allocated to {patient.Name} " +
        $"using {strategyUsed}";

    return RedirectToAction("Dashboard");
}
// GET: /Bed/SimulateCapacity
public IActionResult SimulateCapacity()
{
    if (HttpContext.Session.GetString("UserID") == null)
        return RedirectToAction("Login", "Account");

    // Set all beds to Occupied except 2 per ward
    var beds = _context.Beds.ToList();
    var wardGroups = beds.GroupBy(b => b.WardID);

    foreach (var wardGroup in wardGroups)
    {
        var wardBeds = wardGroup.ToList();
        // Leave 1 bed available per ward, rest occupied
        for (int i = 0; i < wardBeds.Count; i++)
        {
            if (i == 0)
                wardBeds[i].Status = "Available";
            else
                wardBeds[i].Status = "Occupied";
        }
    }

    _context.SaveChanges();

    // Trigger observer notification
    var bedManager = BedManager.GetInstance();
    var totalBeds = beds.Count;
    var occupiedBeds = beds.Count(b => b.Status == "Occupied");
    var occupancyRate = (float)occupiedBeds / totalBeds * 100;

    bedManager.NotifyObservers(
        $"CRITICAL: Hospital occupancy at {Math.Round(occupancyRate, 1)}% " +
        $"— {occupiedBeds} of {totalBeds} beds occupied");

    NotificationController.CreateNotification(
        _context,
        $"CAPACITY WARNING: Hospital occupancy at " +
        $"{Math.Round(occupancyRate, 1)}% — " +
        $"{occupiedBeds} of {totalBeds} beds occupied. " +
        $"Only 1 bed available per ward.");

    TempData["Success"] = 
        $"Demo: Capacity simulated at {Math.Round(occupancyRate, 1)}% " +
        $"occupancy. Check notifications for capacity alert.";

    return RedirectToAction("Dashboard");
}

// GET: /Bed/ResetCapacity
public IActionResult ResetCapacity()
{
    if (HttpContext.Session.GetString("UserID") == null)
        return RedirectToAction("Login", "Account");

    // Reset all beds to original seeded state
    var beds = _context.Beds.ToList();

    foreach (var bed in beds)
    {
        // Match original seeder states
        if (bed.BedID == "B005" || bed.BedID == "B008" ||
            bed.BedID == "B013" || bed.BedID == "B016" ||
            bed.BedID == "B021" || bed.BedID == "B024")
        {
            bed.Status = "Occupied";
        }
        else
        {
            bed.Status = "Available";
        }
    }

    _context.SaveChanges();

    NotificationController.CreateNotification(
        _context,
        "Demo reset: Bed capacity returned to normal levels.");

    TempData["Success"] = "Demo: Bed capacity reset to original state.";

    return RedirectToAction("Dashboard");
}
    }
 
}