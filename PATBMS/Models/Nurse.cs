namespace PATBMS.Models
{
    public class Nurse : User
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

        public void UpdateBedStatus()
        {
            Console.WriteLine("Nurse " + Name + " has updated bed status.");
        }

        public void ConductHandover()
        {
            Console.WriteLine("Nurse " + Name + " has conducted a department handover.");
        }
    }
}