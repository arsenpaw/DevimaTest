using StarWarsApiCSharp;

namespace StarWarsWebApi.Interaces
{
    public interface IPeopleRepo
    {
        Task WritePersonToDB(List<Person> person);
        Task<Person> GetPeopleByIdOrDefault(Guid id);
    }
}