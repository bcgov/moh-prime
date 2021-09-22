using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.ViewModels.Parties;

namespace Prime.Services
{
    public interface IPartyService
    {
        Task<bool> PartyExistsAsync(int partyId, PartyType? withType = null);
        Task<bool> PartyExistsForUserIdAsync(Guid userId, PartyType? withType = null);
        Task<Party> GetPartyAsync(int partyId, PartyType? withType = null);
        Task<Party> GetPartyForUserIdAsync(Guid userId, PartyType? withType = null);
        Task<int> CreateOrUpdatePartyAsync(IPartyChangeModel changeModel, ClaimsPrincipal user);
        void UpdateAddress<T>(Party dbParty, T newAddress) where T : Address;
        Task RemovePartyEnrolmentAsync(int partyId, PartyType partyType);
        Task DeletePartyAsync(int partyId);
        Task<IEnumerable<PartyType>> GetPreApprovedRegistrationsAsync(string firstName, string lastName, string email);
    }
}
