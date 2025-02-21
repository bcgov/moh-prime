using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                .Where(s => s.Id == siteId && s.DeletedDate == null)
                .Select(s => new PermissionsRecord { Username = s.AuthorizedUser.Party.Username })
                .SingleOrDefaultAsync();
        }

        public async Task<bool> AllowToSubmitSite(int siteId, string username)
        {
            var healthAuthorityCode = (int)await _context.AuthorizedUsers
                .Where(au => au.Party.Username == username)
                .Select(au => au.HealthAuthorityCode)
                .SingleOrDefaultAsync();

            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .Where(s => s.Id == siteId && s.DeletedDate == null)
                .Where(s => s.HealthAuthorityOrganization.Id == healthAuthorityCode).AnyAsync();
        }

        public async Task<bool> SiteExistsAsync(int healthAuthorityId, int siteId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .AnyAsync(s => s.Id == siteId && s.HealthAuthorityOrganizationId == healthAuthorityId && s.DeletedDate == null);
        }

        public async Task<bool> SiteIsEditableAsync(int healthAuthorityId, int siteId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .DecompileAsync()
                .AnyAsync(s => s.Id == siteId
                   && s.HealthAuthorityOrganizationId == healthAuthorityId
                   && s.Status == SiteStatusType.Editable
                   && s.DeletedDate == null);
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
            var siteList = await _context.HealthAuthoritySites
                .AsNoTracking()
                .Where(has => has.DeletedDate == null)
                .If(healthAuthorityId.HasValue, q => q.Where(site => site.HealthAuthorityOrganizationId == healthAuthorityId))
                .If(healthAuthoritySiteId.HasValue, q => q.Where(site => site.Id == healthAuthoritySiteId))
                .Include(has => has.SiteSubmissions)
                .Include(has => has.HealthAuthorityVendor)
                    .ThenInclude(hav => hav.Vendor)
                .Include(has => has.HealthAuthorityOrganization)
                .Include(has => has.HealthAuthorityCareType)
                .Include(has => has.AuthorizedUser)
                    .ThenInclude(au => au.Party)
                .Include(has => has.Adjudicator)
                .Include(has => has.SiteStatuses)
                .DecompileAsync()
                .ToListAsync();
            return _mapper.Map<List<HealthAuthoritySiteAdminListViewModel>>(siteList);
        }

        public async Task<IEnumerable<HealthAuthoritySiteAdminListViewModel>> GetSitesAsync(HealthAuthoritySiteSearchOptions searchOptions)
        {
            searchOptions ??= new HealthAuthoritySiteSearchOptions();

            int? statusId = null;
            bool flagged = false;
            if (searchOptions.StatusId.HasValue)
            {
                if (searchOptions.StatusId == (int)SiteStatusType.Flagged)
                {
                    flagged = true;
                }
                else
                {
                    statusId = searchOptions.StatusId == (int)SiteStatusType.EditableNotApproved ?
                        (int)SiteStatusType.Editable : searchOptions.StatusId;
                }
            }

            var query = _context.HealthAuthoritySites
                .AsNoTracking()
                .Where(s => s.DeletedDate == null)
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
                .If(statusId.HasValue,
                    q => q.Where(s => (int)s.SiteStatuses.OrderByDescending(ss => ss.StatusDate)
                    .FirstOrDefault().StatusType == statusId &&
                    ((searchOptions.StatusId == (int)SiteStatusType.Editable && s.ApprovedDate.HasValue) ||
                    (searchOptions.StatusId == (int)SiteStatusType.EditableNotApproved && !s.ApprovedDate.HasValue) ||
                    searchOptions.StatusId == (int)SiteStatusType.InReview ||
                    searchOptions.StatusId == (int)SiteStatusType.Locked)))
                .If(flagged, q => q.Where(s => s.Flagged))
                .If(!string.IsNullOrWhiteSpace(searchOptions.AdminUserName),
                    q => q.Where(s => s.Adjudicator.Username == searchOptions.AdminUserName))
                .If(searchOptions.VendorId.HasValue,
                    q => q.Where(s => s.HealthAuthorityVendor.Vendor.Code == searchOptions.VendorId))
                .If(!string.IsNullOrWhiteSpace(searchOptions.CareType),
                    q => q.Where(s => s.HealthAuthorityCareType.CareType == searchOptions.CareType))
                .ProjectTo<HealthAuthoritySiteAdminListViewModel>(_mapper.ConfigurationProvider)
                .DecompileAsync()
                .OrderBy(s => s.SiteName);

            var matchingHASites = await query.ToListAsync();
            // check for duplicate site id
            foreach (var site in matchingHASites)
            {
                site.DuplicatePecSiteCount = await GetDuplicatePecCount(site.PEC, site.HealthAuthorityOrganizationId, site.Id);
            }

            return matchingHASites;
        }

        private async Task<int> GetDuplicatePecCount(string pec, int originalHASiteId, int originalSiteId)
        {
            return await _context.HealthAuthoritySites
                    .Where(s => s.PEC != null && s.PEC == pec && s.HealthAuthorityOrganizationId == originalHASiteId && originalSiteId != s.Id && s.DeletedDate == null)
                    .CountAsync();
        }

        public async Task<HealthAuthoritySiteViewModel> GetSiteViewModelAsync(int siteId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .Where(s => s.DeletedDate == null)
                .ProjectTo<HealthAuthoritySiteViewModel>(_mapper.ConfigurationProvider)
                .DecompileAsync()
                .SingleOrDefaultAsync(has => has.Id == siteId);
        }

        public async Task<HealthAuthoritySite> GetHealthAuthoritySiteAsync(int siteId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .Include(s => s.HealthAuthorityOrganization)
                .Include(s => s.HealthAuthorityVendor)
                    .ThenInclude(v => v.Vendor)
                .Where(s => s.Id == siteId && s.DeletedDate == null)
                .FirstOrDefaultAsync();
        }

        public async Task<HealthAuthoritySiteAdminViewModel> GetAdminSiteAsync(int siteId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .Where(s => s.DeletedDate == null)
                .ProjectTo<HealthAuthoritySiteAdminViewModel>(_mapper.ConfigurationProvider)
                .DecompileAsync()
                .SingleOrDefaultAsync(has => has.Id == siteId);
        }

        public async Task UpdateSiteAsync(int siteId, HealthAuthoritySiteUpdateModel updateModel, int authorizedUserId)
        {
            var site = await _context.HealthAuthoritySites
                .Include(site => site.PhysicalAddress)
                .Include(site => site.BusinessHours)
                .SingleOrDefaultAsync(has => has.Id == siteId && has.DeletedDate == null);

            site.AuthorizedUserId = authorizedUserId;

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
            var site = await GetSiteAsync(siteId);

            site.Completed = true;

            await _context.SaveChangesAsync();
        }

        public async Task SiteSubmissionAsync(int siteId)
        {
            var site = await GetSiteAsync(siteId);

            site.SubmittedDate = DateTimeOffset.Now;
            site.AddStatus(SiteStatusType.InReview);

            await _context.SaveChangesAsync();
        }

        public async Task<List<int>> TransferAuthorizedUserAsync(int currentAuthorizedUseId, int newAuthorizeduserId)
        {
            var sites = await _context.HealthAuthoritySites
                .Where(s => s.AuthorizedUserId == currentAuthorizedUseId)
                .ToListAsync();

            foreach (var site in sites)
            {
                site.AuthorizedUserId = newAuthorizeduserId;
            }

            await _context.SaveChangesAsync();

            return sites.Select(s => s.Id).ToList();
        }

        public async Task<Site> GetSiteAsync(int siteId)
        {
            return await GetBaseSiteQuery()
                .SingleOrDefaultAsync(s => s.Id == siteId);
        }

        private IQueryable<Site> GetBaseSiteQuery()
        {
            return _context.HealthAuthoritySites
                .Where(s => s.DeletedDate == null);
        }
    }
}
