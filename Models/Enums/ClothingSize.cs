using System.ComponentModel.DataAnnotations;

namespace DnTech_PBS_UniformManagement.Models.Enums
{
    public enum ClothingSize
    {
        [Display(Name = "XS")]
        XS,

        [Display(Name = "S")]
        S,

        [Display(Name = "M")]
        M,

        [Display(Name = "L")]
        L,

        [Display(Name = "XL")]
        XL,

        [Display(Name = "XXL")]
        XXL,

        [Display(Name = "XXXL")]
        XXXL,

        // Tallas numéricas para zapatos
        [Display(Name = "35")]
        Talla35,

        [Display(Name = "36")]
        Talla36,

        [Display(Name = "37")]
        Talla37,

        [Display(Name = "38")]
        Talla38,

        [Display(Name = "39")]
        Talla39,

        [Display(Name = "40")]
        Talla40,

        [Display(Name = "41")]
        Talla41,

        [Display(Name = "42")]
        Talla42,

        [Display(Name = "43")]
        Talla43,

        [Display(Name = "44")]
        Talla44,

        [Display(Name = "45")]
        Talla45
    }
}
