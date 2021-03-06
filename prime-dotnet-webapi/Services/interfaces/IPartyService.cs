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
        Task<Party> GetPartyAsync(int partyId);
        Task<Party> GetPartyForUserIdAsync(Guid userId);
        Task<int> CreateOrUpdatePartyAsync(IPartyChangeModel changeModel, ClaimsPrincipal user);
        void UpdateAddress<T>(Party dbParty, T newAddress) where T : Address;
        Task DeletePartyAsync(int partyId);
        Task<IEnumerable<PartyType>> GetPreApprovedRegistrationsAsync(string firstName, string lastName, string email);
    }
}
