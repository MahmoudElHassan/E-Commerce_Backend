using AutoMapper;
using E_Commerce_DAL;

namespace E_Commerce_BL;

public class ProductTypeManager : IProductTypeManager
{
    #region Field
    private readonly IProductTypeRepo _typeRepo;
    private readonly IMapper _mapper;
    #endregion

    #region Ctor
    public ProductTypeManager(IProductTypeRepo typeRepo, IMapper maapper)
    {
        _typeRepo = typeRepo;
        _mapper = maapper;
    }
    #endregion

    #region Method
    public List<ReadProductTypeDTO> GetAll()
    {
        var dbType = _typeRepo.GetAll().Where(d => d.IsDelete == false);

        return _mapper.Map<List<ReadProductTypeDTO>>(dbType);
    }

    public ReadProductTypeDTO? GetById(Guid id)
    {
        var dbType = _typeRepo.GetById(id);

        if (dbType == null)
            return null;

        return _mapper.Map<ReadProductTypeDTO>(dbType);
    }

    public ReadProductTypeDTO Add(AddProductTypeDTO typeDTO)
    {
        var dbModel = _mapper.Map<ProductType>(typeDTO);

        dbModel.Id = Guid.NewGuid();
        dbModel.IsDelete = false;

        _typeRepo.Add(dbModel);
        _typeRepo.SaveChanges();

        return _mapper.Map<ReadProductTypeDTO>(dbModel);
    }

    public bool Update(UpdateProductTypeDTO typeDTO)
    {
        var dbModel = _typeRepo.GetById(typeDTO.Id);

        if (dbModel == null)
            return false;

        if (dbModel.IsDelete == true)
            return false;

        _mapper.Map(typeDTO, dbModel);

        _typeRepo.Update(dbModel);
        _typeRepo.SaveChanges();

        return true;
    }

    public void Delete(Guid id)
    {
        _typeRepo.DeleteById(id);
        _typeRepo.SaveChanges();
    }
    #endregion
}
