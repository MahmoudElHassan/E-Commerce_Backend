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
    public Task<IReadOnlyList<ReadProductDTO>> GetAll()
    {
        //var brand = _productRepo.GetAll().Result.Where(x => x.productBrand.Name);
        var dbProduct = _productRepo.GetAllEagerLoad().Result;

        return Task.FromResult(_mapper.Map<IReadOnlyList<ReadProductDTO>>(dbProduct));
    }

    public async Task<ReadProductDTO> GetById(int id)
    {
        var dbProduct = _productRepo.GetByIdEagerLoad(id).Result;

        if (dbProduct is null)
            return null;

        return await Task.FromResult(_mapper.Map<ReadProductDTO>(dbProduct));
    }

    public ReadProductDTO Add(AddProductDTO productDTO)
    {
        var dbModel = _mapper.Map<Product>(productDTO);

        //dbModel.Id = Guid.NewGuid();
        dbModel.IsDelete = false;

        _productRepo.Add(dbModel);
        _productRepo.SaveChanges();

        return _mapper.Map<ReadProductDTO>(dbModel);
    }

    public bool Update(UpdateProductDTO productDTO)
    {
        var dbModel = _productRepo.GetById(productDTO.Id);

        if (dbModel is null || dbModel.Result.IsDelete is true)
            return false;

        _mapper.Map(productDTO, dbModel);

        _productRepo.Update(dbModel.Result);
        _productRepo.SaveChanges();

        return true;
    }

    public void Delete(int id)
    {
        _productRepo.DeleteById(id);
        _productRepo.SaveChanges();
    }

    #endregion
}
