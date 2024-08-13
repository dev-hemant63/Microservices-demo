using AutoMapper;
using VMart.AuthAPI.Models;
using VMart.AuthAPI.Models.Dto;

namespace VMart.AuthAPI.Extentions
{
    public class MappingConfig: Profile
    {
        public static MapperConfiguration RegisterMap()
        {
            var config = new MapperConfiguration(config =>
            {
                config.CreateMap<UserRegistrationReq, ApplicationUser>();
                config.CreateMap<ApplicationUser,LoginResponseDto>();
                config.CreateMap<ApplicationUser, UserRegistrationReq>();
            });
            return config;
        }
    }
}
