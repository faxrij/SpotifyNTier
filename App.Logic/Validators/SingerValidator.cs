using App.Logic.Commands.AddSinger;
using FluentValidation;

namespace App.Logic.Validators;

public class SingerValidator : AbstractValidator<AddSingerCommand>
{
    public SingerValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Category name cannot be empty");
        RuleFor(x => x.BirthDate).Must(BeValidBirthDate).WithMessage("Invalid birth date");
    }

    private bool BeValidBirthDate(DateTime birthDate)
    {
        var maxBirthDate = DateTime.Today.AddYears(-15);
        var minBirthDate = DateTime.Today.AddYears(-50);

        return birthDate >= minBirthDate && birthDate <= maxBirthDate;
    }    
}