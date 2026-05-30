namespace PATBMS.Models
{
    public class Discharge
    {
        private string dischargeID;
        private string dischargeDate;
        private string dischargeReason;

        public string DischargeID
        {
            get{return dischargeID;}
            set{dischargeID = value;}
        }
        public string DischargeDate
        {
            get{return dischargeDate;}
            set{dischargeDate = value;}
        }
        public string DischargeReason
        {
            get{return dischargeReason;}
            set{dischargeReason = value;}
        }

        public Discharge(string dischargeID, string dischargeDate, string dischargeReason)
        {
            this.dischargeID = dischargeID;
            this.dischargeDate = dischargeDate;
            this.dischargeReason = dischargeReason;
        }
        public void ConfirmDischarge()
        {
            Console.WriteLine($"{dischargeID} on {dischargeDate} has been discharged because {dischargeReason}");
        }
        public void UpdateBedStatus()
        {
            Console.WriteLine($"Bed status has been updated to Available following discharge {dischargeID}");
        }
    }
}