﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SimpleBase;
using Prime.Models;

namespace Prime.Services
{
    public class DefaultEnrolmentService : BaseService, IEnrolmentService
    {
        private class StatusWrapper
        {
            public Status Status { get; set; }
            public bool AdminOnly { get; set; }
        }

        private Dictionary<Status, StatusWrapper[]> _workflowStateMap;

        private static Status NULL_STATUS = new Status { Code = -1, Name = "No Status" };

        public DefaultEnrolmentService(
            ApiDbContext context, IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        private Dictionary<Status, StatusWrapper[]> GetWorkFlowStateMap()
        {
            if (_workflowStateMap == null)
            {
                // construct the workflow map
                Status IN_PROGRESS = _context.Statuses.Single(s => s.Code == Status.IN_PROGRESS_CODE);
                Status SUBMITTED = _context.Statuses.Single(s => s.Code == Status.SUBMITTED_CODE);
                Status APPROVED = _context.Statuses.Single(s => s.Code == Status.APPROVED_CODE);
                Status DECLINED = _context.Statuses.Single(s => s.Code == Status.DECLINED_CODE);
                Status ACCEPTED_TOS = _context.Statuses.Single(s => s.Code == Status.ACCEPTED_TOS_CODE);
                Status DECLINED_TOS = _context.Statuses.Single(s => s.Code == Status.DECLINED_TOS_CODE);

                _workflowStateMap = new Dictionary<Status, StatusWrapper[]>();
                _workflowStateMap.Add(NULL_STATUS, new[] { new StatusWrapper { Status = IN_PROGRESS, AdminOnly = false } });
                _workflowStateMap.Add(IN_PROGRESS, new[] { new StatusWrapper { Status = SUBMITTED, AdminOnly = false } });
                _workflowStateMap.Add(SUBMITTED, new[] { new StatusWrapper { Status = APPROVED, AdminOnly = true }, new StatusWrapper { Status = DECLINED, AdminOnly = true } });
                _workflowStateMap.Add(APPROVED, new[] { new StatusWrapper { Status = ACCEPTED_TOS, AdminOnly = false }, new StatusWrapper { Status = DECLINED_TOS, AdminOnly = false } });
                _workflowStateMap.Add(DECLINED, new StatusWrapper[0]);
                _workflowStateMap.Add(ACCEPTED_TOS, new StatusWrapper[0]);
                _workflowStateMap.Add(DECLINED_TOS, new StatusWrapper[0]);
            }

            return _workflowStateMap;
        }

        private ICollection<Status> GetAvailableStatuses(Status currentStatus)
        {
            ICollection<Status> availableStatuses = new List<Status>();
            var results = this.GetWorkFlowStateMap()[currentStatus ?? NULL_STATUS];
            var currentUser = _httpContext?.HttpContext?.User;
            foreach (var item in results)
            {
                if (!item.AdminOnly
                        || (currentUser != null
                                && currentUser.IsInRole(PrimeConstants.PRIME_ADMIN_ROLE)))
                {
                    availableStatuses.Add(item.Status);
                }
            }

            return availableStatuses;
        }

        public bool EnrolmentExists(int enrolmentId)
        {
            return _context.Enrolments.Any(e => e.Id == enrolmentId);
        }

        public async Task<Enrolment> GetEnrolmentAsync(int enrolmentId)
        {
            var entity = await _context.Enrolments
                .Include(e => e.Enrollee)
                .ThenInclude(e => e.PhysicalAddress)
                .Include(e => e.Enrollee)
                .ThenInclude(e => e.MailingAddress)
                .Include(e => e.Certifications)
                .Include(e => e.Jobs)
                .Include(e => e.Organizations)
                .Include(e => e.EnrolmentStatuses).ThenInclude(es => es.Status)
                .SingleOrDefaultAsync(e => e.Id == enrolmentId)
                ;

            if (entity != null)
            {
                // add the available statuses to the enrolment
                entity.AvailableStatuses = this.GetAvailableStatuses(entity.CurrentStatus?.Status);
            }

            return entity;
        }

        public async Task<Enrolment> GetEnrolmentForUserIdAsync(Guid userId)
        {
            var entity = await _context.Enrolments
                .Include(e => e.Enrollee)
                .ThenInclude(e => e.PhysicalAddress)
                .Include(e => e.Enrollee)
                .ThenInclude(e => e.MailingAddress)
                .Include(e => e.Certifications)
                .Include(e => e.Jobs)
                .Include(e => e.Organizations)
                .Include(e => e.EnrolmentStatuses).ThenInclude(es => es.Status)
                .SingleOrDefaultAsync(e => e.Enrollee.UserId == userId)
                ;

            if (entity != null)
            {
                // add the available statuses to the enrolment
                entity.AvailableStatuses = this.GetAvailableStatuses(entity.CurrentStatus?.Status);
            }

            return entity;
        }

        public async Task<IEnumerable<Enrolment>> GetEnrolmentsAsync(EnrolmentSearchOptions searchOptions)
        {
            IQueryable<Enrolment> query = _context.Enrolments
                .Include(e => e.Enrollee)
                .ThenInclude(e => e.PhysicalAddress)
                .Include(e => e.Enrollee)
                .ThenInclude(e => e.MailingAddress)
                .Include(e => e.Certifications)
                .Include(e => e.Jobs)
                .Include(e => e.Organizations)
                .Include(e => e.EnrolmentStatuses).ThenInclude(es => es.Status)
                ;

            if (searchOptions.StatusCode != null)
            {
                query = query.Where(e => e.EnrolmentStatuses.Single(es => es.IsCurrent).StatusCode == (short)searchOptions.StatusCode);
            }

            var items = await query.ToListAsync();

            foreach (var item in items)
            {
                // add the available statuses to the enrolment
                item.AvailableStatuses = this.GetAvailableStatuses(item.CurrentStatus?.Status);
            }

            return items;
        }

        public async Task<IEnumerable<Enrolment>> GetEnrolmentsForUserIdAsync(
            Guid userId)
        {
            IQueryable<Enrolment> query = _context.Enrolments
                .Include(e => e.Enrollee)
                .ThenInclude(e => e.PhysicalAddress)
                .Include(e => e.Enrollee)
                .ThenInclude(e => e.MailingAddress)
                .Include(e => e.Certifications)
                .Include(e => e.Jobs)
                .Include(e => e.Organizations)
                .Include(e => e.EnrolmentStatuses).ThenInclude(es => es.Status)
                .Where(e => e.Enrollee.UserId == userId)
                ;

            var items = await query.ToListAsync();

            foreach (var item in items)
            {
                // add the available statuses to the enrolment
                item.AvailableStatuses = this.GetAvailableStatuses(item.CurrentStatus?.Status);
            }

            return items;
        }

        public async Task<int?> CreateEnrolmentAsync(Enrolment enrolment)
        {
            if (enrolment == null)
            {
                throw new ArgumentNullException("Could not create an enrolment, the passed in Enrolment cannot be null.");
            }

            //create a status history record
            EnrolmentStatus enrolmentStatus = new EnrolmentStatus { Enrolment = enrolment, StatusCode = Status.IN_PROGRESS_CODE, StatusDate = DateTime.Now, IsCurrent = true };
            if (enrolment.EnrolmentStatuses == null)
            {
                enrolment.EnrolmentStatuses = new List<EnrolmentStatus>(0);
            }
            enrolment.EnrolmentStatuses.Add(enrolmentStatus);
            _context.Enrolments.Add(enrolment);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create enrolment.");
            }

            return enrolment.Id;
        }

