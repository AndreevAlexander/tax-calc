using TaxCalculator.Validation.Contracts;

namespace TaxCalculator.Validation;

public class ProfileProvider : IProfileProvider
{
    private readonly List<IValidationProfile> _validationProfiles;

    public ProfileProvider()
    {
        _validationProfiles = new();
    }
    
    public IEnumerable<IValidationProfile> GetRules<TModel>() where TModel : class
    {
        return _validationProfiles.Where(x => x.HasRules<TModel>());
    }

    public void RegisterValidationProfile<TProfile>() where TProfile : ValidationProfile, new()
    {
        if (_validationProfiles.All(x => x.GetType() != typeof(TProfile)))
        {
            _validationProfiles.Add(new TProfile());	
        }
    }
}