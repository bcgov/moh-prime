using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels.Plr;

namespace Prime.Services
{
    public interface IPlrProviderService
    {
        public Task<int> CreateOrUpdatePlrProviderAsync(PlrProvider dataObject, bool expectExists = false);
        Task<IEnumerable<PlrViewModel>> GetPlrDataByCollegeIdsAsync(IEnumerable<string> collegeIds);
        Task<bool> CheckPartyValidityAsync(int partyId);
    }
}
