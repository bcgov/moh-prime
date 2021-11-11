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
using LinqKit;

namespace Prime.Services
{
    public class SiteService : BaseService, ISiteService
    {
        private readonly IBusinessEventService _businessEventService;
        private readonly IMapper _mapper;

        public SiteService(
            ApiDbContext context,
            ILogger<SiteService> logger,
            IBusinessEventService businessEventService,
            IMapper mapper)
            : base(context, logger)
        {
            _businessEventService = businessEventService;
            _mapper = mapper;
        }

        public async Task<bool> PecAssignableAsync(int siteId, string pec)
        {
            var siteDto = await _context.Sites
                .AsNoTracking()
                .Where(site => site.Id == siteId)
                .Select(site => new
                {
                    site.PEC,
                    healthAuthorityId = (int?)(site as HealthAuthoritySite).HealthAuthorityOrganizationId
                })
                .SingleAsync();

            if (siteDto.PEC == pec)
            {
                return true;
            }

            if (siteDto.healthAuthorityId.HasValue)
            {
                return !await _context.HealthAuthoritySites
                    .AsNoTracking()
                    .Where(
                        s => s.PEC == pec
                        && s.HealthAuthorityOrganizationId != siteDto.healthAuthorityId
                        )
                    .AnyAsync();
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
                .Include(site => site.PhysicalAddress)
                .SingleOrDefaultAsync(s => s.Id == siteId);

            if (site != null)
            {
                await _businessEventService.CreateSiteEventAsync(siteId, "Site Deleted");

                if (site.PhysicalAddress != null)
                {
                    _context.Addresses.Remove(site.PhysicalAddress);
                }

                if (site is CommunitySite communitySite)
                {
                    await DeleteContactFromSite(communitySite.AdministratorPharmaNetId);
                    await DeleteContactFromSite(communitySite.TechnicalSupportId);
                    await DeleteContactFromSite(communitySite.PrivacyOfficerId);
                }

                _context.Sites.Remove(site);
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

        public async Task<IEnumerable<RemoteAccessSearchViewModel>> GetRemoteUserInfoAsync(IEnumerable<CertSearchViewModel> certs)
        {
            if (certs == null || !certs.Any())
            {
                return Enumerable.Empty<RemoteAccessSearchViewModel>();
            }

            var matchesAnyCert = PredicateBuilder.New<RemoteUserCertification>();
            foreach (var searchedCert in certs)
            {
                matchesAnyCert.Or(ruc => ruc.CollegeCode == searchedCert.CollegeCode && ruc.LicenseNumber == searchedCert.LicenceNumber);
            }

            IEnumerable<RemoteAccessSearchDto> searchResults = await _context.RemoteUserCertifications
                .AsNoTracking()
                .AsExpandable()
                .Where(ruc => ruc.RemoteUser.Site.ApprovedDate.HasValue)
                .Where(matchesAnyCert)
                .ProjectTo<RemoteAccessSearchDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            // Remove duplicates since one RemoteUser could have multiple certs matching the search
            searchResults = searchResults
                .GroupBy(user => user.RemoteUserId)
                .Select(group => group.First());

            return _mapper.Map<IEnumerable<RemoteAccessSearchViewModel>>(searchResults);
        }

        private async Task DeleteContactFromSite(int? contactId)
        {
            if (contactId == null)
            {
                return;
            }

            var contact = await _context.Contacts
                .Include(contact => contact.PhysicalAddress)
                .SingleAsync(contact => contact.Id == contactId);

            if (contact.PhysicalAddress != null)
            {
                _context.Addresses.Remove(contact.PhysicalAddress);
            }
            _context.Contacts.Remove(contact);
        }

        public async Task<SiteRegistrationNote> CreateSiteRegistrationNoteAsync(int siteId, string note, int adminId)
        {
            var SiteRegistrationNote = new SiteRegistrationNote
            {
                SiteId = siteId,
                AdjudicatorId = adminId,
                Note = note,
                NoteDate = DateTimeOffset.Now
            };

            _context.SiteRegistrationNotes.Add(SiteRegistrationNote);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create site registration note.");
            }

            return SiteRegistrationNote;
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

        public async Task MarkUsersAsNotifiedAsync(IEnumerable<RemoteUser> notifiedUsers)
        {
            foreach (var wasNotified in notifiedUsers)
                wasNotified.Notified = true;
            {
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SiteExists(int siteId)
        {
            return await _context.Sites
                .AsNoTracking()
                .AnyAsync(s => s.Id == siteId);
        }
    }
}
