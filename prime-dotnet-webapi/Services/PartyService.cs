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
                currentParty.Addresses = new List<PartyAddress>();
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

        public void UpdateAddress<T>(Party dbParty, T newAddress) where T : Address
        {
            var existingPartyAddress = dbParty.Addresses
                .Where(ea => ea.Address is T)
                .SingleOrDefault();

            if (existingPartyAddress == null)
            {
                if (newAddress == null)
                {
                    // Noop
                    return;
                }
                else
                {
                    // New
                    newAddress.Id = 0;
                    dbParty.Addresses.Add(new PartyAddress
                    {
                        Party = dbParty,
                        Address = newAddress
                    });
                }
            }
            else
            {
                if (newAddress == null)
                {
                    // Remove
                    _context.Remove(existingPartyAddress.Address);
                    _context.Remove(existingPartyAddress);
                    return;
                }
                else
                {
                    // Update
                    newAddress.Id = existingPartyAddress.AddressId;
                    _context.Entry(existingPartyAddress.Address).CurrentValues.SetValues(newAddress);
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

        public async Task<IEnumerable<PartyType>> GetPreApprovedRegistrationsAsync(string firstName, string lastName, string email)
        {
            return await _context.PreApprovedRegistrations
                .AsNoTracking()
                .Where(pre => pre.FirstName.ToLower() == firstName.ToLower()
                    && pre.LastName.ToLower() == lastName.ToLower()
                    && pre.Email.ToLower() == email.ToLower())
                .Select(pre => pre.PartyType)
                .Distinct()
                .ToListAsync();
        }

        private IQueryable<Party> GetBasePartyQuery()
        {
            return _context.Parties
                .Include(e => e.Addresses)
                    .ThenInclude(pa => pa.Address)
                .Include(p => p.PartyEnrolments);
        }

    }
}
