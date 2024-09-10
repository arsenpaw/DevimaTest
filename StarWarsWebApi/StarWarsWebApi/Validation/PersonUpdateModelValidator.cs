using System.Text.RegularExpressions;
using FluentValidation;
using StarWarsWebApi.Models;

namespace StarWarsWebApi.Validation;

public class PersonUpdateModelValidator:AbstractValidator<PersonUpdateModel>
{
    private static readonly List<string> ALLOWED_GENDER = new List<string> {"Male", "Female", "n/a","hermaphrodite"};
    public PersonUpdateModelValidator()
    {
        RuleFor(x => x.Name).Length(1,80).WithMessage("Name shold be between 1 and 80 characters.");
        RuleFor(x => x.Gender).Must(gender => ALLOWED_GENDER.Contains(gender))
            .WithMessage($"Gender must be one of {String.Join(", ", ALLOWED_GENDER)}");
        RuleFor(x => x.Height).Must(height => 
            {
                bool isValid = Int32.TryParse(height, out int heightInCm);
                return isValid && heightInCm > 0;
            })
            .WithMessage("Height must be a valid number greater than zero");
        RuleFor(x => x.BirthYear)
            .Must(year =>
            {
                if (year == "unknown")
                    return true;
                var yearPattern = @"^\d+(\.\d+)?BBY$"; 
                return Regex.IsMatch(year, yearPattern) ;
            })
            .WithMessage("BirthYear must be 'unknown' or a valid Star Wars Year.");
        RuleFor(x => x.Mass)
            .Must(mass =>
            {
                if (mass == "unknown")
                    return true;
                bool isValid = Int32.TryParse(mass, out int weightInKg);
                return isValid && weightInKg > 0;
            })
            .WithMessage("Mass must be 'unknown' or a number.");
        RuleFor(x => x.Homeworld)
            .Matches(@"^https:\/\/swapi\.dev\/api\/planets\/(\d+)\/$")
            .WithMessage("HomeWorld must be an url to existing homeworld.");

    }
}