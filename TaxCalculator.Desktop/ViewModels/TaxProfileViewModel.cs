using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using ReactiveUI;
using TaxCalculator.Application.TaxProfiles.Queries;
using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Desktop.Models;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Desktop.ViewModels;

public class TaxProfileViewModel : ViewModelBase, IRoutableViewModel
{
    private readonly IQueryBus _queryBus;
    
    private readonly IMapper _mapper;

    //public Task<ObservableCollection<TaxProfileModel>> TaxProfiles => LoadData();

    public IObservable<ObservableCollection<TaxProfileModel>> TaxProfiles { get; }
    
    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    public ReactiveCommand<Unit, Unit> RemoveCommand { get; }

    public TaxProfileModel SelectedTaxProfile { get; set; }

    public string UrlPathSegment { get; }
    
    public IScreen HostScreen { get; }

    public TaxProfileViewModel(IScreen screen,
                               IQueryBus queryBus,
                               IMapper mapper)
    {
        _queryBus = queryBus;
        _mapper = mapper;
        HostScreen = screen;
        UrlPathSegment = Guid.NewGuid().ToString().Substring(0, 5);

        EditCommand = ReactiveCommand.Create(EditExecute);
        RemoveCommand = ReactiveCommand.Create(RemoveExecute);

        TaxProfiles = LoadData().ToObservable();
    }

    private async Task<ObservableCollection<TaxProfileModel>> LoadData()
    {
        var taxProfiles =
            await _queryBus.ExecuteAsync<GetTaxProfilesQuery, List<TaxProfile>>(new GetTaxProfilesQuery());

        var taxProfileModels = _mapper.Map<IEnumerable<TaxProfile>, IEnumerable<TaxProfileModel>>(taxProfiles);
        return new ObservableCollection<TaxProfileModel>(taxProfileModels);
    }

    private void EditExecute()
    {
        Console.WriteLine(SelectedTaxProfile.Id);
    }

    private void RemoveExecute()
    {
        Console.WriteLine(SelectedTaxProfile.Id);
    }
}