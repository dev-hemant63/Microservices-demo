using AutoMapper;
using VMart.Services.Order.Models;
using VMart.Services.Order.Models.Dto;

namespace VMart.Services.Order.Extention
{
    public class MappingConfig:Profile
    {
        public static MapperConfiguration RegisterMap()
        {
            var config = new MapperConfiguration(config =>
            {
                config.CreateMap<OrderDetailDto, OrderDetails>();
                config.CreateMap<OrderDetails, OrderDetailDto>();
            });
            return config;
        }
    }
}
