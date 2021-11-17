using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IBusinessEventService
    {
        Task<BusinessEvent> CreateStatusChangeEventAsync(int enrolleeId, string description);
        Task<BusinessEvent> CreateEmailEventAsync(int enrolleeId, string description);
        Task<BusinessEvent> CreateNoteEventAsync(int enrolleeId, string description);
        Task<BusinessEvent> CreateAdminActionEventAsync(int enrolleeId, string description);
        Task<BusinessEvent> CreateAdminViewEventAsync(int enrolleeId, string description);
        Task<BusinessEvent> CreateEnrolleeEventAsync(int enrolleeId, string description);
        Task<BusinessEvent> CreateSiteEventAsync(int siteId, int partyId, string description);
        /// <summary>
        /// Creates a Site Event, with the PartyId set to the Party responsible for the Site; i.e. SigningAuthority for Community Sites, Authorized User for Health Authority Sites
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="description"></param>
        Task<BusinessEvent> CreateSiteEventAsync(int siteId, string description);
        Task<BusinessEvent> CreateOrganizationEventAsync(int organizationId, int partyId, string description);
        Task<BusinessEvent> CreatePharmanetApiCallEventAsync(int enrolleeId, string licencePrefix, string licenceNumber, string description);
    }
}
