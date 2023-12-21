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
    public async Task<IReadOnlyList<ReadProductDTO>> GetAll(string sort, int? brandId, int? typeId)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(sort,brandId,typeId);

        var dbProduct = await _productRepo.ListAsync(spec);

        return _mapper.Map<IReadOnlyList<ReadProductDTO>>(dbProduct);
    }

    public async Task<ReadProductDTO> GetById(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);

        var dbProduct = await _productRepo.GetEntityWithSpec(spec);

        return _mapper.Map<ReadProductDTO>(dbProduct);
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
        var dbModel = _productRepo.GetByIdAsync(productDTO.Id);

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
