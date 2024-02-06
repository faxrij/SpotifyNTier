using FluentValidation;

namespace App.Logic.Commands.AddCategory;

public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
{
    public AddCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Category name cannot be empty");
    }
}