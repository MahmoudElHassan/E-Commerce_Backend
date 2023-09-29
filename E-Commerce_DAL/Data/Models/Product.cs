using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_DAL;

public class Product : BascEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    //[Range(18,2)]
    public decimal Price { get; set; } = decimal.Zero;

    [Required]
    public string PictureURL { get; set; } = string.Empty;

    [ForeignKey("productType")]
    public Guid ProductTypeId { get; set; }
    public virtual ProductType? productType { get; set; }

    [ForeignKey("productBrand")]
    public Guid ProductBrandId { get; set; }
    public virtual ProductBrand? productBrand { get; set; }

}
