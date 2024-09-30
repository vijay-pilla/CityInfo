using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;

namespace CityInfo.API.MappingProfile
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDto>()
                        .ForMember(dest => dest.PointsOfInterest, opt => opt.MapFrom(src => src.PointOfInterest));
            CreateMap<City, CityWithoutPointOfInterestDto>();
            CreateMap<City, CityDto>();
        }
    }
}
