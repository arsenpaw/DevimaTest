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
        IPersonService _personService;
        ILogger<PeopleController> _logger;
        public PeopleController(IRepository<Person> repository, IPersonService personService, ILogger<PeopleController> logger)
        {
            _repository = repository;
            _personService = personService;
            _logger = logger;

        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int page = 1, int pcsPerPage = 10)
        {
           var responce =  await _personService.GetListOfDeviceAndWriteToDbAsync(page, pcsPerPage);
            if (responce.IsError)
            {
                return NotFound(responce.Errors);
            }
            return Ok(responce.Value);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var responce = await _personService.GetByIdWithLocalDbPriorityAsync(id);
            if (responce.IsError)
            {
                return NotFound(responce.Errors);
            }
            return Ok(responce.Value);

        }
    }
}
