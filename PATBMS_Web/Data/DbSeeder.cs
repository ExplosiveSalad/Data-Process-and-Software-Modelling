#nullable disable
using PATBMS_Web.Models;

namespace PATBMS_Web.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Wards.Any()) return;

            var teHenga = new Ward("W001", "Te Henga", "General Medicine", 10);
            var piha = new Ward("W002", "Piha", "Surgical", 8);
            var titirangi = new Ward("W003", "Titirangi", "Emergency", 6);

            context.Wards.AddRange(teHenga, piha, titirangi);
            context.SaveChanges();

            // Te Henga beds - General Medicine (ATS 3-5)
            var teHengaBeds = new List<Bed>
            {
                new Bed("B001", "Available", "Standard", "W001"),
                new Bed("B002", "Available", "Standard", "W001"),
                new Bed("B003", "Available", "Standard", "W001"),
                new Bed("B004", "Available", "High Dependency", "W001"),
                new Bed("B005", "Occupied", "Standard", "W001"),
                new Bed("B006", "Available", "Standard", "W001"),
                new Bed("B007", "Available", "High Dependency", "W001"),
                new Bed("B008", "Occupied", "Standard", "W001"),
                new Bed("B009", "Available", "Standard", "W001"),
                new Bed("B010", "Available", "Standard", "W001"),
            };
            context.Beds.AddRange(teHengaBeds);
            context.SaveChanges();

            // Piha beds - Surgical (ATS 3-4)
            var pihaBeds = new List<Bed>
            {
                new Bed("B011", "Available", "Standard", "W002"),
                new Bed("B012", "Available", "Standard", "W002"),
                new Bed("B013", "Occupied", "High Dependency", "W002"),
                new Bed("B014", "Available", "Standard", "W002"),
                new Bed("B015", "Available", "Standard", "W002"),
                new Bed("B016", "Occupied", "Standard", "W002"),
                new Bed("B017", "Available", "High Dependency", "W002"),
                new Bed("B018", "Available", "Standard", "W002"),
            };
            context.Beds.AddRange(pihaBeds);
            context.SaveChanges();

            // Titirangi beds - Emergency (ATS 1-2)
            var titirangiBeds = new List<Bed>
            {
                new Bed("B019", "Available", "Standard", "W003"),
                new Bed("B020", "Available", "High Dependency", "W003"),
                new Bed("B021", "Occupied", "Standard", "W003"),
                new Bed("B022", "Available", "Standard", "W003"),
                new Bed("B023", "Available", "Standard", "W003"),
                new Bed("B024", "Occupied", "High Dependency", "W003"),
            };
            context.Beds.AddRange(titirangiBeds);
            context.SaveChanges();

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