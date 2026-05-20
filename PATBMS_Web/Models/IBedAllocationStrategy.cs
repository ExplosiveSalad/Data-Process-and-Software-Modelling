#nullable disable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PATBMS_Web.Models
{
    public interface IBedAllocationStrategy
    {
        Bed AllocateBed(Patient patient, List<Ward> wards);
    }
}