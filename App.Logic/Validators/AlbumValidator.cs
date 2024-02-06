using App.Logic.Commands.AddAlbum;
using FluentValidation;

namespace App.Logic.Validators;

public class AlbumValidator : AbstractValidator<AddAlbumCommand>
{
    public AlbumValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty.");
        RuleFor(x => x.ReleaseYear).NotEmpty().WithMessage("Release year cannot be empty");
    }
}