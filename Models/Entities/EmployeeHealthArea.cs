namespace DnTech_PBS_UniformManagement.Models.Entities
{
    public class EmployeeHealthArea
    {
        public string EmployeeId { get; set; } = string.Empty;
        public int HealthAreaId { get; set; }
        public DateTime AssignedAt { get; set; } = DateTime.Now;
        public bool Active { get; set; } = true;

        // Navigation properties
        public ApplicationUser Employee { get; set; } = null!;
        public HealthArea HealthArea { get; set; } = null!;
    }
}
