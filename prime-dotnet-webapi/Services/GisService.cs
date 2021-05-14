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
using Prime.ViewModels.Parties;

namespace Prime.Services
{
    public class GisService : BaseService, IGisService
    {
        private readonly IMapper _mapper;
        private readonly ILdapClient _ldapClient;

        public GisService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ILdapClient ldapClient)
            : base(context, httpContext)
        {
            _mapper = mapper;
            _ldapClient = ldapClient;
        }

        public async Task<GisViewModel> GetGisEnrolmentByIdAsync(int gisId)
        {
            return await _context.GisEnrolments
                .AsNoTracking()
                .ProjectTo<GisViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(g => g.Id == gisId);
        }

        public async Task<GisViewModel> GetGisEnrolmentByUserIdAsync(Guid userId)
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
                return -1;
            }

            return currentGisEnrolment.Id;
        }

        public async Task<bool> LdapLogin(string username, string password, ClaimsPrincipal user)
        {
            var gisUserRole = await _ldapClient.GetUserAsync(username, password);
            var success = gisUserRole == "GISUSER";

            if (success)
            {
                var gisEnrolment = await _context.GisEnrolments
                .Include(g => g.Party)
                .SingleOrDefaultAsync(g => g.Party.UserId == user.GetPrimeUserId());

                gisEnrolment.LdapLoginSuccessDate = DateTime.Now;

                await _context.SaveChangesAsync();
            }

            return success;
        }

        public async Task<int> SubmitApplicationAsync(int gisId)
        {
            var gisEnrolment = await _context.GisEnrolments
                .SingleOrDefaultAsync(g => g.Id == gisId);

            gisEnrolment.SubmittedDate = DateTime.Now;

            return await _context.SaveChangesAsync();
        }
    }
}
