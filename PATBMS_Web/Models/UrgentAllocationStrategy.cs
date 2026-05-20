#nullable disable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PATBMS_Web.Models
{
    public class UrgentAllocationStrategy : IBedAllocationStrategy
    {
        // For ATS 1-2 patients - prioritises high dependency beds
        public Bed AllocateBed(Patient patient, List<Ward> wards)
        {
            foreach (Ward ward in wards)
            {
                foreach (Bed bed in ward.GetBeds())
                {
                    // Business rule - urgent patients get high dependency beds first
                    if (bed.Status == "Available" && bed.BedType == "High Dependency")
                    {
                        bed.MarkAsOccupied();
                        Console.WriteLine($"URGENT: High dependency bed {bed.BedID} allocated to patient {patient.Name}");
                        return bed;
                    }
                }
            }

            // If no high dependency bed available fall back to any available bed
            foreach (Ward ward in wards)
            {
                foreach (Bed bed in ward.GetBeds())
                {
                    if (bed.Status == "Available")
                    {
                        bed.MarkAsOccupied();
                        Console.WriteLine($"URGENT: Standard bed {bed.BedID} allocated to urgent patient {patient.Name}");
                        return bed;
                    }
                }
            }

            Console.WriteLine("No beds available - transfer required");
            return null;
        }
    }
}