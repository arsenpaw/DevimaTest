using StarWarsApiCSharp;

namespace StarWarsWebApi.Repositories
{
    public interface ISwapiPeapleRepo
    {
        Task<Person> GetPersonByIdAsync(int id);
        Task<List<Person>> GetPersonListAsync(int page = 1, int onPageNum = 10);
    }
}