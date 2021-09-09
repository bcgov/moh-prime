using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Prime.HttpClients;
using Prime.Models;
using Prime.ViewModels;
using Prime.ViewModels.Parties;
using static Prime.PrimeEnvironment.MohKeycloak;

namespace Prime.Services
{
    public class GisService : BaseService, IGisService
    {
        private readonly IMapper _mapper;
        private readonly ILdapClient _ldapClient;
        private readonly IMohKeycloakClient _mohKeycloakClient;

        public GisService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ILdapClient ldapClient,
            IMohKeycloakClient mohKeycloakClient)
            : base(context, httpContext)
        {
            _mapper = mapper;
            _ldapClient = ldapClient;
            _mohKeycloakClient = mohKeycloakClient;
        }

        public async Task<GisViewModel> GetGisEnrolmentAsync(int gisId)
        {
            return await _context.GisEnrolments
                .AsNoTracking()
                .ProjectTo<GisViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(g => g.Id == gisId);
        }

        public async Task<GisViewModel> GetGisEnrolmentAsync(Guid userId)
        {
            return await _context.GisEnrolments
                .AsNoTracking()
                .ProjectTo<GisViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(g => g.Party.UserId == userId);
        }

        public async Task<int> CreateOrUpdateGisEnrolmentAsync(GisChangeModel changeModel, ClaimsPrincipal user)
        {
            var currentGisEnrolment = await _context.GisEnrolments
                .Include(g => g.Party)
                .SingleOrDefaultAsync(g => g.Party.UserId == user.GetPrimeUserId());

            if (currentGisEnrolment == null)
            {
                var currentParty = await _context.Parties
                    .SingleOrDefaultAsync(p => p.UserId == user.GetPrimeUserId());

                if (currentParty == null)
                {
                    currentParty = new Party();
                    _context.Parties.Add(currentParty);
                    currentParty.Addresses = new List<PartyAddress>();
                }

                currentGisEnrolment = new GisEnrolment
                {
                    Party = currentParty
                };
                _context.GisEnrolments.Add(currentGisEnrolment);
            }

            changeModel.UpdateGisParty(currentGisEnrolment, user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return InvalidId;
            }

            return currentGisEnrolment.Id;
        }

        public async Task<GisLdapUser> LdapLogin(string username, string password, ClaimsPrincipal user)
        {
            var ldapResponse = await _ldapClient.GetUserAsync(username, password);
            var gisLdapUser = new GisLdapUser
            {
                Unlocked = ldapResponse?.Unlocked,
                GisUserRole = ldapResponse?.Gisuserrole
            };

            if (gisLdapUser.Success)
            {
                var gisEnrolment = await _context.GisEnrolments
                    .Include(g => g.Party)
                    .SingleOrDefaultAsync(g => g.Party.UserId == user.GetPrimeUserId());

                gisEnrolment.LdapLoginSuccessDate = DateTime.Now;

                await _context.SaveChangesAsync();
            }

            return gisLdapUser;
        }

        public async Task SubmitApplicationAsync(int gisId)
        {
            var gisEnrolment = await _context.GisEnrolments
                .Include(g => g.Party)
                .SingleOrDefaultAsync(g => g.Id == gisId);

            gisEnrolment.SubmittedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            // Also update MOH Keycloak now that the applictaion has been completed.
            await _mohKeycloakClient.AssignClientRole(gisEnrolment.Party.UserId, GisClientId, GisUserRole);
        }
    }
}
