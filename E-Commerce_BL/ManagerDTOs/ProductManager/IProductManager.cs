using E_Commerce_DAL;

namespace E_Commerce_BL;

public interface IProductManager
{
    Task<IReadOnlyList<ReadProductDTO>> GetAll();
    Task<ReadProductDTO>? GetById(int id);
    ReadProductDTO Add(AddProductDTO productDTO);
    bool Update(UpdateProductDTO productDTO);
    void Delete(int id);
}
