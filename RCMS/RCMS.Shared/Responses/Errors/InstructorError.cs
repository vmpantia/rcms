using RCMS.Shared.Enumerations;

namespace RCMS.Shared.Responses.Errors;

public class InstructorError
{
    public static Error NotFound(Guid id) => new(ErrorType.NotFound, $"Instructor with an ID of {id} is not found in the database.");
}