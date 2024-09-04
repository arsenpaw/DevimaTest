using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWarsApiCSharp;
using StarWarsWebApi.Interaces;
using StarWarsWebApi.Models;
using WebApplication.Context;

namespace StarWarsWebApi.Repositories
{
    public class PeopleRepo : IPeopleRepo
    {
        private readonly StarWarsContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PeopleRepo> _logger;

        public PeopleRepo(StarWarsContext context, IMapper mapper,
            ILogger<PeopleRepo> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;

        }
        private async Task<List<PersonDbModel>> DuplicateCheck(List<PersonDbModel> dataToDb)
        {
            var idsToCheck = dataToDb.Select(x => x.Name).ToList();


            var existingIds = await _context.Persons
                .Where(x => idsToCheck.Contains(x.Name))
                .Select(x => x.Name)
                .ToListAsync();


            var filteredDataToDb = dataToDb
                .Where(x => !existingIds.Contains(x.Name))
                .ToList();

            return filteredDataToDb;
        }
        public async Task<Person?> GetPeopleByIdOrDefault(Guid id)
        {
            
            var entity = await _context.Persons.
                FirstOrDefaultAsync(x => x.PrivateId == id);
            return entity != null ? _mapper.Map<PersonDbModel, Person>(entity) : null;


        }
        public async Task WritePersonToDB(List<Person> person)
        {
            var dbModelFromUser = _mapper.Map<List<Person>, List<PersonDbModel>>(person);
            var filteredData = await DuplicateCheck(dbModelFromUser);
            try
            {
                await _context.AddRangeAsync(filteredData);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                _logger.LogError("Error writing to database");
                throw;

            }
        }
    }
}
