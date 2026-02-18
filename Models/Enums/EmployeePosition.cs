using System.ComponentModel.DataAnnotations;

namespace DnTech_PBS_UniformManagement.Models.Enums
{
    public enum EmployeePosition
    {
        [Display(Name = "Office worker")]
        OfficeWorker = 1,

        [Display(Name = "Miscellaneous")]
        Miscellaneous = 2
    }
}
