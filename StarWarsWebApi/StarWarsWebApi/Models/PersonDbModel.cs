using StarWarsApiCSharp;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StarWarsWebApi.Models
{
    public class PersonDbModel : Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PrivateId { get; init; }
    }
}
