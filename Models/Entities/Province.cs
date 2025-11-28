namespace DnTech_PBS_UniformManagement.Models.Entities
{
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool Active { get; set; } = true;

        // Navigation property
        public ICollection<HealthArea> HealthAreas { get; set; } = new List<HealthArea>();
    }
}
