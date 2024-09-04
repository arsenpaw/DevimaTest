using Microsoft.AspNetCore.Mvc;
using StarWarsWebApi.Interaces;
using Swashbuckle.AspNetCore.Swagger;

namespace StarWarsWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        ISwapiPeapleRepo _swapiPeapleRepo;
        public PeopleController(ISwapiPeapleRepo swapiPeapleRepo)
        {
            _swapiPeapleRepo = swapiPeapleRepo;
        }

        [HttpGet("{page:int}/{pcsPerPage:int}")]
        public async Task<IActionResult> GetList(           
            int page = 1, int pcsPerPage = 10
        )
        {
           var personList = await  _swapiPeapleRepo.GetPersonListAsync(page, pcsPerPage);  
        }
    }
}
