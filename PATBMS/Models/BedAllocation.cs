namespace PATBMS.Models
{
    public class BedAllocation
    {
        private string allocationID;
        private string allocationDate;
        private string approvedBy;

        public string AllocationID
        {
            get{return allocationID;}
            set{allocationID = value;}
        }
        public string AllocationDate
        {
            get{return allocationDate;}
            set{allocationDate = value;}
        }
        public string ApprovedBy
        {
            get{return approvedBy;}
            set{approvedBy = value;}
        }

        public BedAllocation(string allocationID, string allocationDate, string approvedBy)
        {
            this.allocationID = allocationID;
            this.allocationDate = allocationDate;
            this.approvedBy = approvedBy;
        }
        public void ApproveAllocation()
        {
            Console.WriteLine($"Allocation {allocationID} from {allocationDate} has been approved by {approvedBy}.");
        }
        public void RequestTransfer()
        {
            Console.WriteLine($"{allocationID} from {allocationDate} has been requested to transfer. Awaiting approval.");
        }
    }
}