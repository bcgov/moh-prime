using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Prime.Models;
using Prime.ViewModels.Parties;

namespace Prime.Services
{
    public class PartyService : BaseService, IPartyService
    {
        public PartyService(
            ApiDbContext context,
            ILogger<PartyService> logger)
            : base(context, logger)
        { }

        public async Task<bool> PartyExistsAsync(int partyId, PartyType? withType = null)
        {
            return await _context.Parties
                .AsNoTracking()
                .If(withType.HasValue, q => q.WithPartyType(withType.Value))
                .AnyAsync(p => p.Id == partyId);
        }

        public async Task<bool> PartyExistsForUserIdAsync(Guid userId, PartyType? withType = null)
        {
            return await _context.Parties
                .AsNoTracking()
                .If(withType.HasValue, q => q.WithPartyType(withType.Value))
                .AnyAsync(p => p.UserId == userId);
        }

        public async Task<Party> GetPartyAsync(int partyId, PartyType? withType = null)
        {
            return await GetBasePartyQuery()
                .If(withType.HasValue, q => q.WithPartyType(withType.Value))
                .SingleOrDefaultAsync(e => e.Id == partyId);
        }

        public async Task<Party> GetPartyForUserIdAsync(Guid userId, PartyType? withType = null)
        {
            return await GetBasePartyQuery()
                .If(withType.HasValue, q => q.WithPartyType(withType.Value))
                .SingleOrDefaultAsync(p => p.UserId == userId);
        }

        /// <summary>
        /// Creates or updates a party based on the User ID of the supplied user, and
        /// returns the Id of the affected Party.
        /// </summary>
        public async Task<int> CreateOrUpdatePartyAsync(IPartyChangeModel changeModel, ClaimsPrincipal user)
        {
            var currentParty = await GetBasePartyQuery()
                .SingleOrDefaultAsync(p => p.UserId == user.GetPrimeUserId());

            if (currentParty == null)
            {
                currentParty = new Party
                {
                    Addresses = new List<PartyAddress>()
                };
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

        public void UpdateAddress<T>(Party dbParty, T newAddress) where T : Address
        {
            var existingPartyAddress = dbParty.Addresses
                .SingleOrDefault(ea => ea.Address is T);

            if (existingPartyAddress == null)
            {
                if (newAddress == null)
                {
                    return;
                }

                newAddress.Id = 0;
                dbParty.Addresses.Add(new PartyAddress
                {
                    Party = dbParty,
                    Address = newAddress
                });
            }
            else
            {
                if (newAddress == null)
                {
                    _context.Remove(existingPartyAddress.Address);
                    _context.Remove(existingPartyAddress);
                    return;
                }

                newAddress.Id = existingPartyAddress.AddressId;
                _context.Entry(existingPartyAddress.Address).CurrentValues.SetValues(newAddress);
            }
        }

        public async Task RemovePartyEnrolmentAsync(int partyId, PartyType partyType)
        {
            var partyEnrolment = _context.Set<PartyEnrolment>()
                .SingleOrDefault(pe => pe.PartyId == partyId && pe.PartyType == partyType);

            if (partyEnrolment != null)
            {
                _context.Set<PartyEnrolment>().Remove(partyEnrolment);

                await _context.SaveChangesAsync();
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
