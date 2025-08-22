using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;

namespace RCMS.Web.Components.Controls;

public class Button : ComponentBase
{
    [Parameter] public string Text { get; set; }
    [Parameter] public ButtonType Type { get; set; } = ButtonType.Button;
    [Parameter] public Variant Variant { get; set; } = Variant.Filled;
    [Parameter] public Color Color { get; set; } = Color.Primary;
    [Parameter] public bool IsFullWidth { get; set; } = true;
    [Parameter] public bool IsDisabled { get; set; }
    [Parameter] public bool IsLoading { get; set; }
    [Parameter] public string LoadingText { get; set; }
    [Parameter] public Size LoadingSize { get; set; } = Size.Small;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        int seq = 0;

        builder.OpenComponent<MudButton>(seq++);
        builder.AddAttribute(seq++, "ButtonType", Type);
        builder.AddAttribute(seq++, "Variant", Variant);
        builder.AddAttribute(seq++, "Color", Color);
        builder.AddAttribute(seq++, "FullWidth", IsFullWidth);
        builder.AddAttribute(seq++, "Disabled", IsDisabled);

        builder.AddAttribute(seq++, "ChildContent", (RenderFragment)(childBuilder =>
        {
            if (IsLoading)
            {
                childBuilder.OpenComponent<MudProgressCircular>(seq++);
                childBuilder.AddAttribute(seq++, "Class", "ms-n1");
                childBuilder.AddAttribute(seq++, "Size", LoadingSize);
                childBuilder.AddAttribute(seq++, "Indeterminate", true);
                childBuilder.CloseComponent();

                childBuilder.OpenComponent<MudText>(seq++);
                childBuilder.AddAttribute(seq++, "Class", "ms-2");
                childBuilder.AddAttribute(seq++, "ChildContent", (RenderFragment)(textBuilder =>
                {
                    textBuilder.AddContent(seq++, LoadingText);
                }));
                childBuilder.CloseComponent();
            }
            else
            {
                childBuilder.OpenComponent<MudText>(seq++);
                childBuilder.AddAttribute(seq++, "ChildContent", (RenderFragment)(textBuilder =>
                {
                    textBuilder.AddContent(seq++, Text);
                }));
                childBuilder.CloseComponent();
            }
        }));

        builder.CloseComponent();
    }
}
