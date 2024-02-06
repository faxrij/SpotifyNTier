using FluentValidation;

namespace App.Logic.Commands.UpdateSinger;

public class UpdateSingerCommandValidator : AbstractValidator<UpdateSingerCommand>
{
    public UpdateSingerCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Category name cannot be empty");
        RuleFor(x => x.BirthDate).Must(BeValidBirthDate).WithMessage("Invalid birth date");
    }

    private bool BeValidBirthDate(DateTime birthDate)
    {
        var maxBirthDate = DateTime.Today.AddYears(-15);
        var minBirthDate = DateTime.Today.AddYears(-70);

        return birthDate >= minBirthDate && birthDate <= maxBirthDate;
    }    
}