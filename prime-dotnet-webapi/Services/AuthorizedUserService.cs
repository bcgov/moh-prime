using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Prime.Models;
using Prime.ViewModels.HealthAuthoritySites;
using Prime.ViewModels.Parties;
using DelegateDecompiler.EntityFrameworkCore;

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

        public async Task<bool> AuthorizedUserExistsForUsernameAsync(string username)
        {
            return await _partyService.PartyExistsForUsernameAsync(username, PartyType.AuthorizedUser);
        }

        public async Task<AuthorizedUserViewModel> GetAuthorizedUserAsync(int authorizedUserId)
        {
            return await GetBaseAuthorizedUserQuery()
                .Where(au => au.Id == authorizedUserId)
                .ProjectTo<AuthorizedUserViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<AuthorizedUserViewModel> GetAuthorizedUserForUsernameAsync(string username)
        {
            return await GetBaseAuthorizedUserQuery()
                .Where(au => au.Party.Username == username)
                .ProjectTo<AuthorizedUserViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        /// <summary>
        /// return all HA sites from Authorized User's HA
        /// </summary>
        /// <param name="authorizedUserId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<HealthAuthoritySiteListViewModel>> GetAuthorizedUserSitesAsync(int authorizedUserId)
        {
            var authorizedUser = await _context.AuthorizedUsers.SingleOrDefaultAsync(au => au.Id == authorizedUserId);
            var orgId = (int)authorizedUser.HealthAuthorityCode;

            return await _context.HealthAuthoritySites
                .Where(has => has.HealthAuthorityOrganizationId == orgId && has.DeletedDate == null && has.ArchivedDate == null)
                .ProjectTo<HealthAuthoritySiteListViewModel>(_mapper.ConfigurationProvider)
                .DecompileAsync()
                .ToListAsync();
        }

        public async Task<int> GetAuthorizedUserSiteCountAsync(int authorizedUserId)
        {
            var sites = await _context.HealthAuthoritySites.Where(s => s.AuthorizedUserId == authorizedUserId).ToListAsync();
            return sites.Count;
        }

        public async Task<int> CreateOrUpdateAuthorizedUserAsync(AuthorizedUserChangeModel changeModel, ClaimsPrincipal user)
        {
            var authorizedUser = await GetBaseAuthorizedUserQuery()
                .SingleOrDefaultAsync(au => au.Party.Username == user.GetPrimeUsername());

            if (authorizedUser == null)
            {
                // TODO: Ensure AuthorizedUserChangeModel.Username is populated
                var party = await _partyService.GetPartyForUsernameAsync(changeModel.Username) ?? new Party
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

            var auPartyEnrolment = await _context.PartyEnrolments.Where(p => p.PartyId == authorizedUser.PartyId && p.PartyType == PartyType.AuthorizedUser).FirstOrDefaultAsync();
            var nonAUPartyEnrolment = await _context.PartyEnrolments.Where(p => p.PartyId == authorizedUser.PartyId && p.PartyType != PartyType.AuthorizedUser).FirstOrDefaultAsync();

            if (nonAUPartyEnrolment == null)
            {
                var party = await _context.Parties.Where(p => p.Id == authorizedUser.PartyId).FirstOrDefaultAsync();
                if (party != null)
                {
                    _context.Parties.Remove(party);
                }
            }
            _context.AuthorizedUsers.Remove(authorizedUser);
            _context.PartyEnrolments.Remove(auPartyEnrolment);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAuthorizedUserStatus(int authorizedUserId, AccessStatusType statusType)
        {
            var authorizedUser = await _context.AuthorizedUsers.Where(u => u.Id == authorizedUserId).SingleOrDefaultAsync();
            authorizedUser.Status = statusType;

            await _context.SaveChangesAsync();
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
