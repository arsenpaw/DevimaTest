
using ErrorOr;
using System.Text.RegularExpressions;
using StarWarsWebApi.Services;

namespace StarWarsWebApi.Utils;

public static class UrlParcer 
{
    private static readonly Regex _regex = new Regex(@"(\d+)/$", RegexOptions.Compiled); 
    public static  ErrorOr<int> GetIdFromUrl(string url)
    {
       
        var match = _regex.Match(url);
        var id = match.Success ? int.Parse(match.Groups[1].Value) : 0;
        return match.Success ? id: Error.NotFound();

    }
}