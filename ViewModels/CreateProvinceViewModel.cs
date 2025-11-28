using System.ComponentModel.DataAnnotations;

namespace DnTech_PBS_UniformManagement.ViewModels
{
    public class CreateProvinceViewModel
    {
        [Required(ErrorMessage = "Province name is required")]
        [StringLength(100)]
        [Display(Name = "Province Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(20)]
        [Display(Name = "Province Code")]
        public string? Code { get; set; }
    }
}
