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
                .ForMember(d => d.Username, opt => opt.MapFrom(s => s.User.Username))
                .ForMember(d => d.PhotoUrl, opt => opt.MapFrom(d => d.User.Photos.FirstOrDefault(up => up.IsMain == true).Url));

            CreateMap<PostForDetailedDto, Post>();
            CreateMap<PostForAddDto, Post>();
            CreateMap<Post, PostForDetailedDto>()
                .ForMember(d => d.Answers, opt => opt.MapFrom(s => s.Answers))
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.User.Id))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.User.Username))
                .ForMember(d => d.UserPhotoUrl, opt => opt.MapFrom(p => p.User.Photos.FirstOrDefault(up => up.IsMain == true).Url));
            CreateMap<Post, PostForListDto>()
                .ForMember(d => d.Answer, opt => opt.MapFrom(s => s.Answers.OrderByDescending(a => a.Like).FirstOrDefault()))
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.User.Id))
                .ForMember(d => d.Username, opt => opt.MapFrom(s => s.User.Username))
                .ForMember(d => d.PhotoUrl, opt => opt.MapFrom(p => p.Photos.FirstOrDefault(ph => ph.IsMain == true).Url))
                .ForMember(d => d.UserPhotoUrl, opt => opt.MapFrom(p => p.User.Photos.FirstOrDefault(up => up.IsMain == true).Url))
                .ForMember(d => d.AnswerCount, opt => opt.MapFrom(p => p.Answers.Count()));
            CreateMap<Post, PostForTrendingDto>()
                .ForMember(d => d.AnswerCount, opt => opt.MapFrom(p => p.Answers.Count()))
                .ForMember(d => d.PhotoUrl, opt => opt.MapFrom(p => p.Photos.FirstOrDefault(ph => ph.IsMain == true).Url))
                .ForMember(d => d.UserPhotoUrl, opt => opt.MapFrom(p => p.User.Photos.FirstOrDefault(up => up.IsMain == true).Url));

            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessageToReturnDto>()
                .ForMember(m => m.SenderPhotoUrl, opt => opt
                    .MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain == true).Url))
                .ForMember(m => m.RecipientPhotoUrl, opt => opt
                    .MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain == true).Url));

            CreateMap<PostedPhoto, PhotoForReturnDto>();
            CreateMap<UserPhoto, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, UserPhoto>();
        }
    }
}