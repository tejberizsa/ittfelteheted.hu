using System.Linq;
using AutoMapper;
using IttFelTeheted.API.Dtos;
using IttFelTeheted.API.Models;

namespace IttFelTeheted.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegisterDto, User>();
            CreateMap<User, UserForListDto>()
                .ForMember(u => u.PhotoUrl, opt => opt.MapFrom(p => p.Photos.FirstOrDefault(ph => ph.IsMain == true).Url));
            CreateMap<User, UserForDetailedDto>()
                .ForMember(u => u.PhotoUrl, opt => opt.MapFrom(p => p.Photos.FirstOrDefault(ph => ph.IsMain == true).Url));
            CreateMap<UserForUpdateDto, User>();

            CreateMap<AnswerForAddDto, Answer>();
            CreateMap<Answer, AnswerForDetailedDto>()
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.User.Id))
                .ForMember(d => d.Username, opt => opt.MapFrom(s => s.User.Username));

            CreateMap<PostForDetailedDto, Post>();
            CreateMap<PostForAddDto, Post>();
            CreateMap<Post, PostForDetailedDto>()
                .ForMember(d => d.Answers, opt => opt.MapFrom(s => s.Answers))
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.User.Id))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.User.Username));
            CreateMap<Post, PostForListDto>()
                .ForMember(d => d.Answer, opt => opt.MapFrom(s => s.Answers.OrderByDescending(a => a.Like).FirstOrDefault()))
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.User.Id))
                .ForMember(d => d.Username, opt => opt.MapFrom(s => s.User.Username))
                .ForMember(d => d.PhotoUrl, opt => opt.MapFrom(p => p.Photos.FirstOrDefault(ph => ph.IsMain == true).Url));
        }
    }
}