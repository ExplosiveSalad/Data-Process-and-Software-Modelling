#nullable disable
using System.ComponentModel.DataAnnotations;
namespace PATBMS_Web.Models
{
    public class UserFactory : IUserFactory
    {
        // Factory method - creates correct User subclass based on role
        public User CreateUser(string role, string userID, string name, 
                               string email, string password)
        {
            switch (role.ToLower())
            {
                case "doctor":
                    return new Doctor(
                        userID, name, email, password, role, 
                        "General Medicine");

                case "nurse":
                    return new Nurse(
                        userID, name, email, password, role, 
                        "Unassigned");

                case "admin":
                    return new AdminStaff(
                        userID, name, email, password, role, 
                        "General");

                case "allied":
                    return new AlliedHealthProfessional(
                        userID, name, email, password, role, 
                        "General");

                case "management":
                    return new HospitalManagement(
                        userID, name, email, password, role, 
                        "General", "Senior");

                default:
                    // Business rule - invalid role throws exception
                    throw new ArgumentException($"Invalid role: {role}");
            }
        }
    }
}