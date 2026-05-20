#nullable disable
using PATBMS_Web.Models;

namespace PATBMS_Web.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Only seed if database is empty
            if (context.Wards.Any()) return;

            // Create Wards
            var teHenga = new Ward("W001", "Te Henga", "General Medicine", 10);
            var piha = new Ward("W002", "Piha", "Surgical", 8);
            var titirangi = new Ward("W003", "Titirangi", "Emergency", 6);

            context.Wards.AddRange(teHenga, piha, titirangi);
            context.SaveChanges();

            // Create Beds for Te Henga
            var beds = new List<Bed>
            {
                new Bed("B001", "Available", "Standard"),
                new Bed("B002", "Available", "Standard"),
                new Bed("B003", "Available", "Standard"),
                new Bed("B004", "Available", "High Dependency"),
                new Bed("B005", "Occupied", "Standard"),
                new Bed("B006", "Available", "Standard"),
                new Bed("B007", "Available", "High Dependency"),
                new Bed("B008", "Occupied", "Standard"),
                new Bed("B009", "Available", "Standard"),
                new Bed("B010", "Available", "Standard"),
            };

            context.Beds.AddRange(beds);
            context.SaveChanges();

            // Create Beds for Piha
            var pihaBeds = new List<Bed>
            {
                new Bed("B011", "Available", "Standard"),
                new Bed("B012", "Available", "Standard"),
                new Bed("B013", "Occupied", "High Dependency"),
                new Bed("B014", "Available", "Standard"),
                new Bed("B015", "Available", "Standard"),
                new Bed("B016", "Occupied", "Standard"),
                new Bed("B017", "Available", "High Dependency"),
                new Bed("B018", "Available", "Standard"),
            };

            context.Beds.AddRange(pihaBeds);
            context.SaveChanges();

            // Create Beds for Titirangi
            var titirangiBeds = new List<Bed>
            {
                new Bed("B019", "Available", "Standard"),
                new Bed("B020", "Available", "High Dependency"),
                new Bed("B021", "Occupied", "Standard"),
                new Bed("B022", "Available", "Standard"),
                new Bed("B023", "Available", "Standard"),
                new Bed("B024", "Occupied", "High Dependency"),
            };

            context.Beds.AddRange(titirangiBeds);
            context.SaveChanges();

            // Create Users using UserFactory
            var factory = new UserFactory();

            var users = new List<User>
            {
                factory.CreateUser("doctor", "U001", "Dr. Smith", 
                    "smith@waitakere.nz", "password123"),
                factory.CreateUser("doctor", "U002", "Dr. Patel", 
                    "patel@waitakere.nz", "password123"),
                factory.CreateUser("nurse", "U003", "Jane Doe", 
                    "jane@waitakere.nz", "password123"),
                factory.CreateUser("nurse", "U004", "Mark Tana", 
                    "mark@waitakere.nz", "password123"),
                factory.CreateUser("admin", "U005", "Bob Jones", 
                    "bob@waitakere.nz", "password123"),
                factory.CreateUser("allied", "U006", "Sara Kim", 
                    "sara@waitakere.nz", "password123"),
                factory.CreateUser("management", "U007", "John Baker", 
                    "baker@waitakere.nz", "password123"),
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            // Create sample patients
            var patients = new List<Patient>
            {
                new Patient("P001", "NHI001", "John Tana", 
                    "1990-05-15", "12 West Auckland Rd", "021123456"),
                new Patient("P002", "NHI002", "Mary Smith", 
                    "1985-03-22", "45 Henderson Valley Rd", "021234567"),
                new Patient("P003", "NHI003", "James Lee", 
                    "2000-11-08", "78 Swanson Rd", "021345678"),
            };

            context.Patients.AddRange(patients);
            context.SaveChanges();
        }
    }
}