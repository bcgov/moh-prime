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

        public Task<BusinessEvent> CreateAdminClaimEventAsync(int enrolleeId, string description)
        {
            var businessEvent = new BusinessEvent
            {
                EnrolleeId = enrolleeId,
                AdminId = 0,
                BusinessEventTypeCode = BusinessEventType.ADMIN_CLAIM_CODE,
                Description = description,
                EventDate = DateTime.Now
            };
            return Task.FromResult(businessEvent);
        }

        public Task<BusinessEvent> CreateEmailEventAsync(int enrolleeId, string description)
        {
            var businessEvent = new BusinessEvent
            {
                EnrolleeId = enrolleeId,
                AdminId = 0,
                BusinessEventTypeCode = BusinessEventType.EMAIL_CODE,
                Description = description,
                EventDate = DateTime.Now
            };
            return Task.FromResult(businessEvent);
        }

        public Task<BusinessEvent> CreateNoteEventAsync(int enrolleeId, string description)
        {
            var businessEvent = new BusinessEvent
            {
                EnrolleeId = enrolleeId,
                AdminId = 0,
                BusinessEventTypeCode = BusinessEventType.NOTE_CODE,
                Description = description,
                EventDate = DateTime.Now
            };
            return Task.FromResult(businessEvent);
        }

        public Task<BusinessEvent> CreateEnrolleeEventAsync(int enrolleeId, string description)
        {
            var businessEvent = new BusinessEvent
            {
                EnrolleeId = enrolleeId,
                AdminId = 0,
                BusinessEventTypeCode = BusinessEventType.ENROLLEE_CODE,
                Description = description,
                EventDate = DateTime.Now
            };
            return Task.FromResult(businessEvent);
        }

        public Task<BusinessEvent> CreateStatusChangeEventAsync(int enrolleeId, string description)
        {
            var businessEvent = new BusinessEvent
            {
                EnrolleeId = enrolleeId,
                AdminId = 0,
                BusinessEventTypeCode = BusinessEventType.STATUS_CHANGE_CODE,
                Description = description,
                EventDate = DateTime.Now
            };
            return Task.FromResult(businessEvent);
        }

        public Task<BusinessEvent> CreateSiteEventAsync(int siteId, int partyId, string description)
        {
            var businessEvent = new BusinessEvent
            {
                SiteId = siteId,
                PartyId = partyId,
                BusinessEventTypeCode = BusinessEventType.SITE_CODE,
                Description = description,
                EventDate = DateTime.Now
            };
            return Task.FromResult(businessEvent);
        }
    }
}
