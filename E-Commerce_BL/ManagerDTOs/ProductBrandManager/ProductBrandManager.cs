﻿using AutoMapper;
using E_Commerce_BL.ManagerDTOs;
using E_Commerce_DAL;

namespace E_Commerce_BL;

public class ProductBrandManager : IProductBrandManager
{
    #region Field
    private readonly IProductBrandRepo _brandRepo;
    private readonly IMapper _mapper;
    #endregion

    #region Ctor
    public ProductBrandManager(IProductBrandRepo brandRepo, IMapper maapper)
    {
        _brandRepo = brandRepo;
        _mapper = maapper;
    }
    #endregion

    #region Method
    public async Task<IReadOnlyList<ReadProductBrandDTO>> GetAll()
    {
        var dbBrand = _brandRepo.GetAll().Result.Where(d => d.IsDelete == false);

        return await Task.FromResult(_mapper.Map<List<ReadProductBrandDTO>>(dbBrand));
    }

    public async Task<ReadProductBrandDTO>? GetById(int id)
    {
        var dbBrand = _brandRepo.GetById(id);

        if (dbBrand == null)
            return null;

        return await Task.FromResult(_mapper.Map<ReadProductBrandDTO>(dbBrand));
    }

    public ReadProductBrandDTO Add(AddProductBrandDTO dbBrand)
    {
        var dbModel = _mapper.Map<ProductBrand>(dbBrand);

        //dbModel.Id = int.Parse;
        dbModel.IsDelete = false;

        _brandRepo.Add(dbModel);
        _brandRepo.SaveChanges();

        return _mapper.Map<ReadProductBrandDTO>(dbModel);
    }

    public bool Update(UpdateProductBrandDTO dbBrand)
    {
        var dbModel = _brandRepo.GetById(dbBrand.Id);

        if (dbModel == null)
            return false;

        if (dbModel.Result.IsDelete == true)
            return false;

        _mapper.Map(dbBrand, dbModel);

        _brandRepo.Update(dbModel.Result);
        _brandRepo.SaveChanges();

        return true;
    }

    public void Delete(int id)
    {
        _brandRepo.DeleteById(id);
        _brandRepo.SaveChanges();
    }
    #endregion
}
