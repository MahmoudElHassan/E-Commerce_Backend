namespace E_Commerce_BL;

public interface IProductManager
{
    List<ReadProductDTO> GetAll();
    ReadProductDTO? GetById(Guid id);
    ReadProductDTO Add(AddProductDTO productDTO);
    bool Update(UpdateProductDTO productDTO);
    void Delete(Guid id);
}
