namespace E_Commerce_BL;

public interface IProductTypeManager
{
    List<ReadProductTypeDTO> GetAll();
    ReadProductTypeDTO? GetById(Guid id);
    ReadProductTypeDTO Add(AddProductTypeDTO typeDTO);
    bool Update(UpdateProductTypeDTO typeDTO);
    void Delete(Guid id);
}
