using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class AuthorizedUserService : BaseService, IAuthorizedUserService
    {
        private readonly IMapper _mapper;
        private readonly IPartyService _partyService;

        public AuthorizedUserService(
            ApiDbContext context,
            ILogger<AuthorizedUserService> logger,
            IMapper mapper,
            IPartyService partyService)
            : base(context, logger)
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
            return await GetBaseAuthorizedUserQuery()
                .Where(au => au.Id == authorizedUserId)
                .ProjectTo<AuthorizedUserViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<AuthorizedUserViewModel> GetAuthorizedUserForUserIdAsync(Guid userId)
        {
            return await GetBaseAuthorizedUserQuery()
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
                var party = await _partyService.GetPartyForUserIdAsync(changeModel.UserId) ?? new Party
                {
                    Addresses = new List<PartyAddress>()
                };
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
                return InvalidId;
            }

            return authorizedUser.Id;
        }

        public async Task ActivateAuthorizedUser(int authorizedUserId)
        {
            var authorizedUser = await _context.AuthorizedUsers
                .SingleOrDefaultAsync(au => au.Id == authorizedUserId);

            authorizedUser.Status = AccessStatusType.Active;

            await _context.SaveChangesAsync();
        }

        public async Task ApproveAuthorizedUser(int authorizedUserId)
        {
            var authorizedUser = await _context.AuthorizedUsers
                .SingleOrDefaultAsync(au => au.Id == authorizedUserId);

            authorizedUser.Status = AccessStatusType.Approved;

            await _context.SaveChangesAsync();
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
                        .ThenInclude(pa => pa.Address);
        }
    }
}
