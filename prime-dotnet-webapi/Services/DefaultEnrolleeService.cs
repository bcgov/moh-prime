using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SimpleBase;
using Prime.Models;

namespace Prime.Services
{
    public class DefaultEnrolleeService : BaseService, IEnrolleeService
    {
        private readonly IAutomaticAdjudicationService _automaticAdjudicationService;
        private readonly IEmailService _emailService;

        private class StatusWrapper
        {
            public Status Status { get; set; }
            public bool AdminOnly { get; set; }
        }

        private Dictionary<Status, StatusWrapper[]> _workflowStateMap;

        private static Status NULL_STATUS = new Status { Code = -1, Name = "No Status" };

        public DefaultEnrolleeService(
            ApiDbContext context, IHttpContextAccessor httpContext, IAutomaticAdjudicationService automaticAdjudicationService, IEmailService emailService)
            : base(context, httpContext)
        {
            _automaticAdjudicationService = automaticAdjudicationService;
            _emailService = emailService;
        }

        private Dictionary<Status, StatusWrapper[]> GetWorkFlowStateMap()
        {
            if (_workflowStateMap == null)
            {
                // Construct the workflow map
                // TODO need idea of new enrollee vs old enrollee baked into statuses
                // TODO IN_PROGRESS should be NEW and potentiall prefix a duplicate set of enrolment lifecycle statuses
                Status IN_PROGRESS = _context.Statuses.Single(s => s.Code == Status.IN_PROGRESS_CODE);
                Status SUBMITTED = _context.Statuses.Single(s => s.Code == Status.SUBMITTED_CODE);
                Status APPROVED = _context.Statuses.Single(s => s.Code == Status.APPROVED_CODE);
                Status DECLINED = _context.Statuses.Single(s => s.Code == Status.DECLINED_CODE);
                Status ACCEPTED_TOS = _context.Statuses.Single(s => s.Code == Status.ACCEPTED_TOS_CODE);
                Status DECLINED_TOS = _context.Statuses.Single(s => s.Code == Status.DECLINED_TOS_CODE);

                _workflowStateMap = new Dictionary<Status, StatusWrapper[]>();
                _workflowStateMap.Add(NULL_STATUS, new[] {
                    new StatusWrapper { Status = IN_PROGRESS, AdminOnly = false }
                });
                // TODO only for new enrollees
                _workflowStateMap.Add(IN_PROGRESS, new[] {
                    new StatusWrapper { Status = SUBMITTED, AdminOnly = false }
                });
                _workflowStateMap.Add(SUBMITTED, new[] {
                    // TODO not possible after first ACCEPTED_TOS instead use EDITING
                    new StatusWrapper { Status = IN_PROGRESS, AdminOnly = true },
                    new StatusWrapper { Status = APPROVED, AdminOnly = true },
                    new StatusWrapper { Status = DECLINED, AdminOnly = true }
                });
                _workflowStateMap.Add(APPROVED, new[] {
                    new StatusWrapper { Status = ACCEPTED_TOS, AdminOnly = false },
                    new StatusWrapper { Status = DECLINED_TOS, AdminOnly = false }
                });
                _workflowStateMap.Add(DECLINED, new[] {
                    // TODO not possible after first ACCEPTED_TOS instead use EDITING
                    new StatusWrapper { Status = IN_PROGRESS, AdminOnly = true }
                });
                _workflowStateMap.Add(ACCEPTED_TOS, new[] {
                    new StatusWrapper { Status = SUBMITTED, AdminOnly = false }
                });
                _workflowStateMap.Add(DECLINED_TOS, new[] {
                    // TODO not possible after first ACCEPTED_TOS instead use EDITING
                    new StatusWrapper { Status = IN_PROGRESS, AdminOnly = true }
                });
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

        public async Task<bool> EnrolleeExistsAsync(int enrolleeId)
        {
            return await _context.Enrollees
                .AnyAsync(e => e.Id == enrolleeId);
        }

        public async Task<bool> EnrolleeUserIdExistsAsync(Guid userId)
        {
            return await _context.Enrollees
                .AnyAsync(e => e.UserId == userId);
        }

        public async Task<Enrollee> GetEnrolleeAsync(Guid userId)
        {
            var entity = await this.GetBaseEnrolleeQuery()
                .SingleOrDefaultAsync(e => e.UserId == userId);

            if (entity != null)
            {
                // Add the available statuses to the enrollee
                entity.AvailableStatuses = this.GetAvailableStatuses(entity.CurrentStatus?.Status);
            }

            return entity;
        }


        public async Task<IEnumerable<Enrollee>> GetEnrolleesAsync(EnrolleeSearchOptions searchOptions = null)
        {
            IQueryable<Enrollee> query = this.GetBaseEnrolleeQuery();

            if (searchOptions?.StatusCode != null)
            {
                // TODO refactor see Jira PRIME-251
                query.Load();
                query = _context.Enrollees.Where(e => e.CurrentStatus.StatusCode == (short)searchOptions.StatusCode);
            }

            var items = await query.ToListAsync();

            foreach (var item in items)
            {
                // Add the available statuses to the enrolment
                item.AvailableStatuses = this.GetAvailableStatuses(item.CurrentStatus?.Status);
            }

            return items;
        }

        public async Task<Enrollee> GetEnrolleeForUserIdAsync(Guid userId)
        {
            Enrollee enrollee = await this.GetBaseEnrolleeQuery()
                .SingleOrDefaultAsync(e => e.UserId == userId);

            if (enrollee != null)
            {
                // Add the available statuses to the enrolment
                enrollee.AvailableStatuses = this.GetAvailableStatuses(enrollee?.CurrentStatus?.Status);
            }

            return enrollee;
        }

        public Task<int?> CreateEnrolleeAsync(Enrollee enrollee)
        {
            if (enrollee == null)
            {
                throw new ArgumentNullException(nameof(enrollee), "Could not create an enrollee, the passed in Enrollee cannot be null.");
            }

            return this.CreateEnrolleeInternalAsync(enrollee);
        }

        private async Task<int?> CreateEnrolleeInternalAsync(Enrollee enrollee)
        {
            // Create a status history record
            EnrolmentStatus enrolmentStatus = new EnrolmentStatus
            {
                Enrollee = enrollee,
                StatusCode = Status.IN_PROGRESS_CODE,
                StatusDate = DateTime.Now,
                PharmaNetStatus = false
            };

            if (enrollee.EnrolmentStatuses == null)
            {
                enrollee.EnrolmentStatuses = new List<EnrolmentStatus>(0);
            }

            enrollee.EnrolmentStatuses.Add(enrolmentStatus);
            _context.Enrollees.Add(enrollee);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create enrollee.");
            }

            return enrollee.Id;
        }

        public async Task<int> UpdateEnrolleeAsync(Enrollee enrollee, bool profileCompleted = false)
        {
            var _enrolleeDb = await _context.Enrollees
                                .Include(e => e.PhysicalAddress)
                                .Include(e => e.MailingAddress)
                                .Include(e => e.Certifications)
                                .Include(e => e.Jobs)
                                .Include(e => e.Organizations)
                                .AsNoTracking()
                                .Where(e => e.Id == enrollee.Id)
                                .SingleOrDefaultAsync();

            // Remove existing, and recreate if necessary
            this.ReplaceExistingAddress(_enrolleeDb.PhysicalAddress, enrollee.PhysicalAddress, enrollee);
            this.ReplaceExistingAddress(_enrolleeDb.MailingAddress, enrollee.MailingAddress, enrollee);
            this.ReplaceExistingItems(_enrolleeDb.Certifications, enrollee.Certifications, enrollee);
            this.ReplaceExistingItems(_enrolleeDb.Jobs, enrollee.Jobs, enrollee);
            this.ReplaceExistingItems(_enrolleeDb.Organizations, enrollee.Organizations, enrollee);

            // If profileCompleted is true, this is the first time the enrollee
            // has completed their profile by traversing the wizard, and indicates
            // a change in routing for the enrollee
            enrollee.ProfileCompleted = _enrolleeDb.ProfileCompleted || profileCompleted;

            _context.Entry(enrollee).State = EntityState.Modified;

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
            // Remove existing addresses
            if (dbAddress != null)
            {
                dbAddress.Enrollee = null;
                _context.Addresses.Remove(dbAddress);
            }

            // Create the new addresses, if they exist
            if (newAddress != null)
            {
                // Prevent the ID from being changed by the incoming changes
                newAddress.EnrolleeId = (int)enrollee.Id;
                _context.Entry(newAddress).State = EntityState.Added;
            }
        }

        private void ReplaceExistingItems<T>(ICollection<T> dbCollection, ICollection<T> newCollection, Enrollee enrollee) where T : class, IEnrolleeNavigationProperty
        {
            // Remove existing items
            foreach (var item in dbCollection)
            {
                item.Enrollee = null;
                _context.Remove(item);
            }

            // Create new items
            if (newCollection != null)
            {
                foreach (var item in newCollection)
                {
                    // Prevent the ID from being changed by the incoming changes
                    item.EnrolleeId = (int)enrollee.Id;
                    _context.Entry(item).State = EntityState.Added;
                }
            }
        }

        public async Task DeleteEnrolleeAsync(int enrolleeId)
        {
            var enrollee = await _context.Enrollees
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            if (enrollee == null)
            {
                return;
            }

            _context.Enrollees.Remove(enrollee);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Status>> GetAvailableEnrolmentStatusesAsync(int enrolleeId)
        {
            var enrollee = await _context.Enrollees
                .Include(e => e.EnrolmentStatuses)
                .ThenInclude(es => es.Status)
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            if (enrollee == null)
            {
                return null;
            }

            return this.GetAvailableStatuses(enrollee.CurrentStatus?.Status);
        }

        public async Task<IEnumerable<EnrolmentStatus>> GetEnrolmentStatusesAsync(int enrolleeId)
        {
            IQueryable<EnrolmentStatus> query = _context.EnrolmentStatuses
                .Include(es => es.Status)
                .Where(es => es.EnrolleeId == enrolleeId);

            var items = await query.ToListAsync();

            return items;
        }

        public Task<EnrolmentStatus> CreateEnrolmentStatusAsync(int enrolleeId, Status status)
        {
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status), "Could not create an enrolment status, the passed in Status cannot be null.");
            }

