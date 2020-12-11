using System;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IPartyService
    {
        Task<bool> UserIdExistsAsync<T>(Guid userId) where T : Party;
        Task<Party> GetPartyAsync(int partyId);
        Task<T> GetPartyForUserIdAsync<T>(Guid userId) where T : Party;
        Task<int> CreatePartyAsync(Party party);
        Task<int> UpdatePartyAsync(int partyId, Party party);
        void UpdatePartyPhysicalAddress(Party current, Party updated);
        void UpdatePartyMailingAddress(Party current, Party updated);
        Task DeletePartyAsync(int partyId);
    }
}
