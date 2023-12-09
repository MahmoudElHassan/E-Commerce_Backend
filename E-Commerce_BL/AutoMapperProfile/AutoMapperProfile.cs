using AutoMapper;
using E_Commerce_DAL;

namespace E_Commerce_BL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ReadProductDTO>();
        CreateMap<AddProductDTO, Product>();
        CreateMap<UpdateProductDTO, Product>();

        CreateMap<ProductType, ReadProductTypeDTO>();
        CreateMap<AddProductTypeDTO, ProductType>();
        CreateMap<UpdateProductTypeDTO, ProductType>();

        CreateMap<ProductBrand, ReadProductBrandDTO>();
        CreateMap<AddProductBrandDTO, ProductBrand>();
        CreateMap<UpdateProductBrandDTO, ProductBrand>();

    }
}
