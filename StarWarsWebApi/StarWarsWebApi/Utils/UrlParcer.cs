
using ErrorOr;
using System.Text.RegularExpressions;
using StarWarsWebApi.Services;

namespace StarWarsWebApi.Utils;

public static class UrlParcer 
{

    public static  ErrorOr<int> GetIdFromUrl(string url)
    {
        var regex = new Regex(@"(\d+)/$", RegexOptions.Compiled);  
        var match = regex.Match(url);
        var id = match.Success ? int.Parse(match.Groups[1].Value) : 0;
        return match.Success ? id: Error.NotFound();

    }
}