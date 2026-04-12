namespace PATBMS.Models
{
    public class DiagnosisRecord
    {
        private string diagnosisID;
        private string diagnosis;
        private string treatmentPlan;
        private string dateRecorded;

        public string DiagnosisID
        {
            get{return diagnosisID;}
            set{diagnosisID = value;}
        }
        public string Diagnosis
        {
            get{return diagnosis;}
            set{diagnosis = value;}
        }
        public string TreatmentPlan
        {
            get{return treatmentPlan;}
            set{treatmentPlan = value;}
        }
        public string DateRecorded
        {
            get{return dateRecorded;}
            set{dateRecorded = value;}
        }
        public DiagnosisRecord(string diagnosisID, string diagnosis, string treatmentPlan, string dateRecorded)
        {
            this.diagnosisID = diagnosisID;
            this.diagnosis = diagnosis;
            this.treatmentPlan = treatmentPlan;
            this.dateRecorded = dateRecorded;
        }
        public void SendToAdmin()
        {
            Console.WriteLine("Diagnosis and treatment plan has been sent to Administrative Staff for processing.");
        }
    }
}