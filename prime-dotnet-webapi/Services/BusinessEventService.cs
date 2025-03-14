using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

using Prime.Models;

namespace Prime.Services
{
    public class BusinessEventService : BaseService, IBusinessEventService
    {
        private readonly IAdminService _adminService;
        private readonly IHttpContextAccessor _httpContext;

        public BusinessEventService(
            ApiDbContext context,
            ILogger<BusinessEventService> logger,
            IAdminService adminService,
            IHttpContextAccessor httpContext)
            : base(context, logger)
        {
            _adminService = adminService;
            _httpContext = httpContext;
        }

        public async Task<BusinessEvent> CreateStatusChangeEventAsync(int enrolleeId, string description)
        {
            var businessEvent = await CreateBusinessEvent(BusinessEventType.StatusChange, enrolleeId, description);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create status change business event.");
            }

            return businessEvent;
        }

        public async Task<BusinessEvent> CreateEmailEventAsync(string description)
        {
            var businessEvent = await CreateBusinessEvent(BusinessEventType.Email, description);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create email business event.");
            }

            return businessEvent;
        }

        public async Task<BusinessEvent> CreateEmailEventAsync(int enrolleeId, string description)
        {
            var businessEvent = await CreateBusinessEvent(BusinessEventType.Email, enrolleeId, description);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create email business event.");
            }

            return businessEvent;
        }

        public async Task<BusinessEvent> CreateNoteEventAsync(int enrolleeId, string description)
        {
            var businessEvent = await CreateBusinessEvent(BusinessEventType.Note, enrolleeId, description);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create note business event.");
            }

            return businessEvent;
        }

        public async Task<BusinessEvent> CreateAdminActionEventAsync(int enrolleeId, string description)
        {
            var businessEvent = await CreateBusinessEvent(BusinessEventType.AdminAction, enrolleeId, description);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create admin action business event.");
            }

            return businessEvent;
        }

        public async Task<BusinessEvent> CreateAdminViewEventAsync(int enrolleeId, string description)
        {
            var businessEvent = await CreateBusinessEvent(BusinessEventType.AdminView, enrolleeId, description);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create admin view business event.");
            }

            return businessEvent;
        }

        public async Task<BusinessEvent> CreateEnrolleeEventAsync(int enrolleeId, string description)
        {
            var businessEvent = await CreateBusinessEvent(BusinessEventType.Enrollee, enrolleeId, description);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create enrollee business event.");
            }

            return businessEvent;
        }

        public async Task<BusinessEvent> CreateEnrolleeAbsenceAsync(int enrolleeId, string description)
        {
            var businessEvent = await CreateBusinessEvent(BusinessEventType.Absence, enrolleeId, description);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create enrollee business event.");
            }

            return businessEvent;
        }


        public async Task<BusinessEvent> CreateSiteEventAsync(int siteId, int partyId, string description)
        {
            var businessEvent = await CreateSiteBusinessEvent(BusinessEventType.Site, siteId, partyId, description);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create site business event.");
            }

            return businessEvent;
        }

        public async Task<BusinessEvent> CreateSiteEmailEventAsync(int siteId, int partyId, string description)
        {
            var businessEvent = await CreateSiteBusinessEvent(BusinessEventType.Email, siteId, partyId, description);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create site email business event.");
            }

            return businessEvent;
        }

        public async Task<BusinessEvent> CreateSiteEmailEventAsync(int siteId, string username, string description)
        {
            var partyId = await _context.Parties
                .Where(p => p.Username == username)
                .Select(p => p.Id)
                .SingleOrDefaultAsync();

            return await CreateSiteEmailEventAsync(siteId, partyId, description);
        }

        public async Task<BusinessEvent> CreateSiteEmailEventAsync(int siteId, string description)
        {
            var site = await _context.Sites
                .SingleOrDefaultAsync(s => s.Id == siteId);

            var partyId = site switch
            {
                CommunitySite c => await _context.CommunitySites
                    .AsNoTracking()
                    .Where(site => site.Id == siteId)
                    .Select(site => site.Organization.SigningAuthorityId)
                    .SingleAsync(),
                HealthAuthoritySite h => await _context.HealthAuthoritySites
                    .AsNoTracking()
                    .Where(site => site.Id == siteId)
                    .Select(site => site.AuthorizedUser.PartyId)
                    .SingleAsync(),
                _ => throw new NotImplementedException($"Unknown Site Type in {nameof(CreateSiteEmailEventAsync)}: {site.GetType()}")
            };

            return await CreateSiteEmailEventAsync(siteId, partyId, description);
        }

        public async Task<BusinessEvent> CreateSiteEventAsync(int siteId, string description)
        {
            var site = await _context.Sites
                .SingleOrDefaultAsync(s => s.Id == siteId);

            var partyId = site switch
            {
                CommunitySite c => await _context.CommunitySites
                    .AsNoTracking()
                    .Where(site => site.Id == siteId)
                    .Select(site => site.Organization.SigningAuthorityId)
                    .SingleAsync(),
                HealthAuthoritySite h => await _context.HealthAuthoritySites
                    .AsNoTracking()
                    .Where(site => site.Id == siteId)
                    .Select(site => site.AuthorizedUser.PartyId)
                    .SingleAsync(),
                _ => throw new NotImplementedException($"Unknown Site Type in {nameof(CreateSiteEventAsync)}: {site.GetType()}")
            };

            return await CreateSiteEventAsync(siteId, partyId, description);
        }

        public async Task<BusinessEvent> CreateSiteEventAsync(int siteId, string username, string description)
        {
            var partyId = await _context.Parties
                .Where(p => p.Username == username)
                .Select(p => p.Id)
                .SingleOrDefaultAsync();

            return await CreateSiteEventAsync(siteId, partyId, description);
        }

        public async Task<BusinessEvent> CreateOrganizationEventAsync(int organizationId, int partyId, string description)
        {
            var username = _httpContext.HttpContext.User.GetPrimeUsername();
            Admin admin = await _adminService.GetAdminAsync(username);
            int? adminId = admin?.Id;

            var businessEvent = new BusinessEvent
            {
                PartyId = partyId,
                OrganizationId = organizationId,
                AdminId = adminId,
                BusinessEventTypeCode = BusinessEventType.Organization,
                Description = description,
                EventDate = DateTimeOffset.Now
            };

            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create organization business event.");
            }

            return businessEvent;
        }

        public async Task<BusinessEvent> CreatePharmanetApiCallEventAsync(int enrolleeId, string licencePrefix, string licenceNumber, string description, bool overrideWithDesc)
        {
            string message;

            if (overrideWithDesc)
            {
                message = description;
            }
            else
            {
                message = $"Called PharmaNet API with licence prefix {licencePrefix} and licence number {licenceNumber}:  {description}";
            }

            var businessEvent = await CreateBusinessEvent(BusinessEventType.PharmanetApiCall, enrolleeId, message);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create PharmaNet API call event.");
            }

            return businessEvent;
        }

        public async Task<BusinessEvent> CreatePaperEnrolmentLinkEventAsync(int enrolleeId, string description)
        {
            var businessEvent = await CreateBusinessEvent(BusinessEventType.PaperEnrolmentLink, enrolleeId, description);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create paper enrolment link event.");
            }

            return businessEvent;
        }

        private async Task<BusinessEvent> CreateBusinessEvent(int BusinessEventTypeCode, int enrolleeId, string description)
        {
            var username = _httpContext.HttpContext.User.GetPrimeUsername();
            Admin admin = await _adminService.GetAdminAsync(username);
            int? adminId = admin?.Id;

            var businessEvent = new BusinessEvent
            {
                EnrolleeId = enrolleeId,
                AdminId = adminId,
                BusinessEventTypeCode = BusinessEventTypeCode,
                Description = description,
                EventDate = DateTimeOffset.Now
            };

            return businessEvent;
        }

        private async Task<BusinessEvent> CreateBusinessEvent(int BusinessEventTypeCode, string description)
        {
            var username = _httpContext.HttpContext.User.GetPrimeUsername();
            Admin admin = await _adminService.GetAdminAsync(username);
            int? adminId = admin?.Id;

            var businessEvent = new BusinessEvent
            {
                AdminId = adminId,
                BusinessEventTypeCode = BusinessEventTypeCode,
                Description = description,
                EventDate = DateTimeOffset.Now
            };

            return businessEvent;
        }

        private async Task<BusinessEvent> CreateSiteBusinessEvent(int BusinessEventTypeCode, int siteId, int partyId, string description)
        {
            var username = _httpContext.HttpContext.User.GetPrimeUsername();
            Admin admin = await _adminService.GetAdminAsync(username);
            int? adminId = admin?.Id;

            var businessEvent = new BusinessEvent
            {
                PartyId = partyId,
                SiteId = siteId,
                AdminId = adminId,
                BusinessEventTypeCode = BusinessEventTypeCode,
                Description = description,
                EventDate = DateTimeOffset.Now
            };

            return businessEvent;
        }
    }
}
