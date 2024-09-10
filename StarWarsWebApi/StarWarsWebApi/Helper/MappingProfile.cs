using AutoMapper;
using StarWarsApiCSharp;
using StarWarsWebApi.Models;

using StarWarsWebApi.Services;
using StarWarsWebApi.Utils;

namespace StarWarsWebApi.Helper
{
 
    public sealed class ExternalIdResolver : IValueResolver<Person, PersonDbModel, int?>
    {

        public int? Resolve(Person source, PersonDbModel destination, int? destMember, ResolutionContext context)
        {
            var response =  UrlParcer.GetIdFromUrl(source.Url);
            return response.IsError ? null : response.Value;
        }
    }

   
    public class MappingProfile : Profile
    {
 
        public MappingProfile()
        {
            CreateMap<Person, PersonDbModel>()
                    .ForMember(dest => dest.Films, opt => opt.MapFrom(x => x.Films))
                    .ForMember(dest => dest.Species, opt => opt.MapFrom(x => x.Species))
                    .ForMember(dest => dest.Starships, opt => opt.MapFrom(x => x.Starships))
                    .ForMember(dest => dest.Vehicles, opt => opt.MapFrom(x => x.Vehicles))
                    .ForMember(dest => dest.ExternalApiId, opt => opt.MapFrom<ExternalIdResolver>())
                    ;


            CreateMap<PersonDbModel, Person>();
            
            CreateMap<PersonUpdateModel, PersonDbModel>();
            
            CreateMap<PersonDbModel, PersonDbModel>()
                .ForMember(dest => dest.Films, opt => opt.Ignore())
                .ForMember(dest => dest.Species, opt => opt.Ignore())
                .ForMember(dest => dest.Starships, opt => opt.Ignore())
                .ForMember(dest => dest.Vehicles, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore());
        }
    }
}
