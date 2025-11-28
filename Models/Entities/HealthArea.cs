namespace DnTech_PBS_UniformManagement.Models.Entities
{
    public class HealthArea
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public int ProvinceId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool Active { get; set; } = true;

        // Navigation properties
        public Province Province { get; set; } = null!;
        public ICollection<EmployeeHealthArea> EmployeeHealthAreas { get; set; } = new List<EmployeeHealthArea>();
    }
}
