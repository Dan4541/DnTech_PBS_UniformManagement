using System.ComponentModel.DataAnnotations;

namespace DnTech_PBS_UniformManagement.Models.Enums
{
    public enum GarmentType
    {
        [Display(Name = "Shirt")]
        Shirt,

        [Display(Name = "Pants")]
        Pants,

        [Display(Name = "Blouse")]
        Blouse,

        [Display(Name = "Skirt")]
        Skirt,

        [Display(Name = "Shoes")]
        Shoes,

        [Display(Name = "Apron")]
        Apron,

        [Display(Name = "Cap")]
        Cap,

        [Display(Name = "Vest")]
        Vest,

        [Display(Name = "Others")]
        Others
    }
}
