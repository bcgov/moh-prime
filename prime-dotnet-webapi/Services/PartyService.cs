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

        public async Task<bool> PartyExistsAsync(int partyId)
        {
            return await GetBasePartyQuery()
                .AnyAsync(e => e.Id == partyId);
        }

        public async Task<bool> PartyUserIdExistsAsync(Guid userId)
        {
            return await GetBasePartyQuery()
                .AnyAsync(e => e.UserId == userId);
        }


        public async Task<Party> GetPartyForUserIdAsync(Guid userId)
        {
            return await GetBasePartyQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.UserId == userId);
        }

        public async Task<int> CreatePartyAsync(Party party)
        {
            if (party == null)
            {
                throw new ArgumentNullException(nameof(party), "Could not create an party, the passed in Party cannot be null.");
            }

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

            UpdatePartyAddress(currentParty, party);

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public void UpdatePartyAddress(Party current, Party updated)
        {
            if (updated.PhysicalAddress != null)
            {
                if (current.PhysicalAddress == null)
                {
                    current.PhysicalAddress = updated.PhysicalAddress;
                }
                else
                {
                    this._context.Entry(current.PhysicalAddress).CurrentValues.SetValues(updated.PhysicalAddress);
                }
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

        public async Task<Party> GetPartyAsync(int partyId)
        {
            return await this.GetBasePartyQuery()
            .SingleOrDefaultAsync(e => e.Id == partyId);
        }
    }
}
