

namespace PATBMS.Models
{
    public class TriageAssessment
    {
        private string triageID;
        private int atsCategory;
        private string symptoms;
        private string vitals;
        private string assessmentDate;

        public string TriageID
        {
            get{return triageID;}
            set{triageID = value;}
        }
        public int ATSCategory
        {
            get{return atsCategory;}
            set{atsCategory = value;}
        }
        public string Symptoms
        {
            get{return symptoms;}
            set{symptoms = value;}
        }
        public string Vitals
        {
            get{return vitals;}
            set{vitals = value;}
        }
        public string AssessmentDate
        {
            get{return assessmentDate;}
            set{assessmentDate = value;}
        }
        public TriageAssessment(string triageID, int atsCategory, string symptoms, string vitals, string assessmentDate)
        {
            this.triageID = triageID;
            this.atsCategory = atsCategory;
            this.symptoms = symptoms;
            this.vitals = vitals;
            this.assessmentDate = assessmentDate;
        }

        public void AssignATSCategory()
        {
            Console.WriteLine($"ATS Category {atsCategory} has been assigned to this patient.");
        }
    }
}