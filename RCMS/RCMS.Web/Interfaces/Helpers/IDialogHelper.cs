using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace RCMS.Web.Interfaces.Helpers;

public interface IDialogHelper
{
    Task<bool> ShowDialogAsync<TDialog>(string title, DialogParameters parameters, DialogOptions options) where TDialog : IComponent;

    Task<bool> ShowMessageDialogAsync(string title, string message, string buttonText = "Yes", Color buttonColor = Color.Primary);

    Task<bool> ShowMessageDialogAsync(string title, RenderFragment content, string buttonText = "Yes", Color buttonColor = Color.Primary);
}