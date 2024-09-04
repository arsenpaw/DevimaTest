using StarWarsApiCSharp;

namespace StarWarsWebApi.Interaces
{
    public interface IPeopleRepo
    {
        Task WritePersonToDB(List<Person> person);
        Task<Person> GetPeopleByIdOrDefault(Guid id);
        Task<Person> GetPeopleByIdOrDefault(int ExternalId);
        Task WritePersonToDB(Person person, int id);
    }
}