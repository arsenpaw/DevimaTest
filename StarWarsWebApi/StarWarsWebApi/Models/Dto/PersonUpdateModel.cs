using Newtonsoft.Json;

namespace StarWarsWebApi.Models;

public class PersonUpdateModel
{
    [JsonProperty]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "birth_year")]
    public string BirthYear { get; set; }

    [JsonProperty(PropertyName = "eye_color")]
    public string EyeColor { get; set; }

    [JsonProperty]
    public string Gender { get; set; }

    [JsonProperty(PropertyName = "hair_color")]
    public string HairColor { get; set; }

    [JsonProperty]
    public string Height { get; set; }

    [JsonProperty]
    public string Mass { get; set; }

    [JsonProperty(PropertyName = "skin_color")]
    public string SkinColor { get; set; }
    
    [JsonProperty]
    public string Homeworld { get; set; }
}