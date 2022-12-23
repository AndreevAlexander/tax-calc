using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Desktop.Mvvm.ModelContainer;

namespace TaxCalculator.Desktop.Views.TaxProfileManagement.TaxProfileGridView;

public class TaxProfileGridViewModel : ObservableObject
{
    private readonly IQueryBus _queryBus;

    public ObservableCollection<ModelContainer<TestModel>> Models { get; set; }

    public TaxProfileGridViewModel(IQueryBus queryBus)
    {
        _queryBus = queryBus;

        Models = new ObservableCollection<ModelContainer<TestModel>>
        {
            new ModelContainer<TestModel>(new TestModel
            {
                Title = "test"
            })
        };
        
        OnPropertyChanged(nameof(Models));
    }
}