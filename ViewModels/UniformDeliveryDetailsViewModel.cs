namespace DnTech_PBS_UniformManagement.ViewModels
{
    public class UniformDeliveryDetailsViewModel
    {
        public int DeliveryId { get; set; }
        public string EmployeeId { get; set; } = string.Empty;
        public int HealthAreaId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string? EmployeeIdCard { get; set; }
        public string EmployeePosition { get; set; } = string.Empty;
        public string HealthAreaName { get; set; } = string.Empty;
        public string ProvinceName { get; set; } = string.Empty;

        public DateTime DeliveryDate { get; set; }
        public DateTime? NextDeliveryDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Observations { get; set; }
        public int? DaysUntilNextDelivery { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<DeliveryDetailViewModel> Details { get; set; } = new List<DeliveryDetailViewModel>();
    }
}
