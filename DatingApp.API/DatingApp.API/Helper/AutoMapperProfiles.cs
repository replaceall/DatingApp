using AutoMapper;
using DatingApp.API.DTO;
using DatingApp.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Helper
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserModel, UserForDetailedDto>()
                .ForMember(dist =>dist.PhotoUrl, opt => opt.MapFrom(src =>src.Photos.FirstOrDefault(p=>p.IsMain).URL))
                .ForMember(dist => dist.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<UserModel, UserForListDto>()
                .ForMember(dist => dist.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).URL))
                .ForMember(dist => dist.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForRegisterDTO, UserModel>();
        }
    }
}
