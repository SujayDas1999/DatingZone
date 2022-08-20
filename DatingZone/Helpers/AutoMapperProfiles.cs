using AutoMapper;
using DatingZone.Entities;
using DatingZone.Entities.Dtos;
using DatingZone.Extensions;
using System.Linq;

namespace DatingZone.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(photo => photo.Photos.FirstOrDefault(x => x.IsMain).ImageUrl))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(user => user.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>();
        }
    }
}
