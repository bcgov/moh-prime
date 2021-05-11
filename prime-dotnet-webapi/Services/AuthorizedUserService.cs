using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Prime.Models;
using Prime.ViewModels.Parties;

namespace Prime.Services
{
    public class AuthorizedUserService : BaseService, IAuthorizedUserService
    {
        private readonly IMapper _mapper;
        private readonly IPartyService _partyService;

        public AuthorizedUserService(
            ApiDbContext context,
            IMapper mapper,
            IHttpContextAccessor httpContext,
            IPartyService partyService)
            : base(context, httpContext)
        {
            _mapper = mapper;
            _partyService = partyService;
        }

        public async Task<bool> AuthorizedUserExistsAsync(int authorizedUserId)
        {
            return await _context.AuthorizedUsers
                .AsNoTracking()
                .AnyAsync(au => au.Id == authorizedUserId);
        }

        public async Task<bool> AuthorizedUserExistsForUserIdAsync(Guid userId)
        {
            return await _partyService.PartyExistsForUserIdAsync(userId, PartyType.AuthorizedUser);
        }

        public async Task<AuthorizedUserViewModel> GetAuthorizedUserAsync(int authorizedUserId)
        {
            return await _context.AuthorizedUsers
                .Include(au => au.Party)
                .Where(au => au.Id == authorizedUserId)
                .ProjectTo<AuthorizedUserViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<AuthorizedUserViewModel> GetAuthorizedUserForUserIdAsync(Guid userId)
        {
            return await _context.AuthorizedUsers
                .Include(au => au.Party)
                .Where(au => au.Party.UserId == userId)
                .ProjectTo<AuthorizedUserViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<int> CreateOrUpdateAuthorizedUserAsync(AuthorizedUserChangeModel changeModel, ClaimsPrincipal user)
        {
            var authorizedUser = await GetBaseAuthorizedUserQuery()
                .SingleOrDefaultAsync(au => au.Party.UserId == user.GetPrimeUserId());

            if (authorizedUser == null)
            {
                var party = new Party
                {
                    Addresses = new List<PartyAddress>()
                };
                _context.Parties.Add(party);

                authorizedUser = new AuthorizedUser
                {
                    Party = party,
                    Status = AccessStatusType.UnderReview
                };
                _context.AuthorizedUsers.Add(authorizedUser);
            }

            changeModel.UpdateAuthorizedUser(authorizedUser, user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }

            return authorizedUser.Id;
        }

        public async Task<int> ActivateAuthorizedUser(int authorizedUserId)
        {
            var authorizedUser = await _context.AuthorizedUsers
                .SingleOrDefaultAsync(au => au.Id == authorizedUserId);

            authorizedUser.Status = AccessStatusType.Active;

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not update the authorized user status to {Enum.GetName(typeof(AccessStatusType), 3)}.");
            }

            return updated;
        }

        public async Task<int> ApproveAuthorizedUser(int authorizedUserId)
        {
            var authorizedUser = await _context.AuthorizedUsers
                .SingleOrDefaultAsync(au => au.Id == authorizedUserId);

            authorizedUser.Status = AccessStatusType.Approved;

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not update the authorized user status to {Enum.GetName(typeof(AccessStatusType), 2)}.");
            }

            return updated;
        }

        public async Task DeleteAuthorizedUserAsync(int authorizedUserId)
        {
            var authorizedUser = await _context.AuthorizedUsers
                .SingleOrDefaultAsync(au => au.Id == authorizedUserId);

            if (authorizedUser == null)
            {
                return;
            }

            await _partyService.DeletePartyAsync(authorizedUser.Party.Id);
        }

        private IQueryable<AuthorizedUser> GetBaseAuthorizedUserQuery()
        {
            return _context.AuthorizedUsers
                .Include(au => au.Party)
                    .ThenInclude(p => p.Addresses)
                        .ThenInclude(pa => pa.Address)
                .Include(au => au.HealthAuthority);
        }
    }
}
