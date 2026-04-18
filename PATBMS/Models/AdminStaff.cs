namespace PATBMS.Models
{
    public class AdminStaff : User
    {
        private string department;

        public string Department
        {
            get {return department; }
            set {department = value; }
        }

        public AdminStaff(string userID, string name, string email, string password, string role, string department) : base(userID, name, email, password, role)
        {
            this.department = department;
        }

        public void RegisterPatient()
        {
            Console.WriteLine("Staff Member " + Name + " has registered a Patient.");
        }

        public void SearchPatientByNHI()
        {
            //NHI database integration with the National Health Index will be implemented in Part 2
            Console.WriteLine("Staff Member " + Name + " searched for a Patient.");
        }

        public void ProcessDocument()
        {
            Console.WriteLine("Staff Member " + Name + " has received and processed a document.");
        }
    }
}