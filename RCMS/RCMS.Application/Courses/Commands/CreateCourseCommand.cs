using AutoMapper;
using FluentValidation;
using MediatR;
using RCMS.Domain.Entities;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Shared.Enumerations;
using RCMS.Shared.Models.Courses;
using RCMS.Shared.Responses;
using RCMS.Shared.Validators;

namespace RCMS.Application.Courses.Commands;

public sealed record CreateCourseCommand(SaveCourseDto Course) : IRequest<Result<Guid>>;

public sealed class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator(ICourseRepository courseRepository, ICourseCategoryRepository courseCategoryRepository)
    {
        RuleFor(ccc => ccc.Course)
            .SetValidator(new SaveCourseValidator());

        RuleFor(ccc => ccc.Course)
            .MustAsync(async (scd, ct) =>
            {
                // Check if the data already exists on the database
                var result = await courseRepository.IsExistAsync(
                    expression: c => c.Name == scd.Name,
                    cancellationToken: ct);
                return !result;
            })
            .WithMessage("Course name is already exist in the database.");

        RuleFor(ccc => ccc.Course)
            .MustAsync(async (scd, ct) =>
            {
                // Check if the data already exists on the database
                var result = await courseCategoryRepository.IsExistAsync(
                    expression: cc => cc.Id == Guid.Parse(scd.CategoryId) && cc.Status == CourseCategoryStatus.Active,
                    cancellationToken: ct);
                return !result;
            })
            .WithMessage("Course category is NOT exist or NOT active in the database.");
    }
}

public sealed class CreateCourseCommandHandler(ICourseRepository courseRepository, IMapper mapper) : IRequestHandler<CreateCourseCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        // Map data to entity
        var entity = mapper.Map<Course>(request.Course);

        // Create data on the database
        var result = await courseRepository.CreateAsync(entity, cancellationToken);

        return result.Id;
    }
}