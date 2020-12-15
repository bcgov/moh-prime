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

        public async Task<bool> UserIdExistsAsync(Guid userId)
        {
            return await _context.Parties
                .AsNoTracking()
                .AnyAsync(e => e.UserId == userId);
        }

        /// <summary>
        /// Gets the PartyId for a given UserId. Returns -1 if the UserId does not exist.
        /// </summary>
        /// <param name="userId"></param>
        public async Task<int> GetPartyIdForUserIdAsync(Guid userId)
        {
            var Id = await _context.Parties
                .AsNoTracking()
                .Where(p => p.UserId == userId)
                .Select(p => (int?)p.Id)
                .SingleOrDefaultAsync();

            return Id ?? -1;
        }

        public async Task<Party> GetPartyAsync(int partyId)
        {
            return await GetBasePartyQuery()
                .SingleOrDefaultAsync(e => e.Id == partyId);
        }

        public async Task<Party> GetPartyForUserIdAsync(Guid userId)
        {
            return await GetBasePartyQuery()
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.UserId == userId);
        }

        public async Task<int> CreatePartyAsync(Party party, PartyType type)
        {
            party.ThrowIfNull(nameof(party));

            party.SetPartyType(type);

            _context.Parties.Add(party);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create party.");
            }

            return party.Id;
        }

        /// <summary>
        /// Updates a Party and/or sets a PartyType on a Party.
        /// </summary>
        /// <param name="partyId"></param>
        /// <param name="party"></param>
        /// <param name="type"></param>
        public async Task<int> UpdatePartyAsync(int partyId, Party party = null, PartyType? type = null)
        {
            var currentParty = await GetBasePartyQuery()
                .If(type.HasValue, q => q.Include(x => x.PartyEnrolments))
                .SingleAsync(e => e.Id == partyId);

            if (party != null)
            {
                UpdatePartyInternal(currentParty, party);
            }
            if (type.HasValue)
            {
                party.SetPartyType(type.Value);
            }

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

        private void UpdatePartyInternal(Party current, Party updated)
        {
            _context.Entry(current).CurrentValues.SetValues(updated);

            UpdatePartyPhysicalAddress(current, updated);
            UpdatePartyMailingAddress(current, updated);
        }
    }
}