            return this.CreateEnrolmentStatusInternalAsync(enrolleeId, status);
        }

        private async Task<EnrolmentStatus> CreateEnrolmentStatusInternalAsync(int enrolleeId, Status status)
        {
            var enrollee = await this.GetBaseEnrolleeQuery()
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            if (enrollee == null)
            {
                return null;
            }

            var currentStatus = enrollee.CurrentStatus?.Status;

            // Make sure the status change is allowed
            if (IsStatusChangeAllowed(currentStatus ?? NULL_STATUS, status))
            {
                // Create a new enrolment status
                var createdEnrolmentStatus = new EnrolmentStatus
                {
                    EnrolleeId = enrolleeId,
                    StatusCode = status.Code,
                    StatusDate = DateTime.Now,
                    PharmaNetStatus = false
                };

                enrollee.EnrolmentStatuses.Add(createdEnrolmentStatus);

                switch (status?.Code)
                {
                    case Status.SUBMITTED_CODE:
                        // Check to see if this should be auto adjudicated
                        if (_automaticAdjudicationService.QualifiesForAutomaticAdjudication(enrollee))
                        {
                            // Change the status to adjudicated/approved
                            createdEnrolmentStatus.PharmaNetStatus = false;
                            // Create a new approved enrolment status
                            var adjudicatedEnrolmentStatus = new EnrolmentStatus
                            {
                                EnrolleeId = enrolleeId,
                                StatusCode = Status.APPROVED_CODE,
                                StatusDate = DateTime.Now,
                                PharmaNetStatus = false
                            };
                            adjudicatedEnrolmentStatus.EnrolmentStatusReasons = new List<EnrolmentStatusReason>
                            {
                                new EnrolmentStatusReason
                                {
                                    EnrolmentStatus = adjudicatedEnrolmentStatus,
                                    StatusReasonCode = StatusReason.AUTOMATIC_CODE
                                }
                            };
                            enrollee.EnrolmentStatuses.Add(adjudicatedEnrolmentStatus);
                            // Flip to the object that will get returned
                            createdEnrolmentStatus = adjudicatedEnrolmentStatus;
                        }
                        break;
                    case Status.APPROVED_CODE:
                        // Add the manual reason code
                        createdEnrolmentStatus.EnrolmentStatusReasons = new List<EnrolmentStatusReason>
                        {
                            new EnrolmentStatusReason
                            {
                                EnrolmentStatus = createdEnrolmentStatus,
                                StatusReasonCode = StatusReason.MANUAL_CODE
                            }
                        };
                        break;
                    case Status.DECLINED_CODE:
                        await SetAllPharmaNetStatusesFalseAsync(enrolleeId);
                        createdEnrolmentStatus.PharmaNetStatus = true;
                        break;
                    case Status.ACCEPTED_TOS_CODE:
                        await SetAllPharmaNetStatusesFalseAsync(enrolleeId);
                        enrollee.LicensePlate = this.GenerateLicensePlate();
                        createdEnrolmentStatus.PharmaNetStatus = true;
                        break;
                    case Status.DECLINED_TOS_CODE:
                        await SetAllPharmaNetStatusesFalseAsync(enrolleeId);
                        createdEnrolmentStatus.PharmaNetStatus = true;
                        break;
                }

                if (Status.ACCEPTED_TOS_CODE.Equals(status?.Code))
                {
                    enrollee.LicensePlate = this.GenerateLicensePlate();
                }

                var created = await _context.SaveChangesAsync();

                if (created < 1)
                {
                    throw new InvalidOperationException("Could not create enrolment status.");
                }

                // Enrollee just left manual adjudication, inform the enrollee
                if (currentStatus.Code == Status.SUBMITTED_CODE)
                {
                    _emailService.SendReminderEmail(enrollee);
                }

                return createdEnrolmentStatus;
            }

            throw new InvalidOperationException("Could not create enrolment status, status change is not allowed.");
        }

