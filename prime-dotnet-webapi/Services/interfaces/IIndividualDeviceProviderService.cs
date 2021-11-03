using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IIndividualDeviceProviderService
    {
        Task<IndividualDeviceProviderViewModel> CreateProviderAsync(int communitySiteId, IndividualDeviceProviderCreateOrUpdateModel createModel);
        Task UpdateProviderAsync(int providerId, IndividualDeviceProviderCreateOrUpdateModel updateModel);
        Task RemoveProviderAsync(int providerId);
        Task<IEnumerable<IndividualDeviceProviderViewModel>> GetProvidersAsync(int communitySiteId);
        Task<bool> ProviderExistsOnSiteAsync(int communitySiteId, int providerId);
    }
}
