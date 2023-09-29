using System.ComponentModel.DataAnnotations;

namespace E_Commerce_DAL;

public class ProductBrand : BascEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;


    public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();

}