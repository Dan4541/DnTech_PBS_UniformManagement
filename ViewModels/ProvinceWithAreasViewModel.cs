namespace DnTech_PBS_UniformManagement.ViewModels
{
    public class ProvinceWithAreasViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public int HealthAreasCount { get; set; }
        public List<HealthAreaViewModel> HealthAreas { get; set; } = new();
    }
}
