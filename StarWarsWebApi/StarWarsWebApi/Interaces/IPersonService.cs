using ErrorOr;
using StarWarsApiCSharp;

namespace StarWarsWebApi.Interaces
{
    public interface IPersonService
    {
        Task<ErrorOr<Person>> GetByIdWithLocalDbPriorityAsync(int id);
        Task<ErrorOr<List<Person>>> GetListOfDeviceAndWriteToDbAsync(int page = 1, int pcsPerPage = 10);
    }
}