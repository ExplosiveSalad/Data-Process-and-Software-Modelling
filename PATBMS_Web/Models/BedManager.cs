#nullable disable
using System.Collections.Generic;

namespace PATBMS_Web.Models
{
    public class BedManager : ISubject
    {
        // Singleton instance - only one can ever exist
        private static BedManager instance;

        // Private constructor prevents external instantiation
        private BedManager()
        {
            wards = new List<Ward>();
            observers = new List<IObserver>();
        }

        // Static method to get the single instance
        public static BedManager GetInstance()
        {
            if (instance == null)
            {
                instance = new BedManager();
            }
            return instance;
        }

        // Ward and observer lists
        private List<Ward> wards;
        private List<IObserver> observers;

        // Ward management
        public void AddWard(Ward ward)
        {
            wards.Add(ward);
            Console.WriteLine($"Ward {ward.WardName} added to BedManager.");
        }

        public int GetAvailableBeds(string wardID)
        {
            foreach (Ward ward in wards)
            {
                if (ward.WardID == wardID)
                {
                    return ward.GetAvailableBeds();
                }
            }
            return 0;
        }

        public float GetOccupancyRate(string wardID)
        {
            foreach (Ward ward in wards)
            {
                if (ward.WardID == wardID)
                {
                    return ward.GetOccupancyRate();
                }
            }
            return 0;
        }

        public Bed AllocateBed(Patient patient, IBedAllocationStrategy strategy)
        {
            // Use strategy pattern to allocate bed
            return strategy.AllocateBed(patient, wards);
        }

        // Observer pattern methods
        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
            Console.WriteLine("Observer subscribed to BedManager.");
        }

        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
            Console.WriteLine("Observer unsubscribed from BedManager.");
        }

        public void NotifyObservers(string message)
        {
            foreach (IObserver observer in observers)
            {
                observer.Update(message);
            }
        }

        // Checks occupancy and automatically notifies if threshold exceeded
        public void CheckOccupancyThreshold(string wardID)
        {
            float rate = GetOccupancyRate(wardID);
            if (rate >= 80)
            {
                NotifyObservers($"WARNING: Ward {wardID} occupancy at {rate}% - action required");
            }
            else if (rate >= 60)
            {
                NotifyObservers($"ALERT: Ward {wardID} occupancy at {rate}% - monitor closely");
            }
        }
    }
}