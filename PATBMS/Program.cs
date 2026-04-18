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

    AlliedHealthProfessional allied = new AlliedHealthProfessional("AH001", "Sara Kim", "sara@hospital.nz", "pass123", "Allied Health", "Physiotherapist");
    HospitalManagement management = new HospitalManagement("HM001", "John Baker", "baker@hospital.nz", "pass123", "Management", "Te Henga", "Senior");

    Patient patient = null;
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n=== PATBMS MAIN MENU ===");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register Patient");
            Console.WriteLine("3. Perform Triage");
            Console.WriteLine("4. Allocate Bed");
            Console.WriteLine("5. Record Discharge");
            Console.WriteLine("6. Send Notification");
            Console.WriteLine("7. Conduct Handover");
            Console.WriteLine("8. Allied Health and Management");
            Console.WriteLine("0. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine() ?? "";

            switch(choice)
            {
                case "1":
                Console.WriteLine("\n=== LOGIN ===");
                Console.Write("Enter username: ");
                string username = Console.ReadLine() ?? "";
                Console.Write("Enter password: ");
                string password = Console.ReadLine() ?? "";
                Console.WriteLine($"\nCredentials received: {username}");
                doctor.Login();
                nurse.Login();
                admin.Login();
                break;

                case "2":
                Console.WriteLine("\n=== REGISTER PATIENT ===");
                Console.Write("Enter Patient NHI Number: ");
                string nhi = Console.ReadLine() ?? "";
                Console.Write("Enter Patient Name: ");
                string patientName = Console.ReadLine() ?? "";
                Console.Write("Enter Date of Birth (dd/MM/yyyy): ");
                string dob = Console.ReadLine() ?? "";
                Console.Write("Enter Address: ");
                string address = Console.ReadLine() ?? "";
                Console.Write("Enter Phone Number: ");
                string phone = Console.ReadLine() ?? "";

                //Create patient from user input
                patient = new Patient("P001", nhi, patientName, dob, address, phone);
                admin.RegisterPatient();
                patient.GetRecord();
                break;

                case "3":
                if (patient == null)
                    {
                        Console.WriteLine("No patient registered. Please register a patient first.");
                        break;
                    }
                    Console.WriteLine("\n=== PERFORM TRIAGE ===");
                    Console.Write("Enter patient symptoms: ");
                    string symptoms = Console.ReadLine() ?? "";
                    Console.Write("Enter patient vitals (e.g. BP 120/80): ");
                    string vitals = Console.ReadLine() ?? "";
                    Console.Write("Enter ATS Category (1-5): ");
                    string atsInput = Console.ReadLine() ?? "";
                    int atsCategory = int.Parse(atsInput);

                    nurse.PerformTriage();
                    TriageAssessment triage = new TriageAssessment(
                        "T001",
                        atsCategory,
                        symptoms,
                        vitals,
                        DateTime.Now.ToString("dd/MM/yyyy"));
                    triage.AssignATSCategory();
                    break;

                case "4":
                Console.WriteLine("\n=== ALLOCATE BED ===");
                    Console.WriteLine($"Available beds in {ward.WardName}: {ward.GetAvailableBeds()}");
                    Console.WriteLine($"Occupancy Rate: {ward.GetOccupancyRate()}%");
                    Console.Write("Approve bed allocation? (yes/no): ");
                    string approveInput = Console.ReadLine() ?? "";

                    if (approveInput.ToLower() == "yes")
                    {
                        doctor.ApproveBedAllocation();
                        BedAllocation allocation = new BedAllocation(
                            "BA001",
                            DateTime.Now.ToString("dd/MM/yyyy"),
                            doctor.Name);
                        allocation.ApproveAllocation();
                        bed1.MarkAsOccupied();
                        Console.WriteLine($"Available beds after allocation: {ward.GetAvailableBeds()}");
                        Console.WriteLine($"New Occupancy Rate: {ward.GetOccupancyRate()}%");
                    }
                    else
                    {
                        Console.WriteLine("Bed allocation not approved.");
                        BedAllocation allocation = new BedAllocation(
                            "BA001",
                            DateTime.Now.ToString("dd/MM/yyyy"),
                            doctor.Name);
                        allocation.RequestTransfer();
                    }
                    break;

                case "5":
                Console.WriteLine("\n=== RECORD DISCHARGE ===");
                    Console.Write("Enter discharge reason: ");
                    string reason = Console.ReadLine() ?? "";

                    Discharge discharge = new Discharge(
                        "DIS001",
                        DateTime.Now.ToString("dd/MM/yyyy"),
                        reason);
                    discharge.ConfirmDischarge();
                    discharge.UpdateBedStatus();
                    bed1.MarkAsAvailable();
                    Console.WriteLine($"Available beds after discharge: {ward.GetAvailableBeds()}");
                    break;

                case "6":
                Console.WriteLine("\n=== SEND NOTIFICATION ===");
                    Notification notification = new Notification(
                        "NOT001",
                        "Bed capacity at 66% in Te Henga ward - update required",
                        DateTime.Now.ToString("dd/MM/yyyy"),
                        false);
                    notification.SendNotification();
                    Console.WriteLine($"Acknowledged: {notification.IsAcknowledged}");
                    Console.Write("Acknowledge this notification? (yes/no): ");
                    string ackInput = Console.ReadLine() ?? "";
                    if (ackInput.ToLower() == "yes")
                    {
                        notification.Acknowledge();
                        Console.WriteLine($"Acknowledged: {notification.IsAcknowledged}");
                    }
                    break;

                case "7":
               Console.WriteLine("\n=== CONDUCT HANDOVER AND RECORD ADMISSION ===");
                    Console.Write("Enter clinical status: ");
                    string clinicalStatus = Console.ReadLine() ?? "";
                    Console.Write("Enter outstanding tasks: ");
                    string tasks = Console.ReadLine() ?? "";
                    Console.Write("Enter special needs: ");
                    string needs = Console.ReadLine() ?? "";
                    Console.Write("Enter sending department: ");
                    string sendingDept = Console.ReadLine() ?? "";
                    Console.Write("Enter receiving department: ");
                    string receivingDept = Console.ReadLine() ?? "";

                    HandoverRecord handover = new HandoverRecord(
                        "H001",
                        clinicalStatus,
                        tasks,
                        needs,
                        sendingDept,
                        receivingDept);
                    handover.SendHandover();
                    handover.NotifyDepartment();

                    Admission admission = new Admission(
                        "ADM001",
                        DateTime.Now.ToString("dd/MM/yyyy"),
                        "Acute");
                    admission.RecordAdmission();
                    break;

                case "8":
                Console.WriteLine("\n=== ALLIED HEALTH AND MANAGEMENT ===");
                    allied.Login();
                    Console.Write("Enter assessment notes: ");
                    string assessment = Console.ReadLine() ?? "";
                    Console.WriteLine($"Assessment recorded: {assessment}");
                    allied.RecordAssessment();
                    allied.ConfirmDischarge();
                    management.Login();
                    management.ViewCapacityAlert();
                    management.GenerateReport();
                    break;

                case "0":
                running = false;
                Console.WriteLine("Exiting PATBMS. Goodbye.");
                break;

                default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
            }
        }
    }
}