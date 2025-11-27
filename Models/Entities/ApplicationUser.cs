using Microsoft.AspNetCore.Identity;

namespace DnTech_PBS_UniformManagement.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Fullname { get; set; } = string.Empty;
        public string? IdCard { get; set; }
        public bool Active { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
