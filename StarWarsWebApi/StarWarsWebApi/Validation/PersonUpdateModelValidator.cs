using FluentValidation;
using StarWarsWebApi.Models;

namespace StarWarsWebApi.Validation;

public class PersonUpdateModelValidator:AbstractValidator<PersonUpdateModel>
{
    public PersonUpdateModelValidator()
    {
        RuleFor(x => x.Name).Length(1,80).WithMessage("Name is too long");
    }
}