        private async Task SetAllPharmaNetStatusesFalseAsync(int enrolleeId)
        {
            var existingEnrolmentStatuses = await this.GetEnrolmentStatusesAsync(enrolleeId);

            foreach (var enrolmentStatus in existingEnrolmentStatuses)
            {
                enrolmentStatus.PharmaNetStatus = false;
            }
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

        public async Task<bool> IsEnrolleeInStatusAsync(int enrolleeId, short statusCodeToCheck)
        {
            var enrollee = await _context.Enrollees
                .AsNoTracking()
                .Include(e => e.EnrolmentStatuses)
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);
            if (enrollee == null)
            {
                return false;
            }

            var currentStatusCode = enrollee.CurrentStatus?.StatusCode;

            return statusCodeToCheck.Equals(currentStatusCode);
        }

        private IQueryable<Enrollee> GetBaseEnrolleeQuery()
        {
            return _context.Enrollees
                    .Include(e => e.PhysicalAddress)
                    .Include(e => e.MailingAddress)
                    .Include(e => e.Certifications)
                    .Include(e => e.Jobs)
                    .Include(e => e.Organizations)
                    .Include(e => e.EnrolmentStatuses).ThenInclude(es => es.Status)
                    .Include(e => e.EnrolmentStatuses).ThenInclude(es => es.EnrolmentStatusReasons).ThenInclude(esr => esr.StatusReason)
                    .Include(e => e.AccessAgreementNote)
                    .Include(e => e.EnrolmentCertificateNote);
        }

