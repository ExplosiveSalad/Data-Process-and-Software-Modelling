namespace PATBMS.Models
{
    public class HandoverRecord
    {
        private string handoverID;
        private string clinicalStatus;
        private string outstandingTasks;
        private string specialNeeds;
        private string sendingDepartment;
        private string receivingDepartment;

        public string HandoverID
        {
            get{return handoverID;}
            set{handoverID = value;}
        }
        public string ClinicalStatus
        {
            get{return clinicalStatus;}
            set{clinicalStatus = value;}
        }
        public string OutstandingTasks
        {
            get{return outstandingTasks;}
            set{outstandingTasks = value;}
        }
        public string SpecialNeeds
        {
            get{return specialNeeds;}
            set{specialNeeds = value;}
        }
        public string SendingDepartment
        {
            get{return sendingDepartment;}
            set{sendingDepartment = value;}
        }
        public string ReceivingDepartment
        {
            get{return receivingDepartment;}
            set{receivingDepartment = value;}
        }
        public HandoverRecord(string handoverID, string clinicalStatus, string outstandingTasks, string specialNeeds, string sendingDepartment, string receivingDepartment)
        {
            this.handoverID = handoverID;
            this.clinicalStatus = clinicalStatus;
            this.outstandingTasks = outstandingTasks;
            this.specialNeeds = specialNeeds;
            this.sendingDepartment = sendingDepartment;
            this.receivingDepartment = receivingDepartment;
        }
        public void SendHandover()
        {
            Console.WriteLine($"Sending {handoverID}");
            Console.WriteLine("=== HANDOVER INFORMATION ===");
            Console.WriteLine($"Handover ID: {handoverID}");
            Console.WriteLine($"Clinical Status: {clinicalStatus}");
            Console.WriteLine($"Outstanding Tasks: {outstandingTasks}");
            Console.WriteLine($"Special Needs: {specialNeeds}");
            Console.WriteLine($"Sending Department: {sendingDepartment}");
            Console.WriteLine($"Receiving Department: {receivingDepartment}");
            Console.WriteLine("=== END OF FILE ===");
        }
        public void NotifyDepartment()
        {
            Console.WriteLine($"You have received information from {sendingDepartment}.");
        }
    }
}