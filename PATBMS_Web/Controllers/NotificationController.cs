#nullable disable
using Microsoft.AspNetCore.Mvc;
using PATBMS_Web.Data;
using PATBMS_Web.Models;

namespace PATBMS_Web.Controllers
{
    public class NotificationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            var notifications = _context.Notifications
                .OrderByDescending(n => n.DateSent)
                .ToList();

            ViewBag.CurrentUserID = HttpContext.Session.GetString("UserID");

            return View(notifications);
        }

        [HttpPost]
        public IActionResult Acknowledge(string notificationID)
        {
            if (HttpContext.Session.GetString("UserID") == null)
                return RedirectToAction("Login", "Account");

            var userID = HttpContext.Session.GetString("UserID");
            var userName = HttpContext.Session.GetString("UserName");

            var notification = _context.Notifications.Find(notificationID);
            if (notification != null)
            {
                notification.IsAcknowledged = true;
                notification.AcknowledgedBy = userID;
                _context.SaveChanges();
            }

            TempData["Success"] = "Notification acknowledged.";
            return RedirectToAction("Index");
        }

        // Called by BedManager when capacity threshold is exceeded
        public static void CreateNotification(
            ApplicationDbContext context, string message)
        {
            var notificationID = "N" + DateTime.Now.Ticks.ToString().Substring(10);
            var notification = new Notification(
                notificationID,
                message,
                DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                false);

            context.Notifications.Add(notification);
            context.SaveChanges();
        }
    }
}