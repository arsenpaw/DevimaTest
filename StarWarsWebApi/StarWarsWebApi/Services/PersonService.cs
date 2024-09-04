using AutoMapper;

namespace StarWarsWebApi.Services
{
    public class PersonService
    {


        private readonly IMapper _mapper;
        private readonly ILogger<PersonService> _logger;

        public PersonService(IMapper mapper,
            ILogger<PersonService> logger)
        {
            _mapper = mapper;
            _logger = logger;

        }
    }
}
