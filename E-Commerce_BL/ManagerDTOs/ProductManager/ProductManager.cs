using AutoMapper;
using E_Commerce_DAL;

namespace E_Commerce_BL;

public class ProductManager : IProductManager
{
    #region Field
    private readonly IProductRepo _productRepo;
    private readonly IMapper _mapper;
    #endregion

    #region Ctor
    public ProductManager(IProductRepo productRepo, IMapper maapper)
    {
        _productRepo = productRepo;
        _mapper = maapper;
    }
    #endregion

    #region Method
    public List<ReadProductDTO> GetAll()
    {
        var dbProduct = _productRepo.GetAll().Where(d => d.IsDelete == false);

        return _mapper.Map<List<ReadProductDTO>>(dbProduct);
    }

    public ReadProductDTO? GetById(Guid id)
    {
        var dbProduct = _productRepo.GetById(id);

        if (dbProduct == null)
            return null;

        return _mapper.Map<ReadProductDTO>(dbProduct);
    }

    public ReadProductDTO Add(AddProductDTO productDTO)
    {
        var dbModel = _mapper.Map<Product>(productDTO);

        dbModel.Id = Guid.NewGuid();
        dbModel.IsDelete = false;

        _productRepo.Add(dbModel);
        _productRepo.SaveChanges();

        return _mapper.Map<ReadProductDTO>(dbModel);
    }

    public bool Update(UpdateProductDTO productDTO)
    {
        var dbModel = _productRepo.GetById(productDTO.Id);

        if (dbModel == null)
            return false;

        if (dbModel.IsDelete == true)
            return false;

        _mapper.Map(productDTO, dbModel);

        _productRepo.Update(dbModel);
        _productRepo.SaveChanges();

        return true;
    }

    public void Delete(Guid id)
    {
        _productRepo.DeleteById(id);
        _productRepo.SaveChanges();
    }

    #endregion
}
