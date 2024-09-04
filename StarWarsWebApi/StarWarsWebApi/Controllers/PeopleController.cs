using Microsoft.AspNetCore.Mvc;
using StarWarsApiCSharp;
using StarWarsWebApi.Interaces;
using Swashbuckle.AspNetCore.Swagger;

namespace StarWarsWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        IRepository<Person> _repository;
        IPeopleRepo _peopleRepo;
        public PeopleController(IRepository<Person> repository, IPeopleRepo peopleRepo)
        {
            _repository = repository;
            _peopleRepo = peopleRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery]int page = 1, int pcsPerPage = 10)
        {
           var personList =  _repository.GetEntities(page, pcsPerPage).ToList();  
           await _peopleRepo.WritePersonToDB(personList);
            return Ok(personList);
        }
        [HttpGet("localdb{id:guid}")]
        public async Task<IActionResult> GetLocal(Guid id)
        {
            var entity = await _peopleRepo.GetPeapleByIdOrDefault(id);
            return entity != null ? NotFound() : Ok();           
        }
    }
}
