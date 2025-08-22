using MudBlazor;

namespace RCMS.Web.Services.Contracts;

public class BaseService<TService>(ISnackbar snackbar, ILogger<TService> logger)
{
    protected void HandleUnexpectedError(Exception ex, string baseMessage, Severity severity = Severity.Error)
    {
        var errorMessage = $"{baseMessage} | Message: {ex.Message}";
        snackbar.Add(errorMessage, severity);
        logger.LogError(errorMessage);
    }
}