using E_Commerce_DAL;

namespace E_Commerce_BL;

public class ReadProductDTO 
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? PictureURL { get; set; }
    public Guid ProductTypeId { get; set; }
    public Guid ProductBrandId { get; set; }
    public bool IsDelete { get; set; }
}
