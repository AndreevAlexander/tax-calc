using TaxCalculator.Validation.Contracts;

namespace TaxCalculator.Validation;

public class ProfileProvider : IProfileProvider
{
    private readonly List<IValidationProfile> _validationProfiles;

    private readonly Dictionary<Type, List<IValidationProfile>> _validationProfilesPerModel;

    public ProfileProvider()
    {
        _validationProfiles = new();
        _validationProfilesPerModel = new();
    }
    
    public IEnumerable<IValidationProfile> GetRules<TModel>() where TModel : class
    {
        return _validationProfilesPerModel[typeof(TModel)];
    }

    public void RegisterValidationProfile<TProfile>() where TProfile : ValidationProfile, new()
    {
        var profile = new TProfile();
        var profileModelTypes = profile.GetModelTypes();

        foreach (var profileModelType in profileModelTypes)
        {
            if (!_validationProfilesPerModel.TryGetValue(profileModelType, out List<IValidationProfile>? profiles))
            {
                profiles = new List<IValidationProfile>
                {
                    profile
                };
                
                _validationProfilesPerModel.Add(profileModelType, profiles);
            }
            else
            {
                if (_validationProfilesPerModel[profileModelType].All(x => x.GetType() != typeof(TProfile)))
                {
                    _validationProfilesPerModel[profileModelType].Add(profile);    
                }
            }
        }
    }
}