using StarWarsApiCSharp;

namespace StarWarsWebApi.Repositories
{
    public class SwapiPeapleRepo
    {
        IRepository<Person> _peopleRepo;
        public SwapiPeapleRepo(IRepository<Person> peopleRepo)
        {

            _peopleRepo = peopleRepo;

        }
        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return _peopleRepo.GetById(id);
        }
        public async Task<List<Person>> GetPersonListAsync(int page = 1, int onPageNum = 10)
        {
            return _peopleRepo.GetEntities(page,onPageNum).ToList();
        }



    }
}
