using FluentValidation;
using RCMS.Shared.Models.Courses;

namespace RCMS.Shared.Validators;

public class SaveCourseValidator : AbstractValidator<SaveCourseDto>
{
    public SaveCourseValidator()
    {
        RuleFor(scd => scd.CategoryId)
            .NotEmpty()
            .WithMessage("Category is required.");
        RuleFor(scd => scd.Name)
            .NotEmpty()
            .WithMessage("Name is required.");
    }
}