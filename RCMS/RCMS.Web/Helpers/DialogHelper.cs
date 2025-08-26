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
    
    public async Task<bool> ShowMessageDialogAsync(string title, string message, string buttonText = "Yes", Color buttonColor = Color.Primary)
    {
        return await ShowMessageDialogAsync(title, message, null, buttonText, buttonColor);
    }

    public async Task<bool> ShowMessageDialogAsync(string title, RenderFragment content, string buttonText = "Yes", Color buttonColor = Color.Primary)
    {
        return await ShowMessageDialogAsync(title, null, content, buttonText, buttonColor);
    }

    private async Task<bool> ShowMessageDialogAsync(string title, string? message, RenderFragment? content, string buttonText, Color buttonColor)
    {
        var parameters = new DialogParameters<MessageDialog> { { md => md.Message, message }, { md => md.Content, content }, { md => md.ButtonText, buttonText }, { md => md.ButtonColor, buttonColor } };
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
        var dialogReference = await dialogService.ShowAsync<MessageDialog>(title, parameters, options);
        return await dialogReference.IsConfirmedAsync();
    }
}