using System.ComponentModel.DataAnnotations;

namespace DnTech_PBS_UniformManagement.Models.Enums
{
    public enum EmployeePosition
    {
        [Display(Name = "Office worker")]
        OfficeWorker,

        [Display(Name = "Miscellaneous")]
        Miscellaneous
    }
}
