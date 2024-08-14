using AutoMapper;
using VMart.Services.ProductAPI.Models;
using VMart.Services.ProductAPI.Models.Dto;

namespace VMart.Services.ProductAPI.Extentions
{
    public class MappingConfig : Profile
    {
        public static MapperConfiguration RegisterMap()
        {
            var config = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Products>();
                config.CreateMap<Products, ProductDto>();
            });
            return config;
        }
    }
}
