using FluentValidation;

namespace App.Logic.Commands.AddAlbum;

public class AddAlbumCommandValidator : AbstractValidator<AddAlbumCommand>
{
    public AddAlbumCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty.");
        RuleFor(x => x.ReleaseYear).NotEmpty().WithMessage("Release year cannot be empty");
    }
}