        public async Task<Enrollee> GetEnrolleeAsync(int enrolleeId)
        {
            var entity = await this.GetBaseEnrolleeQuery()
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            if (entity != null)
            {
                // add the available statuses to the enrollee
                entity.AvailableStatuses = this.GetAvailableStatuses(entity.CurrentStatus?.Status);
            }

            return entity;
        }

        public async Task<IEnumerable<AdjudicatorNote>> GetEnrolleeAdjudicatorNotesAsync(Enrollee enrollee)
        {
            return await _context.AdjudicatorNotes
                .Where(an => an.EnrolleeId == enrollee.Id)
                .OrderByDescending(an => an.NoteDate)
                .ToListAsync();
        }

        public async Task<AdjudicatorNote> CreateEnrolleeAdjudicatorNoteAsync(int enrolleeId, AdjudicatorNote adjudicatorNote)
        {
            AdjudicatorNote newAdjudicatorNote = new AdjudicatorNote
            {
                EnrolleeId = enrolleeId,
                Note = adjudicatorNote.Note,
                NoteDate = DateTime.Now
            };

            _context.AdjudicatorNotes.Add(newAdjudicatorNote);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create adjudicator note.");
            };

            return newAdjudicatorNote;
        }

        public async Task<IEnrolleeNote> UpdateEnrolleeNoteAsync(int enrolleeId, IEnrolleeNote newNote)
        {
            var enrollee = await _context.Enrollees
                .Include(e => e.AccessAgreementNote)
                .Include(e => e.EnrolmentCertificateNote)
                .Where(e => e.Id == enrolleeId)
                .SingleOrDefaultAsync();

            IEnrolleeNote dbNote = null;

            if (newNote.GetType() == typeof(AccessAgreementNote))
            {
                dbNote = enrollee.AccessAgreementNote;
            }
            else if (newNote.GetType() == typeof(EnrolmentCertificateNote))
            {
                dbNote = enrollee.EnrolmentCertificateNote;
            }
            else
            {
                throw new ArgumentException("Enrollee note type is not recognized, or not allowed.");
            }

            if (dbNote != null)
            {
                if (newNote.Note == null)
                {
                    _context.Remove(dbNote);
                }
                else
                {
                    dbNote.Note = newNote.Note;
                    dbNote.NoteDate = DateTime.Now;
                    _context.Update(dbNote);
                }
            }
            else if (newNote != null)
            {
                newNote.EnrolleeId = enrolleeId;
                _context.Add(newNote);
            }

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not update the enrollee note.");
            }

            return newNote;
        }
    }
}
