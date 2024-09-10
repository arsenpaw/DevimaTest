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
        
        IPersonService _personService;
        ILogger<PeopleController> _logger;
        IPeopleRepository _peopleRepository;
        IMapper _mapper;
        public PeopleController(IPersonService personService, ILogger<PeopleController> logger,    IPeopleRepository peopleRepository,IMapper mapper)
        {
            _personService = personService;
            _logger = logger;
            _peopleRepository = peopleRepository;
            _mapper = mapper;   
        }

        [HttpGet]
        public async Task<ActionResult> GetList([FromQuery] int page = 1, int pcsPerPage = 82)
        {
           var responce =  await _personService.GetListOfDeviceAndWriteToDbAsync(page, pcsPerPage);
            if (responce.IsError)
            {
                return NotFound(responce.Errors);
            }
            return Ok(new { total = responce.Value.Count, data = responce.Value });

        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Person>> GetById(int id)
        {
            var responce = await _personService.GetByIdWithLocalDbPriorityAsync(id);
            if (responce.IsError)
            {
                return NotFound(responce.Errors);
            }
            return responce.Value;

        }
        
        [HttpGet("/fromLocal/{id:guid}")]
        public async Task<ActionResult<Person>> GetPeople(Guid id)
        {
            var responce = await _peopleRepository.GetPeopleByIdOrDefault(id);
            if (responce == null)
            {
                return NotFound();
            }
            return responce;
        }
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Person>> UpdatePeople([FromBody]PersonUpdateModel person,  Guid id)
        {
            var personDbModel = _mapper.Map<PersonUpdateModel, PersonDbModel>(person);
            personDbModel.PrivateId = id;
            var responce = await _peopleRepository.UpdatePersonToDB(personDbModel);
            if (responce.IsError)
            {
                return NotFound(responce.Errors);
            }
            return _mapper.Map<PersonDbModel, Person>(responce.Value) ;
        }
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeletePeople(Guid id)
        {
            var responce = await _peopleRepository.DeletePersonFromDB(id);
            if (responce.IsError)
            {
                return NotFound(responce.Errors);
            }
            return Ok();
        }
        
        
    }
}