        public async Task<int> UpdateEnrolmentAsync(Enrolment enrolment)
        {
            //get the enrollee from the enrolment
            Enrollee _enrollee = enrolment.Enrollee;
            var _enrolleeDb = _context.Enrollees.Include(e => e.PhysicalAddress).Include(e => e.MailingAddress).AsNoTracking().Where(e => e.Id == _enrollee.Id).FirstOrDefault();
            var _enrolmentDb = _context.Enrolments.Include(e => e.Certifications).Include(e => e.Jobs).Include(e => e.Organizations).AsNoTracking().Where(e => e.Id == enrolment.Id).FirstOrDefault();

            // remove existing addresses, and recreate if necessary
            this.ReplaceExistingAddress(_enrolleeDb.PhysicalAddress, _enrollee.PhysicalAddress, _enrollee);
            this.ReplaceExistingAddress(_enrolleeDb.MailingAddress, _enrollee.MailingAddress, _enrollee);

            // remove existing certifications, and recreate if necessary
            this.ReplaceExistingItems(_enrolmentDb.Certifications, enrolment.Certifications, enrolment);

            // remove existing jobs, and recreate if necessary
            this.ReplaceExistingItems(_enrolmentDb.Jobs, enrolment.Jobs, enrolment);

            // remove existing organizations, and recreate if necessary
            this.ReplaceExistingItems(_enrolmentDb.Organizations, enrolment.Organizations, enrolment);

            //update the enrolment to include the enrolleeId
            enrolment.EnrolleeId = (int)_enrollee.Id;
            _context.Entry(enrolment).State = EntityState.Modified;

            _context.Entry(_enrollee).State = EntityState.Modified;

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        private void ReplaceExistingAddress(Address dbAddress, Address newAddress, Enrollee enrollee)
        {
            // remove existing addresses
            if (dbAddress != null)
            {
                dbAddress.Enrollee = null;
                _context.Addresses.Remove(dbAddress);
            }
            // create the new addresses, if they exist
            if (newAddress != null)
            {
                newAddress.EnrolleeId = (int)enrollee.Id;
                _context.Entry(newAddress).State = EntityState.Added;
            }
        }

        private void ReplaceExistingItems<T>(ICollection<T> dbCollection, ICollection<T> newCollection, Enrolment enrolment) where T : class, IEnrolmentNavigationProperty
        {
            // remove existing items
            foreach (var item in dbCollection)
            {
                item.Enrolment = null;
                _context.Remove(item);
            }
            // create new items
            if (newCollection != null)
            {
                foreach (var item in newCollection)
                {
                    item.EnrolmentId = (int)enrolment.Id;
                    _context.Entry(item).State = EntityState.Added;
                }
            }
        }

        public async Task DeleteEnrolmentAsync(int enrolmentId)
        {
            var enrolment = await _context.Enrolments
                .SingleOrDefaultAsync(e => e.Id == enrolmentId);
            if (enrolment == null)
            {
                return;
            }

            _context.Enrolments.Remove(enrolment);
            _context.Enrollees.Remove(enrolment.Enrollee);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Status>> GetAvailableEnrolmentStatusesAsync(int enrolmentId)
        {
            var enrolment = await _context.Enrolments
                .Include(e => e.EnrolmentStatuses).ThenInclude(es => es.Status)
                .SingleOrDefaultAsync(e => e.Id == enrolmentId);
            if (enrolment == null)
            {
                return null;
            }

            return this.GetAvailableStatuses(enrolment.CurrentStatus?.Status);
        }

        public async Task<IEnumerable<EnrolmentStatus>> GetEnrolmentStatusesAsync(int enrolmentId)
        {
            IQueryable<EnrolmentStatus> query = _context.EnrolmentStatuses
                .Include(es => es.Status)
                .Where(es => es.EnrolmentId == enrolmentId)
                ;

            var items = await query.ToListAsync();

            return items;
        }

        public async Task<EnrolmentStatus> CreateEnrolmentStatusAsync(int enrolmentId, Status status)
        {
            if (status == null)
            {
                throw new ArgumentNullException("Could not create an enrolment status, the passed in Status cannot be null.");
            }

            var enrolment = await _context.Enrolments
                .AsNoTracking()
                .Include(e => e.EnrolmentStatuses).ThenInclude(es => es.Status)
                .SingleOrDefaultAsync(e => e.Id == enrolmentId);
            if (enrolment == null)
            {
                return null;
            }

            var currentStatus = enrolment.CurrentStatus?.Status;

            // make sure the status change is allowed
            if (IsStatusChangeAllowed(currentStatus ?? NULL_STATUS, status))
            {
                // update all of the existing statuses to not be current, and then create a new current status
                var existingEnrolmentStatuses = await this.GetEnrolmentStatusesAsync(enrolmentId);
                foreach (var enrolmentStatus in existingEnrolmentStatuses)
                {
                    enrolmentStatus.IsCurrent = false;
                }

                // create a new enrolment status
                var createdEnrolmentStatus = new EnrolmentStatus { EnrolmentId = enrolmentId, StatusCode = status.Code, StatusDate = DateTime.Now, IsCurrent = true };
                _context.EnrolmentStatuses.Add(createdEnrolmentStatus);

                if (Status.ACCEPTED_TOS_CODE.Equals(status?.Code))
                {
                    //create the license plate for this enrollee
                    var enrollee = await _context.Enrollees
                        .SingleAsync(e => e.Id == enrolment.EnrolleeId);

                    enrollee.LicensePlate = this.GenerateLicensePlate();
                }

                var created = await _context.SaveChangesAsync();
                if (created < 1)
                {
                    throw new InvalidOperationException("Could not create enrolment status.");
                }

                return createdEnrolmentStatus;
            }

            throw new InvalidOperationException("Could not create enrolment status, status change is not allowed.");
        }

        private string GenerateLicensePlate()
        {
            return Base85.Ascii85.Encode(Guid.NewGuid().ToByteArray());
        }

        public bool IsStatusChangeAllowed(Status startingStatus, Status endingStatus)
        {
            ICollection<Status> availableStatuses = this.GetAvailableStatuses(startingStatus);
            return availableStatuses.Contains(endingStatus);
        }

        public async Task<bool> IsEnrolmentInStatusAsync(int enrolmentId, short statusCodeToCheck)
        {
            var enrolment = await _context.Enrolments
                .AsNoTracking()
                .Include(e => e.EnrolmentStatuses)
                .SingleOrDefaultAsync(e => e.Id == enrolmentId);
            if (enrolment == null)
            {
                return false;
            }

            var currentStatusCode = enrolment.CurrentStatus?.StatusCode;

            return statusCodeToCheck.Equals(currentStatusCode);
        }
    }
}
