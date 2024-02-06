using App.Logic.Commands.AddCategory;
using FluentValidation;

namespace App.Logic.Validators;

public class CategoryValidator : AbstractValidator<AddCategoryCommand>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Category name cannot be empty");
    }
}