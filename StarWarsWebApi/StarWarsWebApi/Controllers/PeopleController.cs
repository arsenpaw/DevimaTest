using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarWarsApiCSharp;
using StarWarsWebApi.Context;
using StarWarsWebApi.Interaces;
using StarWarsWebApi.Models;
using StarWarsWebApi.Models.Dto;

namespace StarWarsWebApi.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        
        private readonly IPersonService _personService;
        private readonly  IPeopleRepository _peopleRepository;
        private readonly IMapper _mapper;
        public readonly StarWarsContext _context;
        public PeopleController(IPersonService personService, IPeopleRepository peopleRepository,
            IMapper mapper, StarWarsContext context)
        {
            _personService = personService;
            _peopleRepository = peopleRepository;
            _mapper = mapper;   
            _context = context; 
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
        public async Task<ActionResult<Person>> UpdatePeople([FromBody]PersonUpdateModel person,  Guid id
        ,[FromServices] IValidator<PersonUpdateModel> personValidator )
        {
            var validationResult =  await personValidator.ValidateAsync(person);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            
            var personDbModel = _mapper.Map<PersonUpdateModel, PersonDbModel>(person);
            personDbModel.PrivateId = id;
            var response = await _peopleRepository.UpdatePersonToDB(personDbModel);
            
            if (response.IsError)
                return NotFound(response.Errors);
            
            await _context.SaveChangesAsync(); 
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
            await _context.SaveChangesAsync(); 
            return Ok();
        }
        
        
    }
}
