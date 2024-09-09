using ErrorOr;

namespace StarWarsWebApi.Services;

public interface IUrlParcer
{
    Task<ErrorOr<int>> GetIdFromUrl(string url);
}