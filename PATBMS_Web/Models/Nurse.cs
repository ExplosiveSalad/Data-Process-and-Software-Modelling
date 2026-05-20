using System.ComponentModel.DataAnnotations;

namespace PATBMS_Web.Models
{
    public class Nurse : User, IObserver
    {
        private string wardAssigned;

        public string WardAssigned
        {
            get {return wardAssigned; }
            set {wardAssigned = value; }
        }

        public Nurse(string userID, string name, string email, string password, string role, string wardAssigned) : base(userID, name, email, password, role)
        {
            this.wardAssigned = wardAssigned;
        }

        public void PerformTriage()
        {
            Console.WriteLine("Nurse " + Name + " has performed triage.");
        }

        public void ConductHandover()
        {
            Console.WriteLine("Nurse " + Name + " has conducted a department handover.");
        }

        public void Update(string message)
        {
            // To be enhanced in Phase 2 with real notification system
        Console.WriteLine($"Nurse {Name} received notification: {message}");
        }
    }
}