using RCMS.Shared.Enumerations;

namespace RCMS.Shared.Responses.Errors;

public class StudentError
{
    public static Error NotFound(Guid id) => new(ErrorType.NotFound, $"Student with an ID of {id} is not found in the database.");
}