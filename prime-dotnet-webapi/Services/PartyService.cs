using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;


namespace Prime.Services
{
    public class PartyService : BaseService, IPartyService
    {
        public PartyService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task<bool> UserIdExistsAsync<T>(Guid userId) where T : Party
        {
            return await _context.Parties
                .OfType<T>()
                .AnyAsync(e => e.UserId == userId);
        }

        public async Task<Party> GetPartyAsync(int partyId)
        {
            return await GetBasePartyQuery()
                .SingleOrDefaultAsync(e => e.Id == partyId);
        }

        public async Task<T> GetPartyForUserIdAsync<T>(Guid userId) where T : Party
        {
            return await GetBasePartyQuery()
                .AsNoTracking()
                .OfType<T>()
                .SingleOrDefaultAsync(e => e.UserId == userId);
        }

        public async Task<int> CreatePartyAsync(Party party)
        {
            party.ThrowIfNull(nameof(party));

            _context.Parties.Add(party);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create party.");
            }

            return party.Id;
        }

        public async Task<int> UpdatePartyAsync(int partyId, Party party)
        {
            var currentParty = await GetBasePartyQuery()
                .SingleAsync(e => e.Id == partyId);

            _context.Entry(currentParty).CurrentValues.SetValues(party);

            UpdatePartyPhysicalAddress(currentParty, party);
            UpdatePartyMailingAddress(currentParty, party);

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public void UpdatePartyPhysicalAddress(Party current, Party updated)
        {
            if (updated.PhysicalAddress != null && current.PhysicalAddress != null)
            {
                _context.Entry(current.PhysicalAddress).CurrentValues.SetValues(updated.PhysicalAddress);
            }
            else
            {
                current.PhysicalAddress = updated.PhysicalAddress;
            }
        }

        public void UpdatePartyMailingAddress(Party current, Party updated)
        {
            if (updated.MailingAddress != null && current.MailingAddress != null)
            {
                _context.Entry(current.MailingAddress).CurrentValues.SetValues(updated.MailingAddress);
            }
            else
            {
                current.MailingAddress = updated.MailingAddress;
            }
        }


        public async Task DeletePartyAsync(int partyId)
        {
            var party = await GetBasePartyQuery()
                .SingleOrDefaultAsync(e => e.Id == partyId);

            if (party == null)
            {
                return;
            }

            _context.Parties.Remove(party);
            await _context.SaveChangesAsync();
        }

        private IQueryable<Party> GetBasePartyQuery()
        {
            return _context.Parties
                .Include(p => p.PhysicalAddress);
        }
    }
}
