namespace DnTech_PBS_UniformManagement.ViewModels
{
    public class DashboardViewModel
    {
        public List<ProvinceWithAreasViewModel> Provinces { get; set; } = new();
        public int TotalProvinces { get; set; }
        public int TotalHealthAreas { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalUsers { get; set; }
    }
}
