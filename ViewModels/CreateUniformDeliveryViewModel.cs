using System.ComponentModel.DataAnnotations;

namespace DnTech_PBS_UniformManagement.ViewModels
{
    public class CreateUniformDeliveryViewModel
    {
        [Required]
        public string EmployeeId { get; set; } = string.Empty;
        
        [Required]
        public int HealthAreaId { get; set; }

        [Required(ErrorMessage = "La fecha de entrega es requerida")]
        [Display(Name = "Fecha de Entrega")]
        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; } = DateTime.Now;

        [Display(Name = "Próxima Entrega (Opcional)")]
        [DataType(DataType.Date)]
        public DateTime? NextDeliveryDate { get; set; }

        [StringLength(500)]
        [Display(Name = "Observaciones")]
        public string? Observations { get; set; }

        // Información del empleado (para mostrar en la vista)
        public string? EmployeeName { get; set; }
        public string? EmployeeIdCard { get; set; }
        public string? EmployeePosition { get; set; }
        public string? HealthAreaName { get; set; }

        // Lista de prendas a entregar
        public List<DeliveryDetailItemViewModel> Items { get; set; } = new List<DeliveryDetailItemViewModel>();
    }
}
