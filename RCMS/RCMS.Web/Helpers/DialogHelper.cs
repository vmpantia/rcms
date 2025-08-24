using Microsoft.AspNetCore.Components;
using MudBlazor;
using RCMS.Web.Components.Dialogs;
using RCMS.Web.Extensions;
using RCMS.Web.Interfaces.Helpers;

namespace RCMS.Web.Helpers;

public class DialogHelper(IDialogService dialogService) : IDialogHelper
{
    public async Task<bool> ShowDialogAsync<TDialog>(string title, DialogParameters parameters, DialogOptions options) where TDialog : IComponent
    {
        var dialogReference = await dialogService.ShowAsync<TDialog>(title, parameters, options);
        return await dialogReference.IsConfirmedAsync();
    }
    
    public async Task<bool> ShowMessageDialogAsync(string title, string message)
    {
        return await ShowMessageDialogAsync(title, message, null);
    }

    public async Task<bool> ShowMessageDialogAsync(string title, RenderFragment content)
    {
        return await ShowMessageDialogAsync(title, null, content);
    }

    private async Task<bool> ShowMessageDialogAsync(string title, string? message, RenderFragment? content)
    {
        var parameters = new DialogParameters<MessageDialog> { { md => md.Message, message }, { md => md.Content, content } };
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
        var dialogReference = await dialogService.ShowAsync<MessageDialog>(title, parameters, options);
        return await dialogReference.IsConfirmedAsync();
    }
}