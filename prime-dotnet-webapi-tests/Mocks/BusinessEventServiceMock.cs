using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prime.Models;
using Prime.Services;

namespace PrimeTests.Mocks
{
    public class BusinessEventServiceMock : BaseMockService, IBusinessEventService
    {
        public BusinessEventServiceMock() : base()
        { }


        public override void SeedData()
        { }

        public Task<BusinessEvent> CreateAdminClaimEventAsync(int enrolleeId, string description, int? adminId = null)
        {
            var businessEvent = new BusinessEvent
            {
                EnrolleeId = enrolleeId,
                AdminId = adminId,
                BusinessEventTypeCode = BusinessEventType.ADMIN_CLAIM_CODE,
                Description = description,
                EventDate = DateTime.Now
            };
            return Task.FromResult(businessEvent);
        }

        public Task<BusinessEvent> CreateEmailEventAsync(int enrolleeId, string description, int? adminId = null)
        {
            var businessEvent = new BusinessEvent
            {
                EnrolleeId = enrolleeId,
                AdminId = adminId,
                BusinessEventTypeCode = BusinessEventType.EMAIL_CODE,
                Description = description,
                EventDate = DateTime.Now
            };
            return Task.FromResult(businessEvent);
        }

        public Task<BusinessEvent> CreateNoteEventAsync(int enrolleeId, string description, int? adminId = null)
        {
            var businessEvent = new BusinessEvent
            {
                EnrolleeId = enrolleeId,
                AdminId = adminId,
                BusinessEventTypeCode = BusinessEventType.NOTE_CODE,
                Description = description,
                EventDate = DateTime.Now
            };
            return Task.FromResult(businessEvent);
        }

        public Task<BusinessEvent> CreateStatusChangeEventAsync(int enrolleeId, string description, int? adminId = null)
        {
            var businessEvent = new BusinessEvent
            {
                EnrolleeId = enrolleeId,
                AdminId = adminId,
                BusinessEventTypeCode = BusinessEventType.STATUS_CHANGE_CODE,
                Description = description,
                EventDate = DateTime.Now
            };
            return Task.FromResult(businessEvent);
        }

    }
}
