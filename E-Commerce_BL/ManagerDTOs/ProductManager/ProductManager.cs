using AutoMapper;
using E_Commerce_DAL;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
    public async Task<IReadOnlyList<ReadProductDTO>> GetAll([FromQuery] ProductSpecParams productParams)
    {

        var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
        var countSpec = new ProductsWithFiltersForCountSpecification(productParams);

        var totalItems = await _productRepo.CountAsync(countSpec);
        var products = await _productRepo.ListAsync(spec);

        new Pagination<ReadProductDTO>(productParams.PageIndex, productParams.PageSize, totalItems, data);

        return _mapper.Map<IReadOnlyList<ReadProductDTO>>(products);

        //new Pagination<ReadProductDTO>(productParams.PageIndex, productParams.PageSize, totalItems, data);

        //var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

        //var brand = _productRepo.GetAll().Result.Where(x => x.productBrand.Name);
        //var dbProduct = _productRepo.GetAllEagerLoad().Result;

        //return Task.FromResult(_mapper.Map<IReadOnlyList<ReadProductDTO>>(dbProduct));
    }

    public async Task<ReadProductDTO> GetById(int id)
    {
        var dbProduct = _productRepo.GetByIdEagerLoad(id).Result;

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
