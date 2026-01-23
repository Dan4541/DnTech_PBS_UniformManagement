using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnTech_PBS_UniformManagement.Models.Entities
{
    public class UniformDelivery
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Empleado")]
        public string EmployeeId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Área de Salud")]
        public int HealthAreaId { get; set; }

        [Required]
        [Display(Name = "Fecha de Entrega")]
        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; } = DateTime.Now;

        [Display(Name = "Próxima Entrega")]
        [DataType(DataType.Date)]
        public DateTime? NextDeliveryDate { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Estado")]
        public string Status { get; set; } = "Sin entrega"; // Sin entrega, Entregado, Próximo

        [StringLength(500)]
        [Display(Name = "Observaciones")]
        public string? Observations { get; set; }

        [Display(Name = "Días Restantes para Próxima Entrega")]
        public int? DaysUntilNextDelivery { get; set; }

        [Display(Name = "Fecha de Registro")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("EmployeeId")]
        public virtual EmployeeHealthArea? Employee { get; set; }

        public virtual ICollection<DeliveryDetail> DeliveryDetails { get; set; } = new List<DeliveryDetail>();
    }
}
