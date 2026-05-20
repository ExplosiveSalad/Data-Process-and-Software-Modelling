#nullable disable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PATBMS_Web.Models
{
    public class StandardAllocationStrategy : IBedAllocationStrategy
    {
        // For ATS 3-5 patients - allocates first available standard bed
        public Bed AllocateBed(Patient patient, List<Ward> wards)
        {
            foreach (Ward ward in wards)
            {
                foreach (Bed bed in ward.GetBeds())
                {
                    // Business rule - standard patients get standard beds
                    if (bed.Status == "Available" && bed.BedType == "Standard")
                    {
                        bed.MarkAsOccupied();
                        Console.WriteLine($"STANDARD: Bed {bed.BedID} allocated to patient {patient.Name}");
                        return bed;
                    }
                }
            }

            Console.WriteLine("No standard beds available - transfer required");
            return null;
        }
    }
}