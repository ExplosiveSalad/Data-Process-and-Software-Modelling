namespace PATBMS.Models
{
    public class HospitalManagement : User
    {
        private string department;
        private string managerLevel;
        public string Department
        {
            get {return department; }
            set {department = value; }
        }
        public string ManagerLevel
        {
            get {return managerLevel; }
            set {managerLevel = value; }
        }
        public HospitalManagement(string userID, string name, string email, string password, string role, string department, string managerLevel) : base(userID, name, email, password, role)
        {
            this.department = department;
            this.managerLevel = managerLevel;
        }
        public void ViewCapacityAlert()
        {
            Console.WriteLine("Manager " + Name + " is viewing the capacity alert.");
        }
        public void GenerateReport()
        {
            Console.WriteLine("Manager " + Name + " generated a Report.");
        }
    }
}