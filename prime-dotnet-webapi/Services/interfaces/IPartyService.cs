using System;
using System.Threading.Tasks;
using Prime.Models;


namespace Prime.Services
{
    public interface IPartyService
    {
        Task<Party> GetPartyForUserIdAsync(Guid userId);

        Task<bool> PartyExistsAsync(int partyId);

        Task<bool> PartyUserIdExistsAsync(Guid userId);

        Task<Party> GetPartyAsync(int partyId);

        Task<int> CreatePartyAsync(Party party);

        Task<int> UpdatePartyAsync(int partyId, Party party);

        Task DeletePartyAsync(int partyId);

    }
}
