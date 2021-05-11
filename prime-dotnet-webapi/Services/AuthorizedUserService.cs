using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;
using Prime.ViewModels.Parties;

namespace Prime.Services
{
    public class AuthorizedUserService : BaseService, IAuthorizedUserService
    {
        private readonly IPartyService _partyService;

        public AuthorizedUserService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IPartyService partyService)
            : base(context, httpContext)
        {
            _partyService = partyService;
        }

        public async Task<bool> AuthorizedUserExistsAsync(int authorizedUserId)
        {
            return await _context.AuthorizedUsers
                .AsNoTracking()
                .AnyAsync(au => au.Id == authorizedUserId);
        }

        public async Task<bool> AuthorizedUserForUserIdAsync(Guid userId)
        {
            return await _partyService.PartyExistsForUserIdAsync(userId, PartyType.AuthorizedUser);
        }

        public async Task<AuthorizedUser> GetAuthorizedUserAsync(int authorizedUserId)
        {
            return await GetBaseAuthorizedUserQuery()
                .SingleOrDefaultAsync(e => e.Id == authorizedUserId);
        }

        public async Task<AuthorizedUser> GetAuthorizedUserForUserIdAsync(Guid userId)
        {
            return await GetBaseAuthorizedUserQuery()
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Party.UserId == userId);
        }

        public async Task<int> CreateOrUpdateAuthorizedUserAsync(AuthorizedUserChangeModel changeModel, ClaimsPrincipal user)
        {
            var authorizedUser = await GetBaseAuthorizedUserQuery()
                .SingleOrDefaultAsync(p => p.Party.UserId == user.GetPrimeUserId());

            if (authorizedUser == null)
            {
                authorizedUser = new AuthorizedUser();
                _context.AuthorizedUsers.Add(authorizedUser);
                authorizedUser.Party = new Party();
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

        public async Task DeleteAuthorizedUserAsync(int authorizedUserId)
        {
            var authorizedUser = await GetBaseAuthorizedUserQuery()
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
                .Include(au => au.HealthAuthority);
        }
    }
}
