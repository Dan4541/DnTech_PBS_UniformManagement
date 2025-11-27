using Microsoft.AspNetCore.Identity;

namespace DnTech_PBS_UniformManagement.Models.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
