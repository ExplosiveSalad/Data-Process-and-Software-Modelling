#nullable disable
using System.ComponentModel.DataAnnotations;
namespace PATBMS_Web.Models
{
    public interface IUserFactory
    {
        User CreateUser(string role, string userID, string name, 
                        string email, string password);
    }
}