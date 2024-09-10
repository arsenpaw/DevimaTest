using StarWarsApiCSharp;
using StarWarsWebApi.Models;
using ErrorOr;
namespace StarWarsWebApi.Interaces
{
    public interface IPeopleRepository
    {
        Task WritePersonToDB(List<Person> person);
        Task<Person?> GetPeopleByIdOrDefault(Guid id);
        Task<Person?> GetPeopleByIdOrDefault(int ExternalId);
        Task WritePersonToDB(Person person, int id);

        Task<ErrorOr<PersonDbModel>> UpdatePersonToDB(PersonDbModel person);

        Task<ErrorOr<Deleted>> DeletePersonFromDB(Guid id);
    }
}