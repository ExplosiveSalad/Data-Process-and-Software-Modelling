namespace PATBMS.Models
{
    public class AlliedHealthProfessional : User
    {
        private string profession;
        
        public string Profession
        {
            get {return profession; }
            set {profession = value; }
        }

        public AlliedHealthProfessional(string userID, string name, string email, string password, string role, string profession) : base(userID, name, email, password, role)
        {
            this.profession = profession;
        }

        public void RecordAssessment()
        {
            Console.WriteLine("Professional " + Name + " has recorded an assessment.");
        }

        public void ConfirmDischarge()
        {
            Console.WriteLine("Professional " + Name + " has confirmed a patients discharge.");
        }
    }
}