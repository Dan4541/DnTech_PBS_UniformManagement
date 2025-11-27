using DnTech_PBS_UniformManagement.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace DnTech_PBS_UniformManagement.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(200)]
        [Display(Name = "Full Name")]
        public string Fullname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        [Display(Name = "ID Card")]
        public string? IdCard { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        public List<string> UserRoles { get; set; } = new();
        public List<string>? SelectedRoles { get; set; }
        public List<ApplicationRole> AvailableRoles { get; set; } = new();
    }
}
