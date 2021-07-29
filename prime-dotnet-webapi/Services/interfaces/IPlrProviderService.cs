using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels.Plr;

namespace Prime.Services
{
    public interface IPlrProviderService
    {
        public Task<int> CreateOrUpdatePlrProviderAsync(PlrProvider dataObject, bool expectExists = false);
        Task<IEnumerable<PlrViewModel>> GetPlrDataByCollegeIdAsync(IEnumerable<string> collegeId);
    }
}
