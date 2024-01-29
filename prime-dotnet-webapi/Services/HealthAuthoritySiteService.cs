using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelegateDecompiler;
using DelegateDecompiler.EntityFrameworkCore;
using Prime.Models;
using Prime.ViewModels.HealthAuthoritySites;
using Prime.Models.Api;

namespace Prime.Services
{
    public class HealthAuthoritySiteService : BaseService, IHealthAuthoritySiteService
    {
        private readonly IMapper _mapper;
        private readonly IBusinessEventService _businessEventService;

        public HealthAuthoritySiteService(
            ApiDbContext context,
            ILogger<HealthAuthoritySiteService> logger,
            IMapper mapper,
            IBusinessEventService businessEventService)
            : base(context, logger)
        {
            _mapper = mapper;
            _businessEventService = businessEventService;
        }

        public async Task<PermissionsRecord> GetPermissionsRecordAsync(int siteId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .Where(s => s.Id == siteId)
                .Select(s => new PermissionsRecord { Username = s.AuthorizedUser.Party.Username })
                .SingleOrDefaultAsync();
        }

        public async Task<bool> SiteExistsAsync(int healthAuthorityId, int siteId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .AnyAsync(s => s.Id == siteId && s.HealthAuthorityOrganizationId == healthAuthorityId);
        }

        public async Task<bool> SiteIsEditableAsync(int healthAuthorityId, int siteId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .DecompileAsync()
                .AnyAsync(s => s.Id == siteId
                   && s.HealthAuthorityOrganizationId == healthAuthorityId
                   && s.Status == SiteStatusType.Editable);
        }

        public async Task<HealthAuthoritySiteViewModel> CreateSiteAsync(int healthAuthorityId, HealthAuthoritySiteCreateModel createModel)
        {
            var site = new HealthAuthoritySite
            {
                HealthAuthorityOrganizationId = healthAuthorityId,
                HealthAuthorityCareTypeId = createModel.HealthAuthorityCareTypeId,
                HealthAuthorityVendorId = createModel.healthAuthorityVendorId,
                AuthorizedUserId = createModel.AuthorizedUserId,
                CareSettingCode = (int)CareSettingType.HealthAuthority
            };
            site.AddStatus(SiteStatusType.Editable);

            _context.HealthAuthoritySites.Add(site);
            await _context.SaveChangesAsync();

            await _businessEventService.CreateSiteEventAsync(site.Id, "Health Authority Site Created");

            return _mapper.Map<HealthAuthoritySiteViewModel>(site);
        }

        public async Task<IEnumerable<HealthAuthoritySiteAdminListViewModel>> GetSitesAsync(int? healthAuthorityId = null, int? healthAuthoritySiteId = null)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .If(healthAuthorityId.HasValue, q => q.Where(site => site.HealthAuthorityOrganizationId == healthAuthorityId))
                .If(healthAuthoritySiteId.HasValue, q => q.Where(site => site.Id == healthAuthoritySiteId))
                .ProjectTo<HealthAuthoritySiteAdminListViewModel>(_mapper.ConfigurationProvider)
                .DecompileAsync()
                .ToListAsync();
        }

        public async Task<IEnumerable<HealthAuthoritySiteAdminListViewModel>> GetSitesAsync(HealthAuthoritySiteSearchOptions searchOptions)
        {
            searchOptions ??= new HealthAuthoritySiteSearchOptions();

            var query = _context.HealthAuthoritySites
                .AsNoTracking()
                .If(!string.IsNullOrWhiteSpace(searchOptions.TextSearch),
                    q => q.Search(
                        s => s.SiteName,
                        s => s.PEC,
                        s => s.AuthorizedUser.Party.FirstName,
                        s => s.AuthorizedUser.Party.LastName,
                        s => s.HealthAuthorityOrganization.Name,
                        s => s.HealthAuthorityVendor.Vendor.Name,
                        s => s.HealthAuthorityCareType.CareType)
                        .Containing(searchOptions.TextSearch)
                        )
                .If(searchOptions.StatusId.HasValue,
                    q => q.Where(s => (int)s.SiteStatuses.OrderByDescending(ss => ss.StatusDate)
                    .FirstOrDefault().StatusType == searchOptions.StatusId))
                .If(!string.IsNullOrWhiteSpace(searchOptions.AdminUserName),
                    q => q.Where(s => s.Adjudicator.Username == searchOptions.AdminUserName))
                .If(searchOptions.VendorId.HasValue,
                    q => q.Where(s => s.HealthAuthorityVendor.Vendor.Code == searchOptions.VendorId))
                .If(!string.IsNullOrWhiteSpace(searchOptions.CareType),
                    q => q.Where(s => s.HealthAuthorityCareType.CareType == searchOptions.CareType))
                .ProjectTo<HealthAuthoritySiteAdminListViewModel>(_mapper.ConfigurationProvider)
                .DecompileAsync();

            return await query.ToListAsync();
        }

        public async Task<HealthAuthoritySiteViewModel> GetSiteAsync(int siteId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .ProjectTo<HealthAuthoritySiteViewModel>(_mapper.ConfigurationProvider)
                .DecompileAsync()
                .SingleOrDefaultAsync(has => has.Id == siteId);
        }

        public async Task<HealthAuthoritySiteAdminViewModel> GetAdminSiteAsync(int siteId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .ProjectTo<HealthAuthoritySiteAdminViewModel>(_mapper.ConfigurationProvider)
                .DecompileAsync()
                .SingleOrDefaultAsync(has => has.Id == siteId);
        }

        public async Task UpdateSiteAsync(int siteId, HealthAuthoritySiteUpdateModel updateModel)
        {
            var site = await _context.HealthAuthoritySites
                .Include(site => site.PhysicalAddress)
                .Include(site => site.BusinessHours)
                .SingleOrDefaultAsync(has => has.Id == siteId);

            _context.Entry(site).CurrentValues.SetValues(updateModel);

            if (updateModel.PhysicalAddress != null)
            {
                if (site.PhysicalAddress != null)
                {
                    _context.Addresses.Remove(site.PhysicalAddress);
                }
                site.PhysicalAddress = _mapper.Map<PhysicalAddress>(updateModel.PhysicalAddress);
            }

            if (updateModel.BusinessHours != null)
            {
                _context.RemoveRange(site.BusinessHours);
                site.BusinessHours = _mapper.Map<ICollection<BusinessDay>>(updateModel.BusinessHours);
            }

            await _context.SaveChangesAsync();
        }

        public async Task SetSiteCompletedAsync(int siteId)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            site.Completed = true;

            await _context.SaveChangesAsync();

            await _businessEventService.CreateSiteEventAsync(site.Id, "Health Authority Site Completed");
        }

        public async Task SiteSubmissionAsync(int siteId)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            site.SubmittedDate = DateTimeOffset.Now;
            site.AddStatus(SiteStatusType.InReview);

            await _context.SaveChangesAsync();

            await _businessEventService.CreateSiteEventAsync(site.Id, "Health Authority Site Submitted");
        }
    }
}
