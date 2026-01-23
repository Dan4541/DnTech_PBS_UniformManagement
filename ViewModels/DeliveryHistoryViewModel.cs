namespace DnTech_PBS_UniformManagement.ViewModels
{
    public class DeliveryHistoryViewModel
    {
        public int Id { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime? NextDeliveryDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public int ItemsCount { get; set; }
        public int? DaysUntilNextDelivery { get; set; }
    }
}
