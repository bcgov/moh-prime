using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SimpleBase;
using Prime.Models;
using Prime.ViewModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Prime.Models.Api;
using System.Reflection;

namespace Prime.Services
{
    public class EnrolleeService : BaseService, IEnrolleeService
    {
        private readonly IAutomaticAdjudicationService _automaticAdjudicationService;
        private readonly IEmailService _emailService;
        private readonly IPrivilegeService _privilegeService;
        private readonly IAccessTermService _accessTermService;
        private readonly IEnrolleeProfileVersionService _enroleeProfileVersionService;

        private class StatusWrapper
        {
            public Status Status { get; set; }
            public bool AdminOnly { get; set; }
        }

        private Dictionary<Status, StatusWrapper[]> _workflowStateMap;

        private static Status NULL_STATUS = new Status { Code = -1, Name = "No Status" };

        public EnrolleeService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IAutomaticAdjudicationService automaticAdjudicationService,
            IEmailService emailService,
            IPrivilegeService privilegeService,
            IAccessTermService accessTermService,
            IEnrolleeProfileVersionService enroleeProfileVersionService)
            : base(context, httpContext)
        {
            _automaticAdjudicationService = automaticAdjudicationService;
            _emailService = emailService;
            _privilegeService = privilegeService;
            _accessTermService = accessTermService;
            _enroleeProfileVersionService = enroleeProfileVersionService;
        }

        private Dictionary<Status, StatusWrapper[]> GetWorkFlowStateMap()
        {
            if (_workflowStateMap == null)
            {
                // Construct the workflow map
                // TODO Should be async
                Status ACTIVE = _context.Statuses.Single(s => s.Code == Status.ACTIVE_CODE);
                Status UNDER_REVIEW = _context.Statuses.Single(s => s.Code == Status.UNDER_REVIEW_CODE);
                Status REQUIRES_TOA = _context.Statuses.Single(s => s.Code == Status.REQUIRES_TOA_CODE);
                Status LOCKED = _context.Statuses.Single(s => s.Code == Status.LOCKED_CODE);

                _workflowStateMap = new Dictionary<Status, StatusWrapper[]>();
                _workflowStateMap.Add(NULL_STATUS, new[] {
                    new StatusWrapper { Status = ACTIVE, AdminOnly = false }
                });
                _workflowStateMap.Add(ACTIVE, new[] {
                    new StatusWrapper { Status = UNDER_REVIEW, AdminOnly = false },
                    new StatusWrapper { Status = LOCKED, AdminOnly = true }
                });
                _workflowStateMap.Add(UNDER_REVIEW, new[] {
                    new StatusWrapper { Status = ACTIVE, AdminOnly = true },
                    new StatusWrapper { Status = REQUIRES_TOA, AdminOnly = true },
                    new StatusWrapper { Status = LOCKED, AdminOnly = true }
                });
                _workflowStateMap.Add(REQUIRES_TOA, new[] {
                    new StatusWrapper { Status = ACTIVE, AdminOnly = false },
                    new StatusWrapper { Status = LOCKED, AdminOnly = true }
                });
                _workflowStateMap.Add(LOCKED, new[] {
                    new StatusWrapper { Status = ACTIVE, AdminOnly = true }
                });
            }

            return _workflowStateMap;
        }

        private ICollection<Status> GetAvailableStatuses(Status currentStatus)
        {
            var userIsAdmin = _httpContext?.HttpContext?.User?.IsInRole(PrimeConstants.PRIME_ADMIN_ROLE) ?? false;
            var stateMap = this.GetWorkFlowStateMap()[currentStatus ?? NULL_STATUS];

            return stateMap
                .Where(m => userIsAdmin || !m.AdminOnly)
                .Select(m => m.Status)
                .ToList();
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
                entity.Privileges = await _privilegeService.GetPrivilegesForEnrolleeAsync(entity);
            }

