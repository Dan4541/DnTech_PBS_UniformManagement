using System.ComponentModel.DataAnnotations;

namespace DnTech_PBS_UniformManagement.Models.Enums
{
    public enum DeliveryStatus
    {
        [Display(Name = "Not delivered")]
        NotDelivered,

        [Display(Name = "Delivered")]
        Delivered,

        [Display(Name = "Upcoming")]
        Upcoming
    }
}
