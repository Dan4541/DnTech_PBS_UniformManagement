namespace DnTech_PBS_UniformManagement.ViewModels
{
    public class DeliveryDetailViewModel
    {
        public int Id { get; set; }
        public string GarmentType { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string? Notes { get; set; }
    }
}
