
using ErrorOr;
using System.Text.RegularExpressions;

namespace StarWarsWebApi.Services;

public class UrlParcer : IUrlParcer
{
    private readonly Regex _regex;
    ILogger<UrlParcer> _logger;
        
    public UrlParcer(ILogger<UrlParcer> logger)
    {
        _regex = new Regex(@"(\d+)/$", RegexOptions.Compiled);  
        _logger = logger;
    }

    public async Task<ErrorOr<int>> GetIdFromUrl(string url)
    {
        var match = _regex.Match(url);
        var id = match.Success ? int.Parse(match.Groups[1].Value) : 0;
        return match.Success ? id: Error.NotFound();

    }
}