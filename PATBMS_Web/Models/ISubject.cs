#nullable disable
using System.ComponentModel.DataAnnotations;
namespace PATBMS_Web.Models
{
    public interface ISubject
    {
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
        void NotifyObservers(string message);
    }
}