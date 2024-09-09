using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StarWarsApiCSharp;
using StarWarsWebApi.Controllers;
using StarWarsWebApi.Interaces;
using ErrorOr;


namespace StarWarsWebApi.Services
{
    public class PersonService : IPersonService
    {
        IRepository<Person> _repository;
        IPeopleRepository _peopleRepo;
        ILogger<PeopleController> _logger;
        IUrlParcer _urlParcer;
        public PersonService(IRepository<Person> repository, IPeopleRepository peopleRepo, ILogger<PeopleController> logger, IUrlParcer urlParcer)
        {
            _urlParcer = urlParcer;
            _repository = repository;
            _peopleRepo = peopleRepo;
            _logger = logger;

        }

        public async Task<ErrorOr<Person>> GetByIdWithLocalDbPriorityAsync(int id)
        {
            var entityFromLocalDb = await _peopleRepo.GetPeopleByIdOrDefault(id);
            _logger.LogInformation("Is entity with id present on our db {entityFromLocalDb}", entityFromLocalDb);
            if (entityFromLocalDb != null)
            {
                return (entityFromLocalDb);
            }
            var person = _repository.GetById(id);
            _logger.LogInformation("Get entity from external API ");
            if (person == null)
            {
                return Error.NotFound();
            }
            await _peopleRepo.WritePersonToDB(person, id);
            _logger.LogInformation("Send responce to user from external API");
            return (person);
        }


        private async Task<ErrorOr<string>> GetPersonIdFromUrl(string url)
        {
            return Error.NotFound();
        }

        public async Task<ErrorOr<List<Person>>> GetListOfDeviceAndWriteToDbAsync(int page , int pcsPerPage)
        {
            _logger.LogInformation("Retrieving people from repository");

            ICollection<Person> personCollection = _repository.GetEntities(page, pcsPerPage).ToList();

            if (personCollection == null || !personCollection.Any())
            {
                _logger.LogWarning("No people found");
                return Error.NotFound("No people found");
            }

            _logger.LogInformation("Writing people to the database");
            var personList = personCollection.ToList();
            
            await _peopleRepo.WritePersonToDB(personList);

            _logger.LogInformation("Returning person collection to the user");
            return personList;

        }

    }
}
