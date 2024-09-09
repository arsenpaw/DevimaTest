using AutoMapper;
using StarWarsApiCSharp;
using StarWarsWebApi.Models;
using System.Diagnostics;
using StarWarsWebApi.Services;

namespace StarWarsWebApi
{
 
    public sealed class ExternalIdResolver : AutoMapper.IValueResolver<Person, PersonDbModel, int?>
    {
        private readonly IUrlParcer _urlParcer;

        public ExternalIdResolver(IUrlParcer urlParcer)
        {
            _urlParcer = urlParcer;
        }

        public int? Resolve(Person source, PersonDbModel destination, int? destMember, ResolutionContext context)
        {
            
            var task = Task.Run(async () =>
            {
                var response = await _urlParcer.GetIdFromUrl(source.Url);
                return response;
            });

            var result = task.Result;

            return result.IsError ? null : result.Value;
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
                .ForMember(dest => dest.Edited, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore());
        }
    }
}
