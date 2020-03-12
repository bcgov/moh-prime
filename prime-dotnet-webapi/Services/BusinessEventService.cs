using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prime.Models;

namespace Prime.Services
{
    public class BusinessEventService : BaseService, IBusinessEventService
    {
        public BusinessEventService(
            ApiDbContext context, IHttpContextAccessor httpContext) : base(context, httpContext)
        { }

<<<<<<< HEAD
        public async Task<BusinessEvent> CreateStatusChangeEventAsync(int enrolleeId, string description, int? adminId = null)
=======
        public BusinessEventService(IServiceProvider provider,
            IAdminService adminService)
            : base(provider)
>>>>>>> 64d2346... di
        {
            var businessEvent = this.CreateBusinessEvent(BusinessEventType.STATUS_CHANGE_CODE, enrolleeId, description, adminId);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create status change business event.");
            };

            return businessEvent;
        }


        public async Task<BusinessEvent> CreateEmailEventAsync(int enrolleeId, string description, int? adminId = null)
        {
            var businessEvent = this.CreateBusinessEvent(BusinessEventType.EMAIL_CODE, enrolleeId, description, adminId);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create email business event.");
            };

            return businessEvent;
        }


        public async Task<BusinessEvent> CreateNoteEventAsync(int enrolleeId, string description, int? adminId = null)
        {
            var businessEvent = this.CreateBusinessEvent(BusinessEventType.NOTE_CODE, enrolleeId, description, adminId);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create email business event.");
            };

            return businessEvent;
        }


        public async Task<BusinessEvent> CreateAdminClaimEventAsync(int enrolleeId, string description, int? adminId = null)
        {
            var businessEvent = this.CreateBusinessEvent(BusinessEventType.ADMIN_CLAIM_CODE, enrolleeId, description, adminId);
            _context.BusinessEvents.Add(businessEvent);
            var created = await _context.SaveChangesAsync();

            if (created < 1)
            {
                throw new InvalidOperationException("Could not create admin claim business event.");
            };

            return businessEvent;
        }


        private BusinessEvent CreateBusinessEvent(int BusinessEventTypeCode, int enrolleeId, string description, int? adminId = null)
        {
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
    }
}
