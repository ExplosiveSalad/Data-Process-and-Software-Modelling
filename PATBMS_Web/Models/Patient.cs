#nullable disable
using System.ComponentModel.DataAnnotations;

namespace PATBMS_Web.Models
{
    public class Patient
    {
        public Patient()
        {
            admissionStatus = "Registered";
            bedID = null;
        }
        private string patientID;
        private string nhiNumber;
        private string name;
        private string dateOfBirth;
        private string address;
        private string phoneNumber;
        private string admissionStatus;
        private string bedID;

        [Key]
        public string PatientID
        {
            get {return patientID;}
            set {patientID = value;}
        }
        public string NHINumber
        {
            get{return nhiNumber;}
            set{nhiNumber = value;}
        }
        public string Name
        {
            get{return name;}
            set{name = value;}
        }
        public string DateOfBirth
        {
            get{return dateOfBirth;}
            set{dateOfBirth = value;}
        }
        public string Address
        {
            get{return address;}
            set{address = value;}
        }
        public string PhoneNumber
        {
            get{return phoneNumber;}
            set{phoneNumber = value;}
        }

        public string AdmissionStatus
        {
            get { return admissionStatus; }
            set { admissionStatus = value; }
        }

        public string BedID
        {
            get { return bedID; }
            set { bedID = value; }
        }

        public Patient(string patientID, string nhiNumber, string name, string dateOfBirth, string address, string phoneNumber)
        {
            this.patientID = patientID;
            this.nhiNumber = nhiNumber;
            this.name = name;
            this.dateOfBirth = dateOfBirth;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.admissionStatus  = "Registered";
        }

        public void GetRecord()
        {
            Console.WriteLine("=== Patient Record ===");
            Console.WriteLine($"Patient ID: {PatientID}");
            Console.WriteLine($"NHI Number: {NHINumber}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Date of Birth: {dateOfBirth}");
            Console.WriteLine($"Address: {address}");
            Console.WriteLine($"Phone Number: {phoneNumber}");
            Console.WriteLine($"Admission  Status: {admissionStatus}");
        }
    }
}