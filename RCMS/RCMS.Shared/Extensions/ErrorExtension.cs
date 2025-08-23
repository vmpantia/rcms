using Newtonsoft.Json.Linq;
using RCMS.Shared.Enumerations;
using RCMS.Shared.Responses.Errors;

namespace RCMS.Shared.Extensions;

public static class ErrorExtension
{
    public static string GetMessage(this Error? error)
    {
        if (error is { Type: ErrorType.Validation, Value: JObject json })
        {
            var validationErrors = json.ToObject<IDictionary<string, string[]>>();
            var validationErrorMessages = validationErrors?.SelectMany(ve => ve.Value) ?? [];
            var errorMessage = validationErrorMessages.Aggregate(error.Message, (current, validationErrorMessage) => current + $"<br/> - {validationErrorMessage}");
            return errorMessage;
        }
        
        return error?.Message ?? "Unknown Error.";
    }
}