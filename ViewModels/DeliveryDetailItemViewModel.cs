using System.ComponentModel.DataAnnotations;

namespace DnTech_PBS_UniformManagement.ViewModels
{
    public class DeliveryDetailItemViewModel
    {
        [Required(ErrorMessage = "El tipo de prenda es requerido")]
        [Display(Name = "Tipo de Prenda")]
        public string GarmentType { get; set; } = string.Empty;

        [Required(ErrorMessage = "La talla es requerida")]
        [Display(Name = "Talla")]
        public string Size { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cantidad es requerida")]
        [Display(Name = "Cantidad")]
        [Range(1, 100, ErrorMessage = "La cantidad debe estar entre 1 y 100")]
        public int Quantity { get; set; } = 1;
    }
}
