using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Prime.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Prime.Services
{
    public class SiteSubmissionService : BaseService, ISiteSubmissionService
    {

        public SiteSubmissionService(
                ApiDbContext context,
                ILogger<SubmissionService> logger
                ) : base(context, logger)
        {

        }

        public async Task CreateCommunitySiteSubmissionAsync(int siteId)
        {
            var communitySite = await _context.CommunitySites
                .AsNoTracking()
                .Include(cs => cs.BusinessLicences)
                    .ThenInclude(bl => bl.BusinessLicenceDocument)
                .Include(cs => cs.BusinessHours)
                .Include(cs => cs.SiteStatuses)
                .Include(cs => cs.SiteVendors)
                    .ThenInclude(sv => sv.Vendor)
                .Include(cs => cs.RemoteUsers)
                    .ThenInclude(ru => ru.RemoteUserCertification)
                        .ThenInclude(ruc => ruc.College)
                .Include(cs => cs.RemoteUsers)
                    .ThenInclude(ru => ru.RemoteUserCertification)
                        .ThenInclude(ruc => ruc.License)
                .Include(cs => cs.PhysicalAddress)
                .Include(cs => cs.AdministratorPharmaNet)
                    .ThenInclude(ap => ap.PhysicalAddress)
                .Include(cs => cs.PrivacyOfficer)
                    .ThenInclude(po => po.PhysicalAddress)
                .Include(cs => cs.TechnicalSupport)
                    .ThenInclude(ts => ts.PhysicalAddress)
                .Where(cs => cs.Id == siteId)
                .SingleOrDefaultAsync();

            var siteSubmission = new SiteSubmission
            {
                SiteId = siteId,
                ProfileSnapshot = JObject.FromObject(communitySite, JsonSerializer.Create(
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    })
                ),
                CreatedDate = DateTimeOffset.Now,
            };

            _context.SiteSubmissions.Add(siteSubmission);

            await _context.SaveChangesAsync();
        }

        public async Task CreateHealthAuthoritySiteSubmissionAsync(int siteId)
        {
            var healthAuthoritySite = await _context.HealthAuthoritySites
                .AsNoTracking()
                .Include(has => has.BusinessHours)
                .Include(has => has.SiteStatuses)
                .Include(has => has.PhysicalAddress)
                .Include(has => has.HealthAuthorityPharmanetAdministrator)
                    .ThenInclude(pa => pa.Contact)
                        .ThenInclude(c => c.PhysicalAddress)
                .Include(has => has.HealthAuthorityTechnicalSupport)
                    .ThenInclude(ts => ts.Contact)
                        .ThenInclude(c => c.PhysicalAddress)
                .Include(has => has.AuthorizedUser)
                    .ThenInclude(au => au.Party)
                        .ThenInclude(p => p.Addresses)
                            .ThenInclude(a => a.Address)
                .Include(has => has.HealthAuthorityOrganization)
                .Include(has => has.HealthAuthorityCareType)
                .Include(has => has.HealthAuthorityVendor)
                    .ThenInclude(hav => hav.Vendor)
                .Where(has => has.Id == siteId)
                .SingleOrDefaultAsync();

            var siteSubmission = new SiteSubmission
            {
                SiteId = siteId,
                ProfileSnapshot = JObject.FromObject(healthAuthoritySite, JsonSerializer.Create(
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    })
                ),
                CreatedDate = DateTimeOffset.Now,
            };

            _context.SiteSubmissions.Add(siteSubmission);

            await _context.SaveChangesAsync();
        }

        public async Task<List<SiteSubmission>> GetSiteSubmissionsAsync(int siteId)
        {
            return await _context.SiteSubmissions
                .Where(ss => ss.SiteId == siteId)
                .ToListAsync();
        }

        public async Task<SiteSubmission> GetSiteSubmissionAsync(int siteId, int siteSubmissionId)
        {
            return await _context.SiteSubmissions
            .Where(ss => ss.SiteId == siteId && ss.Id == siteSubmissionId)
            .SingleOrDefaultAsync();
        }
    }
}
