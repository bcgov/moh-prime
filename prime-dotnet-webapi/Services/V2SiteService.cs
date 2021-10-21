using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using Prime.Models;
using Prime.ViewModels;
using Prime.ViewModels.Sites;

using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Prime.Services
{
    public class V2SiteService : BaseService, IV2SiteService
    {
        private readonly IBusinessEventService _businessEventService;
        private readonly IMapper _mapper;

        public V2SiteService(
            ApiDbContext context,
            ILogger<V2SiteService> logger,
            IBusinessEventService businessEventService,
            IMapper mapper)
            : base(context, logger)
        {
            _businessEventService = businessEventService;
            _mapper = mapper;
        }

        public async Task<bool> PecAssignableAsync(int siteId, string pec)
        {
            if (string.IsNullOrWhiteSpace(pec))
            {
                return false;
            }

            var siteDto = await _context.Sites
                .AsNoTracking()
                .Where(site => site.Id == siteId)
                .Select(site => new
                {
                    site.CareSettingCode,
                    site.PEC
                })
                .SingleOrDefaultAsync();

            if (siteDto?.CareSettingCode == null)
            {
                return false;
            }

            if (siteDto.CareSettingCode == (int)CareSettingType.HealthAuthority
                || siteDto.PEC == pec)
            {
                return true;
            }

            return !await _context.Sites
                .AsNoTracking()
                .AnyAsync(site => site.PEC == pec);
        }

        public async Task UpdateCompletedAsync(int siteId, bool completed)
        {
            var site = await _context.Sites
                .SingleOrDefaultAsync(s => s.Id == siteId);

            if (site == null)
            {
                throw new ArgumentException($"Could not set Completed on Site {siteId}, it doesn't exist.");
            }

            site.Completed = completed;

            await _context.SaveChangesAsync();
        }

        public async Task<Site> UpdateSiteAdjudicator(int siteId, int? adminId = null)
        {
            var site = await _context.Sites
                .SingleOrDefaultAsync(s => s.Id == siteId);

            if (site == null)
            {
                throw new ArgumentException($"Could not Update Adjudicator on Site {siteId}, it doesn't exist.");
            }

            site.AdjudicatorId = adminId;
            await _context.SaveChangesAsync();

            return site;
        }

        public async Task<Site> UpdatePecCode(int siteId, string pecCode)
        {
            var site = await _context.Sites
                .SingleOrDefaultAsync(s => s.Id == siteId);

            site.PEC = pecCode;

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not update the site.");
            }

            await _businessEventService.CreateSiteEventAsync(site.Id, "Site ID (PEC Code) associated with site");

            return site;
        }

        public async Task DeleteSiteAsync(int siteId)
        {
            var site = await _context.Sites
                .SingleOrDefaultAsync(s => s.Id == siteId);

            if (site != null)
            {
                var provisionerId = site.ProvisionerId;

                if (site.PhysicalAddress != null)
                {
                    _context.Addresses.Remove(site.PhysicalAddress);
                }

                DeleteContactFromSite(site.AdministratorPharmaNet);
                DeleteContactFromSite(site.TechnicalSupport);
                DeleteContactFromSite(site.PrivacyOfficer);

                _context.Sites.Remove(site);

                await _businessEventService.CreateSiteEventAsync(siteId, "Site Deleted");

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Site> ApproveSite(int siteId)
        {
            var site = await _context.Sites.SingleOrDefaultAsync(s => s.Id == siteId);

            site.AddStatus(SiteStatusType.Editable);
            site.ApprovedDate = DateTimeOffset.Now;
            await _context.SaveChangesAsync();

            await _businessEventService.CreateSiteEventAsync(site.Id, "Site Approved");

            return site;
        }

        public async Task<Site> DeclineSite(int siteId)
        {
            var site = await _context.Sites.SingleOrDefaultAsync(s => s.Id == siteId);
            site.AddStatus(SiteStatusType.Locked);
            site.ApprovedDate = null;
            await _context.SaveChangesAsync();

            await _businessEventService.CreateSiteEventAsync(site.Id, "Site Declined");

            return site;
        }

        public async Task<Site> UnrejectSite(int siteId)
        {
            var site = await _context.Sites.SingleOrDefaultAsync(s => s.Id == siteId);
            site.AddStatus(SiteStatusType.InReview);
            await _context.SaveChangesAsync();

            await _businessEventService.CreateSiteEventAsync(site.Id, "Site Unrejected");

            return site;
        }

        public async Task<Site> EnableEditingSite(int siteId)
        {
            var site = await _context.Sites.SingleOrDefaultAsync(s => s.Id == siteId);
            site.ApprovedDate = null;
            site.AddStatus(SiteStatusType.Editable);
            await _context.SaveChangesAsync();

            await _businessEventService.CreateSiteEventAsync(site.Id, "Site Enabled Editing");

            return site;
        }

        public async Task<Site> SubmitRegistrationAsync(int siteId)
        {
            var site = await _context.Sites.SingleOrDefaultAsync(s => s.Id == siteId);
            site.SubmittedDate = DateTimeOffset.Now;
            site.AddStatus(SiteStatusType.InReview);
            _context.Update(site);

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not submit the site.");
            }

            await _businessEventService.CreateSiteEventAsync(site.Id, "Site Submitted");

            return site;
        }

        public async Task<IEnumerable<SiteRegistrationNoteViewModel>> GetSiteRegistrationNotesAsync(int siteId)
        {
            return await _context.SiteRegistrationNotes
                .Where(srn => srn.SiteId == siteId)
                .Include(srn => srn.Adjudicator)
                .OrderByDescending(srn => srn.NoteDate)
                .ProjectTo<SiteRegistrationNoteViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<SiteRegistrationNoteViewModel> GetSiteRegistrationNoteAsync(int siteId, int siteRegistrationNoteId)
        {
            return await _context.SiteRegistrationNotes
                .Where(srn => srn.SiteId == siteId)
                .Include(srn => srn.Adjudicator)
                .Include(srn => srn.SiteNotification)
                    .ThenInclude(sre => sre.Admin)
                .ProjectTo<SiteRegistrationNoteViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(srn => srn.Id == siteRegistrationNoteId);
        }

        public async Task<IEnumerable<BusinessDayViewModel>> GetBusinessHoursAsync(int siteId)
        {
            return await _context.Set<BusinessDay>()
                .Where(day => day.SiteId == siteId)
                .ProjectTo<BusinessDayViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<RemoteUserViewModel>> GetRemoteUsersAsync(int siteId)
        {
            return await _context.RemoteUsers
                .Where(user => user.SiteId == siteId)
                .ProjectTo<RemoteUserViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        private void DeleteContactFromSite(Contact contact)
        {
            if (contact != null)
            {
                if (contact.PhysicalAddress != null)
                {
                    _context.Addresses.Remove(contact.PhysicalAddress);
                }
                _context.Contacts.Remove(contact);
            }
        }

    }
}
