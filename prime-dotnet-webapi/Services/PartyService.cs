using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;
using Prime.ViewModels.Parties;

namespace Prime.Services
{
    public class PartyService : BaseService, IPartyService
    {
        public PartyService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

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

        /// <summary>
        /// Creates or updates a party based on the User ID of the supplied user.
        /// Returns the Id of the affected Party.
        /// </summary>
        /// <param name="changeModel"></param>
        /// <param name="user"></param>
        public async Task<int> CreateOrUpdatePartyAsync(IPartyChangeModel changeModel, ClaimsPrincipal user)
        {
            var currentParty = await GetBasePartyQuery()
                .SingleOrDefaultAsync(p => p.UserId == user.GetPrimeUserId());

            if (currentParty == null)
            {
                currentParty = new Party();
                _context.Parties.Add(currentParty);
            }

            changeModel.UpdateParty(currentParty, user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }

            return currentParty.Id;
        }

        public void UpdatePartyPhysicalAddress(Party current, Party updated)
        {
            if (current.PhysicalAddress == null)
            {
                current.PhysicalAddress = updated.PhysicalAddress;
            }
            else
            {
                current.PhysicalAddress.SetValues(updated.PhysicalAddress);
            }
        }

        public void UpdatePartyMailingAddress(Party current, Party updated)
        {
            if (current.MailingAddress == null)
            {
                current.MailingAddress = updated.MailingAddress;
            }
            else
            {
                current.MailingAddress.SetValues(updated.MailingAddress);
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

        public async Task<IEnumerable<PartyType>> GetPreApprovedRegistrationsAsync(string firstName, string lastName, string email)
        {
            return await _context.PreApprovedRegistrations
                .AsNoTracking()
                .Where(pre => pre.FirstName == firstName
                    && pre.LastName == lastName
                    && pre.Email == email)
                .Select(pre => pre.PartyType)
                .ToListAsync();
        }

        private IQueryable<Party> GetBasePartyQuery()
        {
            return _context.Parties
                .Include(p => p.PhysicalAddress)
                .Include(p => p.MailingAddress)
                .Include(p => p.PartyEnrolments);
        }
    }
}
