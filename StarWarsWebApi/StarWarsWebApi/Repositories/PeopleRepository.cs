using AutoMapper;

using Microsoft.EntityFrameworkCore;
using StarWarsApiCSharp;
using StarWarsWebApi.Interaces;
using StarWarsWebApi.Models;
using ErrorOr;
using WebApplication.Context;

namespace StarWarsWebApi.Repositories
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly StarWarsContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PeopleRepository> _logger;

        public PeopleRepository(StarWarsContext context, IMapper mapper,
            ILogger<PeopleRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;

        }
        private async Task<List<PersonDbModel>> DuplicateCheck(List<PersonDbModel> dataToDb)
        {
            var idsToCheck = dataToDb.Select(x => x.ExternalApiId).ToList();


            var existingIds = await _context.Persons
                .Where(x => idsToCheck.Contains(x.ExternalApiId))
                .Select(x => x.ExternalApiId)
                .ToListAsync();


            var filteredDataToDb = dataToDb
                .Where(x => !existingIds.Contains(x.ExternalApiId))
                .ToList();

            return filteredDataToDb;
        }
        public async Task<Person?> GetPeopleByIdOrDefault(Guid id)
        {

            var entity = await _context.Persons.
                FirstOrDefaultAsync(x => x.PrivateId == id);
            return entity != null ? _mapper.Map<PersonDbModel, Person>(entity) : null;
        }
        public async Task<Person?> GetPeopleByIdOrDefault(int ExternalId)
        {

            var entity = await _context.Persons.
                FirstOrDefaultAsync(x => x.ExternalApiId == ExternalId);
            return entity != null ? _mapper.Map<PersonDbModel, Person>(entity) : null;
        }
        private async Task AddExternalIdToEntity(string entityName, int externalApiId)
        {
            var entity = await _context.Persons
                .FirstOrDefaultAsync(x => x.Name == entityName);
            if (entity == null)
            {
                _logger.LogInformation("Entity not found in database");
                return;
            }
            try
            {
                entity.ExternalApiId = externalApiId;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                _logger.LogError("Error writing to database while add id");
                throw;
            }

        }
        private async Task<bool> IsPersonExist(string name)
        {
            return _context.Persons.Any(x => x.Name == name);
        }
        public async Task WritePersonToDB(Person person, int id)
        {

            if (await IsPersonExist(person.Name))
            {
                if (_context.Persons.Any(x => x.ExternalApiId == null))
                {
                    _logger.LogInformation("Person already exist in database, but ExternalApiId is null");
                    await AddExternalIdToEntity(person.Name, id);
                }
                _logger.LogInformation("Person already exist in database");
                return;
            }
            try
            {
                var dbModelFromUser = _mapper.Map<Person, PersonDbModel>(person);
                dbModelFromUser.ExternalApiId = id;
                await _context.Persons.AddAsync(dbModelFromUser);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                _logger.LogError("Error writing to database while insert new model");
                throw;

            }
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
                _logger.LogError("Error writing to database while insert list");
                throw;

            }
        }

        
        public async Task<ErrorOr<Updated>> UpdatePersonToDB(PersonDbModel person)
        {
            try
            {
                var entityToChange = await _context.Persons
                    .FirstOrDefaultAsync(x => x.PrivateId == person.PrivateId);
                if (entityToChange == null)
                    return Error.Failure("Person not found"); 
                _mapper.Map(person, entityToChange);
               await _context.SaveChangesAsync();

                return Result.Updated;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error writing to database while updating person");
                throw;
            }
        }

        public async Task<ErrorOr<Deleted>> DeletePersonFromDB(Guid id)
        {
            try
            {
                var entityToDelete = await _context.Persons
                    .FirstOrDefaultAsync(x => x.PrivateId == id);
                if (entityToDelete == null)
                    return Error.Failure("Person not found"); 
                _context.Persons.Remove(entityToDelete);
                var changes = await _context.SaveChangesAsync();
                if (changes == 0)
                    return Error.Failure("Person not updated");

                return Result.Deleted;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error writing to database while updating person");
                throw;
            }
        }

        
    }
}
