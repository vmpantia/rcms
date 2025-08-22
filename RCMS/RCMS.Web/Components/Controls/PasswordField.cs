using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace RCMS.Web.Components.Controls;

public class PasswordField<T> : MudTextField<T>
{
    private bool _isShowPassword = false;

    public PasswordField()
    {
        Variant = Variant.Outlined;
        Margin = Margin.Dense;
        InputType = InputType.Password;
        Adornment = Adornment.End;
        AdornmentIcon = Icons.Material.Filled.VisibilityOff;
        AdornmentAriaLabel = "Show Password";
        OnAdornmentClick = EventCallback.Factory.Create<MouseEventArgs>(this, ShowOrHidePassword);
    }
    
    private void ShowOrHidePassword()
    {
        _isShowPassword = !_isShowPassword;
        InputType = _isShowPassword ? InputType.Text : InputType.Password;
        AdornmentIcon = _isShowPassword ? Icons.Material.Filled.Visibility : Icons.Material.Filled.VisibilityOff;
        AdornmentAriaLabel = _isShowPassword ? "Show Password" : "Hide Password";
    }
}