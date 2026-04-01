using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnTech_PBS_UniformManagement.Models.Entities
{
    public class DeliveryDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Entrega")]
        public int UniformDeliveryId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tipo de Prenda")]
        public string GarmentType { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        [Display(Name = "Talla")]
        public string Size { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Cantidad")]
        [Range(1, 100, ErrorMessage = "La cantidad debe estar entre 1 y 100")]
        public int Quantity { get; set; } = 1;

        // Navigation properties
        [ForeignKey("UniformDeliveryId")]
        public virtual UniformDelivery? UniformDelivery { get; set; }
    }
}
