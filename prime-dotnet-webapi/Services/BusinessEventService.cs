using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prime.Models;

namespace Prime.Services
{
    public class BusinessEventService : BaseService, IBusinessEventService
    {
        private readonly IAdminService _adminService;

        public BusinessEventService(ApiDbContext context, IHttpContextAccessor httpContext,
            IAdminService adminService)
            : base(context, httpContext)
        {
            _adminService = adminService;
        }

        public async Task<BusinessEvent> CreateStatusChangeEventAsync(int enrolleeId, string description)
        {
            var businessEvent = await CreateBusinessEvent(BusinessEventType.STATUS_CHANGE_CODE, enrolleeId, description);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create status change business event.");
            };

            return businessEvent;
        }

        public async Task<BusinessEvent> CreateEmailEventAsync(int enrolleeId, string description)
        {
            var businessEvent = await CreateBusinessEvent(BusinessEventType.EMAIL_CODE, enrolleeId, description);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create email business event.");
            };

            return businessEvent;
        }

        public async Task<BusinessEvent> CreateNoteEventAsync(int enrolleeId, string description)
        {
            var businessEvent = await CreateBusinessEvent(BusinessEventType.NOTE_CODE, enrolleeId, description);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create note business event.");
            };

            return businessEvent;
        }

        public async Task<BusinessEvent> CreateAdminClaimEventAsync(int enrolleeId, string description)
        {
            var businessEvent = await CreateBusinessEvent(BusinessEventType.ADMIN_CLAIM_CODE, enrolleeId, description);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create admin claim business event.");
            };

            return businessEvent;
        }

        public async Task<BusinessEvent> CreateAdminViewEventAsync(int enrolleeId, string description)
        {
            var businessEvent = await CreateBusinessEvent(BusinessEventType.ADMIN_VIEW_CODE, enrolleeId, description);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create admin claim business event.");
            };

            return businessEvent;
        }

        public async Task<BusinessEvent> CreateEnrolleeEventAsync(int enrolleeId, string description)
        {
            var businessEvent = await this.CreateBusinessEvent(BusinessEventType.ENROLLEE_CODE, enrolleeId, description);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create enrollee business event.");
            };

            return businessEvent;
        }

        public async Task<BusinessEvent> CreateSiteEventAsync(int siteId, int partyId, string description)
        {
            var businessEvent = await this.CreateSiteBusinessEvent(BusinessEventType.SITE_CODE, siteId, partyId, description);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create site business event.");
            };

            return businessEvent;
        }

        private async Task<BusinessEvent> CreateBusinessEvent(int BusinessEventTypeCode, int enrolleeId, string description)
        {
            var userId = _httpContext.HttpContext.User.GetPrimeUserId();
            Admin admin = await _adminService.GetAdminForUserIdAsync(userId);
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

        private async Task<BusinessEvent> CreateSiteBusinessEvent(int BusinessEventTypeCode, int siteId, int partyId, string description)
        {
            var userId = _httpContext.HttpContext.User.GetPrimeUserId();
            Admin admin = await _adminService.GetAdminForUserIdAsync(userId);
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
