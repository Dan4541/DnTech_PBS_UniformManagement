using System.ComponentModel.DataAnnotations;

namespace DnTech_PBS_UniformManagement.ViewModels
{
    public class AssignEmployeeViewModel
    {
        [Required]
        public int HealthAreaId { get; set; }

        public string? HealthAreaName { get; set; }

        [Required(ErrorMessage = "Please select an employee")]
        [Display(Name = "Select Employee")]
        public string? EmployeeId { get; set; }

        // Para crear un nuevo empleado en el mismo formulario
        [Display(Name = "Or Create New Employee")]
        public bool CreateNewEmployee { get; set; }

        [Display(Name = "Full Name")]
        public string? NewEmployeeFullname { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string? NewEmployeeEmail { get; set; }

        [Display(Name = "ID Card")]
        public string? NewEmployeeIdCard { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? NewEmployeePassword { get; set; }
    }
}
