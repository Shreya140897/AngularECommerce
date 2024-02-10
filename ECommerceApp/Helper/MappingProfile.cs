using API.DTO;
using AutoMapper;
using ECommerceAPI.Entities;

namespace API.Helper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.ProductBrand, o => o.MapFrom(x => x.ProductBrand.Name))
                .ForMember(x => x.ProductType, o => o.MapFrom(x => x.ProductType.Name))
                .ForMember(x => x.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
                
        }
    }
}
