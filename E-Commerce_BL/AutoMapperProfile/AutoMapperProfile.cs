using AutoMapper;
using E_Commerce_DAL;

namespace E_Commerce_BL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ReadProductDTO>()
            .ForMember(x => x.productBrand, z => z.MapFrom(v => v.productBrand.Name))
            .ForMember(x => x.productType, z => z.MapFrom(v => v.productType.Name))
            .ForMember(x => x.PictureURL, z => z.MapFrom<ProducrAPIResolver>());

        CreateMap<AddProductDTO, Product>();
        CreateMap<UpdateProductDTO, Product>();

        CreateMap<ProductType, ReadProductTypeDTO>();
        CreateMap<AddProductTypeDTO, ProductType>();
        CreateMap<UpdateProductTypeDTO, ProductType>();

        CreateMap<ProductBrand, ReadProductBrandDTO>();
        CreateMap<AddProductBrandDTO, ProductBrand>();
        CreateMap<UpdateProductBrandDTO, ProductBrand>();

        CreateMap<CustomerBasketDto, CustomerBasket>();
        CreateMap<BasketItemDto, BasketItem>();

        CreateMap<Address, AddressDto>().ReverseMap();


    }
}
