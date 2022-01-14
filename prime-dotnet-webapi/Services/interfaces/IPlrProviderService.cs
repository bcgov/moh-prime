using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels;
using Prime.ViewModels.Plr;

namespace Prime.Services
{
    public interface IPlrProviderService
    {
        Task<int> CreateOrUpdatePlrProviderAsync(PlrProvider dataObject, bool expectExists = false);
        Task<IEnumerable<PlrViewModel>> GetMatchingPlrDataAsync(IEnumerable<CertificationViewModel> certifications);
        Task<bool> PartyExistsInPlrWithCollegeIdAndNameAndDobAsync(int partyId);
    }
}
