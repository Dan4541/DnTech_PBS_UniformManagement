namespace DnTech_PBS_UniformManagement.ViewModels
{
    public class HealthAreaViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; } = string.Empty;
        public int EmployeesCount { get; set; }
    }
}
