namespace PATBMS.Models
{
    public class Bed
    {
    private string bedID;
    private string status;
    private string bedType;

    public string BedID
    {
        get {return bedID;}
        set{bedID = value;}
    }
    public string Status
        {
            get{return status;}
            set{status = value;}
        }
    public string BedType
        {
            get{return bedType;}
            set{bedType = value;}
        }
    public Bed(string bedID, string status, string bedType)
        {
            this.bedID = bedID;
            this.status = status;
            this.bedType = bedType;
        }
        
    public void MarkAsOccupied()
        {
            status = "Occupied";
            Console.WriteLine("Bed has been marked as occupied.");
        }
    public void MarkAsAvailable()
        {
            status = "Available";
            Console.WriteLine("Bed has been marked as available.");
        }
}
}