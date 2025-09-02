using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace RCMS.Web.Models;

public class SelectFieldOption
{
    public SelectFieldOption(string value, RenderFragment display)
    {
        Value = value;
        Display = display;
    }

    public SelectFieldOption(string value)
    {
        Value = value;        
        Display = builder =>
        {
            builder.OpenComponent<MudText>(0);
            builder.AddAttribute(1, "ChildContent", (RenderFragment)(childBuilder =>
            {
                childBuilder.AddContent(2, value);
            }));
            builder.CloseComponent();
        };
    }

    public SelectFieldOption(string value, string display)
    {
        Value = value;        
        Display = builder =>
        {
            builder.OpenComponent<MudText>(0);
            builder.AddAttribute(1, "ChildContent", (RenderFragment)(childBuilder =>
            {
                childBuilder.AddContent(2, display);
            }));
            builder.CloseComponent();
        };
    }
    
    public string Value { get; set; }
    public RenderFragment Display { get; set; }
}