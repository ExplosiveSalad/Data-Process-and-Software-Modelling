using PATBMS.Models;
class Program
{
    static void Main(string[] args)
    {
    Ward ward = new Ward("W001", "Te Henga", "General Medicine", 3);
    Bed bed1 = new Bed("B001", "Available", "Standard");
    Bed bed2 = new Bed("B002", "Available", "Standard");
    Bed bed3 = new Bed("B003", "Occupied", "High Dependency");
    ward.AddBed(bed1);
    ward.AddBed(bed2);
    ward.AddBed(bed3);

    Doctor doctor = new Doctor("D001", "Smith", "smith@hospital.nz", "pass123", "Doctor", "General Medicine");
    Nurse nurse = new Nurse("N001", "Jane Doe", "jane@hospital.nz", "pass123", "Nurse", "Te Henga");
    AdminStaff admin = new AdminStaff("A001", "Bob Jones", "bob@hospital.nz", "pass123", "Admin", "ECC");

    Patient patient = new Patient("P001", "NHI12345", "John Tana", "1990-05-15", "12 West Auckland Rd", "021123456");
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n=== PATBMS MAIN MENU ===");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register Patient");
            Console.WriteLine("3. Perform Triage");
            Console.WriteLine("4. Allocate Bed");
            Console.WriteLine("5. Record Discharge");
            Console.WriteLine("0. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine() ?? "";

            switch(choice)
            {
                case "1":
                doctor.Login();
                nurse.Login();
                admin.Login();
                break;

                case "2":
                admin.RegisterPatient();
                patient.GetRecord();
                break;

                case "3":
                nurse.PerformTriage();
                TriageAssessment triage = new TriageAssessment("T001", 3, "Chest pain", "BP 140/90", DateTime.Now.ToString("dd/MM/yyyy"));
                triage.AssignATSCategory();
                break;

                case "4":
                Console.WriteLine($"Available beds in {ward.WardName}: {ward.GetAvailableBeds()}");
                doctor.ApproveBedAllocation();
                BedAllocation allocation = new BedAllocation("BA001", DateTime.Now.ToString("dd/MM/yyyy"), doctor.Name);
                allocation.ApproveAllocation();
                bed1.MarkAsOccupied();
                Console.WriteLine($"Available beds after allocation: {ward.GetAvailableBeds()}");
                Console.WriteLine($"Occupancy Rate: {ward.GetOccupancyRate()}%");
                break;

                case "5":
                Discharge discharge = new Discharge("DIS001", DateTime.Now.ToString("dd/MM/yyyy"), "Treatment Complete");
                discharge.ConfirmDischarge();
                discharge.UpdateBedStatus();
                bed1.MarkAsAvailable();
                Console.WriteLine($"Available beds after discharge: {ward.GetAvailableBeds()}");
                break;

                case "0":
                running = false;
                break;

                default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
            }
        }
    }
}