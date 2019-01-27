using AutoMapper;
using IttFelTeheted.API.Dtos;
using IttFelTeheted.API.Models;

namespace IttFelTeheted.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // CreateMap<User, UserForDetailedDto>()
            //     .ForMember(dest => dest.PhotoUrl, opt => {
            //         opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            //     });
        }
    }
}