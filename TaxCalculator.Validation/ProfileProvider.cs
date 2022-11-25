using System;
using System.Collections.Generic;
using System.Linq;
using TaxCalculator.Validation.Contracts;

namespace TaxCalculator.Validation
{
    public class ProfileProvider : IProfileProvider
    {
        private readonly Dictionary<Type, List<IValidationProfile>> _validationProfiles;

        public ProfileProvider()
        {
            _validationProfiles = new Dictionary<Type, List<IValidationProfile>>();
        }

        public IEnumerable<IValidationProfile> GetRules<TModel>() where TModel : class
        {
            return _validationProfiles[typeof(TModel)];
        }

        public void RegisterValidationProfile<TProfile>() where TProfile : ValidationProfile, new()
        {
            var profile = new TProfile();
            var profileModelTypes = profile.GetModelTypes();

            foreach (var profileModelType in profileModelTypes)
            {
                if (!_validationProfiles.TryGetValue(profileModelType, out List<IValidationProfile> profiles))
                {
                    profiles = new List<IValidationProfile>
                    {
                        profile
                    };

                    _validationProfiles.Add(profileModelType, profiles);
                }
                else
                {
                    if (_validationProfiles[profileModelType].All(x => x.GetType() != typeof(TProfile)))
                    {
                        _validationProfiles[profileModelType].Add(profile);
                    }
                }
            }
        }
    }
}