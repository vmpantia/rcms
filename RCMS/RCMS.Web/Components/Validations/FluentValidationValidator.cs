using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace RCMS.Web.Components.Validations;

public class FluentValidationValidator<T> : ComponentBase
{
    [CascadingParameter] EditContext CurrentEditContext { get; set; }
    [Inject] public IValidator<T> Validator { get; set; }
    
    private ValidationMessageStore _messages;

    protected override void OnInitialized()
    {
        _messages = new ValidationMessageStore(CurrentEditContext);
        CurrentEditContext.OnValidationRequested += (s, e) => ValidateModel();
        CurrentEditContext.OnFieldChanged += (s, e) => ValidateModel();
    }

    private void ValidateModel()
    {
        _messages.Clear();
        var context = new ValidationContext<T>((T)CurrentEditContext.Model);
        var result = Validator.Validate(context);

        foreach (var error in result.Errors)
        {
            var field = new FieldIdentifier(CurrentEditContext.Model, error.PropertyName);
            _messages.Add(field, error.ErrorMessage);
        }

        CurrentEditContext.NotifyValidationStateChanged();
    }
}