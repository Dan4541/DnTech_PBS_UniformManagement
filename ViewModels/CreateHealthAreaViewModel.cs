using System.ComponentModel.DataAnnotations;

namespace DnTech_PBS_UniformManagement.ViewModels
{
    public class CreateHealthAreaViewModel
    {
        [Required(ErrorMessage = "Health area name is required")]
        [StringLength(200)]
        [Display(Name = "Health Area Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(20)]
        [Display(Name = "Health Area Code")]
        public string? Code { get; set; }

        [Required]
        public int ProvinceId { get; set; }

        public string? ProvinceName { get; set; }
    }
}
