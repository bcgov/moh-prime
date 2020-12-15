using System;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IPartyService
    {
        Task<bool> UserIdExistsAsync(Guid userId);
        Task<int> GetPartyIdForUserIdAsync(Guid userId);
        Task<Party> GetPartyAsync(int partyId);
        Task<Party> GetPartyForUserIdAsync(Guid userId);
        Task<int> CreatePartyAsync(Party party, PartyType type);
        Task<int> UpdatePartyAsync(int partyId, Party party = null, PartyType? type = null);
        void UpdatePartyPhysicalAddress(Party current, Party updated);
        void UpdatePartyMailingAddress(Party current, Party updated);
        Task DeletePartyAsync(int partyId);
    }
}
