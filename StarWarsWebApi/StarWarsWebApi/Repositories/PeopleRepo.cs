using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWarsApiCSharp;
using StarWarsWebApi.Interaces;
using StarWarsWebApi.Models;
using System;
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
           return  _context.Persons.Any(x => x.Name == name);
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
    }
}
