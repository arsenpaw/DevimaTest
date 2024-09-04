using Microsoft.AspNetCore.Mvc;
using StarWarsApiCSharp;
using StarWarsWebApi.Interaces;
using StarWarsWebApi.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace StarWarsWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        IRepository<Person> _repository;
        IPeopleRepository _peopleRepo;
        ILogger<PeopleController> _logger;
        public PeopleController(IRepository<Person> repository, IPeopleRepository peopleRepo, ILogger<PeopleController> logger)
        {
            _repository = repository;
            _peopleRepo = peopleRepo;
            _logger = logger;

        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int page = 1, int pcsPerPage = 10)
        {
            var personCoolection = _repository.GetEntities(page, pcsPerPage);
            _logger.LogInformation("Getting people from API");
            if (personCoolection == null)
            {
                return NotFound();
            }
            await _peopleRepo.WritePersonToDB(personCoolection.ToList());
            _logger.LogInformation("Send responce to userfrom API");
            return Ok(personCoolection);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entityFromLocalDb = await _peopleRepo.GetPeopleByIdOrDefault(id);
            _logger.LogInformation("Is entity with id present on our db {entityFromLocalDb}", entityFromLocalDb);
            if (entityFromLocalDb != null)
            {
                return Ok(entityFromLocalDb);
            }
            var person = _repository.GetById(id);
            _logger.LogInformation("Get entity from external API ");
            if (person == null)
            {
                return NotFound();
            }
            await _peopleRepo.WritePersonToDB(person, id);
            _logger.LogInformation("Send responce to user from external API");
            return Ok(person);


        }
    }
}
