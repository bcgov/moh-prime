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
        private readonly IBusinessEventService _businessEventService;

        public PartyService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IBusinessEventService businessEventService)
            : base(context, httpContext)
        {
            _businessEventService = businessEventService;
        }

        public async Task<bool> PartyExistsAsync(int partyId)
        {
            return await _context.Parties
                .AnyAsync(e => e.Id == partyId);
        }

        public async Task<bool> PartyUserIdExistsAsync(Guid userId)
        {
            return await _context.Parties
                .AnyAsync(e => e.UserId == userId);
        }


        public async Task<Party> GetPartyForUserIdAsync(Guid userId)
        {
            var entity = await _context.Parties
                .FirstOrDefaultAsync(e => e.UserId == userId);

            return entity;
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
            var currentParty = await _context.Parties
                .SingleAsync(e => e.Id == partyId);

            _context.Entry(party).CurrentValues.SetValues(party);

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public async Task DeletePartyAsync(int partyId)
        {
            var party = await _context.Parties
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
            return _context.Parties;
        }

        public async Task<Party> GetPartyAsync(int partyId)
        {
            IQueryable<Party> query = this.GetBasePartyQuery();

            var entity = await query
                .SingleOrDefaultAsync(e => e.Id == partyId);

            return entity;
        }

    }
}
