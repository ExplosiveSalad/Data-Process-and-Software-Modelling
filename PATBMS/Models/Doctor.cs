
namespace PATBMS.Models
{
 public class Doctor : User
    {
        private string specialisation;

        public string Specialisation
        {
            get {return specialisation; }
            set {specialisation = value; }
        }

        public Doctor(string userID, string name, string email, string password, string role, string specialisation) : base(userID, name, email, password, role)
        {
            this.specialisation = specialisation;
        }

        public void EnterDiagnosis()
        {
            Console.WriteLine("Dr. " + Name + " has entered a diagnosis and treatment plan.");
        }

        public void AuthoriseAdmission()
        {
            Console.WriteLine("Dr. " + Name + " has authorised patient admission.");
        }

        public void ApproveBedAllocation()
        {
            Console.WriteLine("Dr. " + Name + " has approved the bed allocation.");
        }
    }
}