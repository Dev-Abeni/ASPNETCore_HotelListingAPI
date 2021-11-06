using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDisplayDto>().ReverseMap();
            CreateMap<Country, CountryCreateDto>().ReverseMap();
            CreateMap<Hotel, HotelDisplayDto>().ReverseMap();
            CreateMap<Hotel, HotelFormDto>().ReverseMap();
            CreateMap<ApiUser, UserDto>().ReverseMap();
        }
    }
}
