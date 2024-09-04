using AutoMapper;
using StarWarsApiCSharp;
using StarWarsWebApi.Models;
using System.Diagnostics;

namespace StarWarsWebApi
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDbModel>()
                   .ForMember(dest => dest.Films, opt => opt.MapFrom(x => x.Films))
             .ForMember(dest => dest.Species, opt => opt.MapFrom(x => x.Species))
             .ForMember(dest => dest.Starships, opt => opt.MapFrom(x => x.Starships))
               .ForMember(dest => dest.Vehicles, opt => opt.MapFrom(x => x.Vehicles));


            CreateMap<PersonDbModel,Person>();
        }
    }
}
