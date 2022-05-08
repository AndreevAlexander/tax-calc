using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TaxCalculator.Validation;
using TaxCalculator.Validation.Contracts;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend;

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
    public Action? OnValidation { get; set; }

    [Inject]
    public IValidationEngine ValidationEngine { get; set; }
    
    private ValidationMessageStore ValidationMessageStore { get; set; }

    protected override void OnInitialized()
    {
        ValidationMessageStore = new ValidationMessageStore(EditContext);

        EditContext.OnFieldChanged += OnFieldChanged;
        EditContext.OnValidationRequested += OnValidationRequested;
        EditContext.OnValidationStateChanged += OnValidationStateChanged;
    }

    private void ValidateModel()
    {
        ValidationMessageStore.Clear();
        
        var result = ValidationEngine.Validate((TModel)EditContext.Model, ValidationContext);

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

    private void OnValidationRequested(object? sender, ValidationRequestedEventArgs e)
    { 
        ValidateModel();
    }

    private void OnFieldChanged(object? sender, FieldChangedEventArgs e)
    {
        ValidateModel();
    }
    
    private void OnValidationStateChanged(object? sender, ValidationStateChangedEventArgs e)
    {
        OnValidation?.Invoke();
    }
}