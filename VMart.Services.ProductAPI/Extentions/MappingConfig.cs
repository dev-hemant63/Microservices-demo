using AutoMapper;

namespace VMart.Services.ProductAPI.Extentions
{
    public class MappingConfig : Profile
    {
        public static MapperConfiguration RegisterMap()
        {
            var config = new MapperConfiguration(config =>
            {
                
            });
            return config;
        }
    }
}
