namespace PATBMS.Models
{
    public class Notification
    {
        private string notificationID;
        private string message;
        private string dateSent;
        private bool isAcknowledged;

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

        public Notification(string notificationID, string message, string dateSent, bool isAcknowledged)
        {
            this.notificationID = notificationID;
            this.message = message;
            this.dateSent = dateSent;
            this.isAcknowledged = isAcknowledged;
        }
        public void SendNotification()
        {
            //Automated 30-minute alert system as per NFR6 to be implemented in Part 2
            Console.WriteLine($"=== NEW NOTIFICATION ===");
            Console.WriteLine($"Notification ID: {notificationID}");
            Console.WriteLine($"Message: {message}");
            Console.WriteLine($"Date Sent: {dateSent}");
        }
        public void Acknowledge()
        {
            isAcknowledged = true;
            Console.WriteLine($"Notification {notificationID} has been acknowledged");
        }
    }
}