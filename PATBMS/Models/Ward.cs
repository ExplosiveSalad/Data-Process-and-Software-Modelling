namespace PATBMS.Models
{
    public class Ward
    {
        private string wardID;
        private string wardName;
        private string specialty;
        private int totalBeds;
        private List<Bed> beds;

        public string WardID
        {
            get{return wardID;}
            set{wardID = value;}
        }
        public string WardName
        {
            get{return wardName;}
            set{wardName = value;}
        }
        public string Specialty
        {
            get{return specialty;}
            set{specialty = value;}
        }
        public int TotalBeds
        {
            get{return totalBeds;}
            set{totalBeds = value;}
        }
        public Ward(string wardID, string wardName, string specialty, int totalBeds)
        {
            beds = new List<Bed>();
            this.wardID = wardID;
            this.wardName = wardName;
            this.specialty = specialty;
            this.totalBeds = totalBeds;
        }

        public int GetAvailableBeds()
        {
           int count = 0;
           foreach(Bed bed in beds)
            {
                if (bed.Status == "Available")
                {
                    count++;
                }
            } 
            return count;
        }

        public float GetOccupancyRate()
        {
            //Real time dashboard integration will be implemented in Part 2
            if (totalBeds == 0) return 0;
            return ((float)(totalBeds - GetAvailableBeds())/ totalBeds) * 100;
        }
        public void AddBed(Bed bed)
        {
            beds.Add(bed);
            Console.WriteLine($"Bed {bed.BedID} has been added to {wardName}.");
        }
    }
}