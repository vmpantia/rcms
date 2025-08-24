using MudBlazor;

namespace RCMS.Web.Extensions;

public static class DialogExtension
{
    public static async Task<bool> IsConfirmedAsync(this IDialogReference dialogReference)
    {
        var dialogResult =  await dialogReference.Result;
        return dialogResult is { Canceled: false, Data: true };
    }
}