using DnTech_PBS_UniformManagement.Models.Enums;

namespace DnTech_PBS_UniformManagement.ViewModels
{
    public class EmployeeWithUniformsViewModel
    {
        public string EmployeeId { get; set; } = string.Empty;
        public int HealthAreaId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? IdCard { get; set; }
        public string Email { get; set; } = string.Empty;
        public EmployeePosition? Position { get; set; }
        public DateTime AssignedAt { get; set; }

        // Información de la última entrega
        public int? LastDeliveryId { get; set; }
        public DateTime? LastDeliveryDate { get; set; }
        public DateTime? NextDeliveryDate { get; set; }
        public string? DeliveryStatus { get; set; }
        public int? DaysUntilNextDelivery { get; set; }
        public int TotalDeliveries { get; set; }
    }
}