            return entity;
        }


        public async Task<IEnumerable<Enrollee>> GetEnrolleesAsync(EnrolleeSearchOptions searchOptions = null)
        {
            IEnumerable<Enrollee> items = await this.GetBaseEnrolleeQuery()
                                                    .ToListAsync();

            if (searchOptions?.StatusCode != null)
            {
                // TODO refactor see Jira PRIME-251
                items = items.Where(e => e.CurrentStatus.StatusCode == (short)searchOptions.StatusCode);
            }

            foreach (var item in items)
            {
                item.Privileges = await _privilegeService.GetPrivilegesForEnrolleeAsync(item);
            }

            return items;
        }

        public async Task<Enrollee> GetEnrolleeForUserIdAsync(Guid userId)
        {
            Enrollee enrollee = await this.GetBaseEnrolleeQuery()
                .SingleOrDefaultAsync(e => e.UserId == userId);

            if (enrollee != null)
            {
                enrollee.Privileges = await _privilegeService.GetPrivilegesForEnrolleeAsync(enrollee);
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
                StatusCode = Status.ACTIVE_CODE,
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

        public async Task<int> UpdateEnrolleeAsync(int enrolleeId, EnrolleeProfileViewModel enrolleeProfile, bool profileCompleted = false)
        {
            var _enrolleeDb = await _context.Enrollees
                .Include(e => e.MailingAddress)
                .Include(e => e.Certifications)
                .Include(e => e.Jobs)
                .Include(e => e.Organizations)
                .AsNoTracking()
                .Where(e => e.Id == enrolleeId)
                .SingleOrDefaultAsync();

            Enrollee enrollee = new Enrollee { Id = enrolleeId };   // stub model, only has Id
            _context.Enrollees.Attach(enrollee); // track your stub model
            _context.Entry(enrollee).CurrentValues.SetValues(enrolleeProfile); // reflection

            // Remove existing, and recreate if necessary
            this.ReplaceExistingAddress(_enrolleeDb.MailingAddress, enrolleeProfile.MailingAddress, enrolleeProfile, enrolleeId);
            this.ReplaceExistingItems(_enrolleeDb.Certifications, enrolleeProfile.Certifications, enrolleeProfile, enrolleeId);
            this.ReplaceExistingItems(_enrolleeDb.Jobs, enrolleeProfile.Jobs, enrolleeProfile, enrolleeId);
            this.ReplaceExistingItems(_enrolleeDb.Organizations, enrolleeProfile.Organizations, enrolleeProfile, enrolleeId);

            // If profileCompleted is true, this is the first time the enrollee
            // has completed their profile by traversing the wizard, and indicates
            // a change in routing for the enrollee
            enrollee.ProfileCompleted = _enrolleeDb.ProfileCompleted || profileCompleted;

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        private void ReplaceExistingAddress(Address dbAddress, Address newAddress, EnrolleeProfileViewModel enrollee, int enrolleeId)
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
                newAddress.EnrolleeId = enrolleeId;
                _context.Entry(newAddress).State = EntityState.Added;
            }
        }

        private void ReplaceExistingItems<T>(ICollection<T> dbCollection, ICollection<T> newCollection, EnrolleeProfileViewModel enrollee, int enrolleeId) where T : class, IEnrolleeNavigationProperty
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
                    item.EnrolleeId = enrolleeId;
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

        private async Task<EnrolmentStatus> CreateEnrolmentStatusInternalAsync(int enrolleeId, Status newStatus)
        {
            var enrollee = await this.GetBaseEnrolleeQuery()
                .Include(e => e.Certifications)
                    .ThenInclude(cer => cer.College) // Needed for PharmaNet College auto-adjudication
                .Include(e => e.Certifications)
                    .ThenInclude(l => l.License) // Needed for LicenceClass Rule
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            if (enrollee == null)
            {
                return null;
            }

            var oldStatus = enrollee.CurrentStatus?.Status;

            if (!IsStatusChangeAllowed(oldStatus ?? NULL_STATUS, newStatus))
            {
                throw new InvalidOperationException("Could not create enrolment status, status change is not allowed.");
            }

            var createdEnrolmentStatus = new EnrolmentStatus
            {
                EnrolleeId = enrolleeId,
                StatusCode = newStatus.Code,
                StatusDate = DateTime.Now,
                PharmaNetStatus = false
            };
            enrollee.EnrolmentStatuses.Add(createdEnrolmentStatus);

            switch (newStatus.Code)
            {
                case Status.UNDER_REVIEW_CODE:
                    // Store a copy of the submitted enrollee profile
                    await _enroleeProfileVersionService.CreateEnrolleeProfileVersionAsync(enrollee);

                    if (await _automaticAdjudicationService.QualifiesForAutomaticAdjudication(enrollee))
                    {
                        // Change the status to adjudicated/approved
                        var adjudicatedEnrolmentStatus = new EnrolmentStatus
                        {
                            EnrolleeId = enrolleeId,
                            StatusCode = Status.REQUIRES_TOA_CODE,
                            StatusDate = DateTime.Now,
                            PharmaNetStatus = false
                        };
                        adjudicatedEnrolmentStatus.AddStatusReason(StatusReason.AUTOMATIC_CODE);

                        enrollee.EnrolmentStatuses.Add(adjudicatedEnrolmentStatus);

                        await _accessTermService.CreateEnrolleeAccessTermAsync(enrollee);

                        // Flip to the object that will get returned
                        createdEnrolmentStatus = adjudicatedEnrolmentStatus;
                    }
                    break;

                case Status.REQUIRES_TOA_CODE:
                    // Approved through manual processing
                    createdEnrolmentStatus.AddStatusReason(StatusReason.MANUAL_CODE);

                    await _accessTermService.CreateEnrolleeAccessTermAsync(enrollee);

                    break;

                case Status.LOCKED_CODE:
                    await SetAllPharmaNetStatusesFalseAsync(enrolleeId);
                    createdEnrolmentStatus.PharmaNetStatus = true;
                    break;

                case Status.ACTIVE_CODE:
                    // Sent back to edit profile from Under Review
                    if (oldStatus.Code == Status.UNDER_REVIEW_CODE)
                    {
                        break;
                    }
                    // Accepted Terms of Access
                    if (oldStatus.Code == Status.REQUIRES_TOA_CODE)
                    {
                        await SetAllPharmaNetStatusesFalseAsync(enrolleeId);
                        SetGPID(enrollee);
                        createdEnrolmentStatus.PharmaNetStatus = true;
                        await _accessTermService.AcceptCurrentAccessTermAsync(enrollee);
                        await _privilegeService.AssignPrivilegesToEnrolleeAsync(enrolleeId, enrollee);
                        break;
                    }
                    break;

            }

            await _context.SaveChangesAsync();

            // Enrollee just left manual adjudication, inform the enrollee
            if (oldStatus?.Code == Status.UNDER_REVIEW_CODE)
            {
                await _emailService.SendReminderEmailAsync(enrollee);
            }

            return createdEnrolmentStatus;
        }

        private async Task SetAllPharmaNetStatusesFalseAsync(int enrolleeId)
        {
            var existingEnrolmentStatuses = await this.GetEnrolmentStatusesAsync(enrolleeId);

            foreach (var enrolmentStatus in existingEnrolmentStatuses)
            {
                enrolmentStatus.PharmaNetStatus = false;
            }
        }

        private void SetGPID(Enrollee enrollee)
        {
            if (string.IsNullOrWhiteSpace(enrollee.GPID))
            {
                enrollee.GPID = Base85.Ascii85.Encode(Guid.NewGuid().ToByteArray());
            }
        }

        public bool IsStatusChangeAllowed(Status startingStatus, Status endingStatus)
        {
            ICollection<Status> availableStatuses = this.GetAvailableStatuses(startingStatus);
            return availableStatuses.Contains(endingStatus);
        }

        public async Task<bool> IsEnrolleeInStatusAsync(int enrolleeId, params short[] statusCodesToCheck)
        {
            var enrollee = await _context.Enrollees
                .AsNoTracking()
                .Include(e => e.EnrolmentStatuses)
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            if (enrollee == null)
            {
                return false;
            }

            var currentStatusCode = enrollee.CurrentStatus?.StatusCode ?? -1;

            return statusCodesToCheck.Contains(currentStatusCode);
        }

        private IQueryable<Enrollee> GetBaseEnrolleeQuery()
        {
            return _context.Enrollees
                    .Include(e => e.PhysicalAddress)
                    .Include(e => e.MailingAddress)
                    .Include(e => e.Certifications)
                        .ThenInclude(c => c.License)
                            .ThenInclude(l => l.DefaultPrivileges)
                    .Include(e => e.Jobs)
                    .Include(e => e.Organizations)
                    .Include(e => e.EnrolmentStatuses)
                        .ThenInclude(es => es.Status)
                    .Include(e => e.EnrolmentStatuses)
                        .ThenInclude(es => es.EnrolmentStatusReasons)
                        .ThenInclude(esr => esr.StatusReason)
                    .Include(e => e.AccessAgreementNote)
                    .Include(e => e.AssignedPrivileges)
                        .ThenInclude(AssignedPrivilege => AssignedPrivilege.Privilege)
                    .Include(e => e.AccessTerms);
        }

        public async Task<Enrollee> GetEnrolleeAsync(int enrolleeId)
        {
            var entity = await this.GetBaseEnrolleeQuery()
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            if (entity != null)
            {
                entity.Privileges = await _privilegeService.GetPrivilegesForEnrolleeAsync(entity);
            }

            return entity;
        }

        public async Task<Enrollee> GetEnrolleeNoTrackingAsync(int enrolleeId)
        {
            var entity = await this.GetBaseEnrolleeQuery()
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            if (entity != null)
            {
                entity.Privileges = await _privilegeService.GetPrivilegesForEnrolleeAsync(entity);
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

        public async Task<AdjudicatorNote> CreateEnrolleeAdjudicatorNoteAsync(int enrolleeId, string note)
        {
            var adjudicatorNote = new AdjudicatorNote
            {
                EnrolleeId = enrolleeId,
                Note = note,
                NoteDate = DateTime.Now
            };

            _context.AdjudicatorNotes.Add(adjudicatorNote);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create adjudicator note.");
            };

            return adjudicatorNote;
        }

        public async Task<IEnrolleeNote> UpdateEnrolleeNoteAsync(int enrolleeId, IEnrolleeNote newNote)
        {
            var enrollee = await _context.Enrollees
                .Include(e => e.AccessAgreementNote)
                .Where(e => e.Id == enrolleeId)
                .SingleOrDefaultAsync();

            IEnrolleeNote dbNote = null;

            if (newNote.GetType() == typeof(AccessAgreementNote))
            {
                dbNote = enrollee.AccessAgreementNote;
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


        public async Task<Enrollee> UpdateEnrolleeAlwaysManualAsync(int enrolleeId, bool alwaysManual)
        {
            var enrollee = await _context.Enrollees
                .Where(e => e.Id == enrolleeId)
                .SingleOrDefaultAsync();

            enrollee.AlwaysManual = alwaysManual;
            await _context.SaveChangesAsync();

            return enrollee;
        }

        public async Task<int> GetEnrolleeCountAsync()
        {
            return await _context.Enrollees
                   .CountAsync();

        }
    }
}
