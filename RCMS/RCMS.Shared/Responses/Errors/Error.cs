using RCMS.Shared.Enumerations;

namespace RCMS.Shared.Responses.Errors;

public sealed record Error(ErrorType Type, string Message, object? Value = null);