using FluentValidation;

namespace App.Logic.Commands.UpdateSong;

public class UpdateSongCommandValidator : AbstractValidator<UpdateSongCommand>
{
    public UpdateSongCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty.");
        RuleFor(x => x.DurationInSeconds).NotEmpty().WithMessage("Song Duration cannot be empty");
        RuleFor(x => x.Lyrics).NotEmpty().WithMessage("Lyrics Cannot be Empty");
    }
}