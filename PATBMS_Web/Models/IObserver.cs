#nullable disable
using System.ComponentModel.DataAnnotations;
namespace PATBMS_Web.Models
{
    public interface IObserver
    {
        void Update(string message);
    }
}