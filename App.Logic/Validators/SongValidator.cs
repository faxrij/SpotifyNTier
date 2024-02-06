using App.Logic.Commands.AddSong;
using FluentValidation;

namespace App.Logic.Validators;

public class SongValidator : AbstractValidator<AddSongCommand>
{
    public SongValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty.");
        RuleFor(x => x.DurationInSeconds).NotEmpty().WithMessage("Song Duration cannot be empty");
        RuleFor(x => x.Lyrics).NotEmpty().WithMessage("Lyrics Cannot be Empty");
    }
}