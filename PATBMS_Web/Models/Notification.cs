#nullable disable
using System.ComponentModel.DataAnnotations;

namespace PATBMS_Web.Models
{
    public class Notification
    {
        public Notification() { }
        private string notificationID;
        private string message;
        private string dateSent;
        private bool isAcknowledged;
        private string acknowledgedBy;

        [Key]
        public string NotificationID
        {
            get{return notificationID;}
            set{notificationID = value;}
        }
        public string Message
        {
            get{return message;}
            set{message = value;}
        }
        public string DateSent
        {
            get{return dateSent;}
            set{dateSent = value;}
        }
        public bool IsAcknowledged
        {
            get{return isAcknowledged;}
            set{isAcknowledged = value;}
        }
        public string AcknowledgedBy
        {
            get { return acknowledgedBy; }
            set {acknowledgedBy = value; }
        }

        public Notification(string notificationID, string message, string dateSent, bool isAcknowledged)
        {
            this.notificationID = notificationID;
            this.message = message;
            this.dateSent = dateSent;
            this.isAcknowledged = isAcknowledged;
            this.acknowledgedBy = null;
        }
        public void Acknowledge()
        {
            isAcknowledged = true;
            Console.WriteLine($"Notification {notificationID} has been acknowledged");
        }
    }
}