namespace DnTech_PBS_UniformManagement.ViewModels
{
    public class EmployeeDeliveriesViewModel
    {
        public string EmployeeId { get; set; } = string.Empty;
        public int HealthAreaId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string? EmployeeIdCard { get; set; }
        public string EmployeePosition { get; set; } = string.Empty;
        public string HealthAreaName { get; set; } = string.Empty;
        public string ProvinceName { get; set; } = string.Empty;

        public List<DeliveryHistoryViewModel> Deliveries { get; set; } = new List<DeliveryHistoryViewModel>();
    }
}
