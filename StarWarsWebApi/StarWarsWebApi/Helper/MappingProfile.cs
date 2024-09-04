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
            CreateMap<Person, PersonDbModel>();

            CreateMap<PersonDbModel,Person>();
        }
    }
}
