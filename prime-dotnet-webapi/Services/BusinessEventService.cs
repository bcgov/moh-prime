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

        public async Task<BusinessEvent> CreateBusinessEventAsync(int enrolleeId, short eventTypeCode, string description, int? adminId = null)
        {
            var businessEvent = new BusinessEvent
            {
                EnrolleeId = enrolleeId,
                AdminId = adminId,
                BusinessEventTypeCode = eventTypeCode,
                Description = description,
                EventDate = DateTime.Now
            };

            _context.BusinessEvents.Add(businessEvent);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create business event.");
            };

            return businessEvent;
        }
    }
}
