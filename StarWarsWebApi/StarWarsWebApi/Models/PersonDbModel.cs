using StarWarsApiCSharp;

namespace StarWarsWebApi.Models
{
    public class PersonDbModel : Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }
    }
}
