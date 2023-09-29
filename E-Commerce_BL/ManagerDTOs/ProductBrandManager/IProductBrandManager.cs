namespace E_Commerce_BL.ManagerDTOs;

public interface IProductBrandManager
{
    List<ReadProductBrandDTO> GetAll();
    ReadProductBrandDTO? GetById(Guid id);
    ReadProductBrandDTO Add(AddProductBrandDTO brandDTO);
    bool Update(UpdateProductBrandDTO brandDTO);
    void Delete(Guid id);
}
