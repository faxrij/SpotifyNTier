using FluentValidation;

namespace App.Logic.Commands.UpdateAlbum;

public class UpdateAlbumCommandValidator :  AbstractValidator<UpdateAlbumCommand>
{
    public UpdateAlbumCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Album Title cannot be empty");
        RuleFor(x => x.ReleaseYear).NotEmpty().WithMessage("Album Release Year cannot be empty");
    }
}