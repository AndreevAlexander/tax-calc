using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TaxCalculator.Validation.Contracts;

namespace TaxCalculator.WebFrontend.Validation;

public class Validator<TModel> : ComponentBase 
    where TModel : class
{
    [CascadingParameter]
    private EditContext EditContext { get; set; }

    [Parameter] 
    public object? ValidationContext { get; set; }

    [Parameter]
    public Type ModelType { get; set; }

    [Parameter]
    public Action? OnValidated { get; set; }

    [Inject]
    public IValidationEngine ValidationEngine { get; set; }
    
    private ValidationMessageStore ValidationMessageStore { get; set; }

    protected override void OnInitialized()
    {
        ValidationMessageStore = new ValidationMessageStore(EditContext);

        EditContext.OnFieldChanged += OnFieldChanged;
        EditContext.OnValidationRequested += OnValidationRequested;
    }

    private async Task ValidateModelAsync()
    {
        ValidationMessageStore.Clear();
        
        var result = await ValidationEngine.ValidateAsync((TModel)EditContext.Model, ValidationContext);

        if (result.HasErrors)
        {
            foreach (var resultsForProperty in result.ValidationResults)
            {
                var identifier = EditContext.Field(resultsForProperty.Key);

                foreach (var validationResult in resultsForProperty.Value)
                {
                    ValidationMessageStore.Add(identifier, validationResult.Message ?? string.Empty);
                }
            }
        }
        
        EditContext.NotifyValidationStateChanged();
    }

    private async void OnValidationRequested(object? sender, ValidationRequestedEventArgs e)
    {
        await ValidateModelAsync();
        OnValidated?.Invoke();
    }

    private async void OnFieldChanged(object? sender, FieldChangedEventArgs e)
    {
        await ValidateModelAsync();
        OnValidated?.Invoke();
    }
}