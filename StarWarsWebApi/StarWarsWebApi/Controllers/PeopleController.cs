using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StarWarsApiCSharp;
using StarWarsWebApi.Interaces;
using StarWarsWebApi.Models;
using StarWarsWebApi.Models.Dto;

namespace StarWarsWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        
        private readonly IPersonService _personService;
        private readonly  IPeopleRepository _peopleRepository;
        private readonly IMapper _mapper;
        public PeopleController(IPersonService personService, IPeopleRepository peopleRepository,IMapper mapper)
        {
            _personService = personService;
            _peopleRepository = peopleRepository;
            _mapper = mapper;   
        }

        [HttpGet]
        public async Task<ActionResult<PaginaedResponse<List<Person>>>> GetList([FromQuery] int page = 1, int pcsPerPage = 82)
        {
           var response =  await _personService.GetListOfDeviceAndWriteToDbAsync(page, pcsPerPage);
            if (response.IsError)
            {
                return NotFound(response.Errors);
            }
            return new PaginaedResponse<List<Person>> { Count = response.Value.Count, Data = response.Value };


        }

       
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Person>> GetById(int id)
        {
            var response = await _personService.GetByIdWithLocalDbPriorityAsync(id);
            if (response.IsError)
            {
                return NotFound(response.Errors);
            }
            return response.Value;

        }
        
        [HttpGet("/fromLocal/{id:guid}")]
        public async Task<ActionResult<Person>> GetPeople(Guid id)
        {
            var response = await _peopleRepository.GetPeopleByIdOrDefault(id);
            if (response == null)
            {
                return NotFound();
            }
            return response;
        }
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Person>> UpdatePeople([FromBody]PersonUpdateModel person,  Guid id)
        {
            var personDbModel = _mapper.Map<PersonUpdateModel, PersonDbModel>(person);
            personDbModel.PrivateId = id;
            var response = await _peopleRepository.UpdatePersonToDB(personDbModel);
            if (response.IsError)
            {
                return NotFound(response.Errors);
            }
            return _mapper.Map<PersonDbModel, Person>(response.Value) ;
        }
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeletePeople(Guid id)
        {
            var response = await _peopleRepository.DeletePersonFromDB(id);
            if (response.IsError)
            {
                return NotFound(response.Errors);
            }
            
            return Ok();
        }
        
        
    }
}
