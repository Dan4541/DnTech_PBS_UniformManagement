using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnTech_PBS_UniformManagement.Models.Entities
{
    public class EmployeeHealthArea
    {
        [Key]
        public string EmployeeId { get; set; } = string.Empty;

        [Required]
        public int HealthAreaId { get; set; }

        [Required(ErrorMessage = "El puesto es requerido")]
        [StringLength(50)]
        [Display(Name = "Puesto")]
        public string Position { get; set; } = string.Empty;

        [Display(Name = "Fecha de Asignación")]
        [DataType(DataType.Date)]
        public DateTime AssignedAt { get; set; } = DateTime.Now;

        [Display(Name = "Activo")]
        public bool Active { get; set; } = true;

        // Navigation properties
        [ForeignKey("EmployeeId")]
        public virtual ApplicationUser? Employee { get; set; }

        [ForeignKey("HealthAreaId")]
        public virtual HealthArea? HealthArea { get; set; }

        public virtual ICollection<UniformDelivery> UniformDeliveries { get; set; } = new List<UniformDelivery>();
    }
}
