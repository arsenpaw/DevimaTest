using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using StarWarsApiCSharp;
using StarWarsWebApi.Interaces;
using StarWarsWebApi.Models;
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
        IPeopleRepository _peopleRepository;
        IMapper _mapper;
        public PeopleController(IRepository<Person> repository, IPersonService personService, ILogger<PeopleController> logger,    IPeopleRepository peopleRepository,IMapper mapper)
        {
            _repository = repository;
            _personService = personService;
            _logger = logger;
            _peopleRepository = peopleRepository;
            _mapper = mapper;   
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int page = 1, int pcsPerPage = 82)
        {
           var responce =  await _personService.GetListOfDeviceAndWriteToDbAsync(page, pcsPerPage);
            if (responce.IsError)
            {
                return NotFound(responce.Errors);
            }
            return Ok(new { total = responce.Value.Count, data = responce.Value });

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
        
        [HttpGet("/fromLocal/{id:guid}")]
        public async Task<ActionResult<IList<Person>>> GetPeople(Guid id)
        {
            var responce = await _peopleRepository.GetPeopleByIdOrDefault(id);
            if (responce == null)
            {
                return NotFound();
            }
            return Ok(responce);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<IList<Person>>> UpdatePeople([FromBody]PersonUpdateModel person,  Guid id)
        {
            var personDbModel = _mapper.Map<PersonUpdateModel, PersonDbModel>(person);
            personDbModel.PrivateId = id;
            var responce = await _peopleRepository.UpdatePersonToDB(personDbModel);
            if (responce.IsError)
            {
                return NotFound(responce.Errors);
            }
            return Ok(responce.Value);
        }
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<IList<Person>>> DeletePeople(Guid id)
        {
            var responce = await _peopleRepository.DeletePersonFromDB(id);
            if (responce.IsError)
            {
                return NotFound(responce.Errors);
            }
            return Ok(responce.Value);
        }
        
        
    }
}
