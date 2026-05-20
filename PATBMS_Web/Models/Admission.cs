#nullable disable
using System.ComponentModel.DataAnnotations;

namespace PATBMS_Web.Models
{
    public class Admission
    {
        public Admission() { }
        private string admissionID;
        private string admissionDate;
        private string admissionType;

        [Key]
        public string AdmissionID
        {
            get{return admissionID;}
            set{admissionID = value;}
        }
        public string AdmissionDate
        {
            get{return admissionDate;}
            set{admissionDate = value;}
        }
        public string AdmissionType
        {
            get{return admissionType;}
            set{admissionType = value;}
        }

        public Admission(string admissionID, string admissionDate, string admissionType)
        {
            this.admissionID = admissionID;
            this.admissionDate = admissionDate;
            this.admissionType = admissionType;
        }
        public void RecordAdmission()
        {
            Console.WriteLine($"AdmissionID: {admissionID}");
            Console.WriteLine($"Admission Date: {admissionDate}");
            Console.WriteLine($"Admission Type: {admissionType}");
            Console.WriteLine("Admission Recorded.");
        }
    }
}