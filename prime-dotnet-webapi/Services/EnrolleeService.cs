using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SimpleBase;
using Prime.Models;
using Prime.ViewModels;
using Prime.Models.Api;

namespace Prime.Services
{
    public class EnrolleeService : BaseService, IEnrolleeService
    {
        private readonly IAutomaticAdjudicationService _automaticAdjudicationService;
        private readonly IEmailService _emailService;
        private readonly IPrivilegeService _privilegeService;
        private readonly IAccessTermService _accessTermService;
        private readonly IEnrolleeProfileVersionService _enroleeProfileVersionService;
        private readonly IBusinessEventService _businessEventService;

        public EnrolleeService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IAutomaticAdjudicationService automaticAdjudicationService,
            IEmailService emailService,
            IPrivilegeService privilegeService,
            IAccessTermService accessTermService,
            IEnrolleeProfileVersionService enroleeProfileVersionService,
            IBusinessEventService businessEventService)
            : base(context, httpContext)
        {
            _automaticAdjudicationService = automaticAdjudicationService;
            _emailService = emailService;
            _privilegeService = privilegeService;
            _accessTermService = accessTermService;
            _enroleeProfileVersionService = enroleeProfileVersionService;
            _businessEventService = businessEventService;
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
            IQueryable<Enrollee> query = this.GetBaseEnrolleeQuery()
                .Include(e => e.Adjudicator);

            if (searchOptions != null && searchOptions.TextSearch != null)
            {
                query = query.Where(e =>
                    e.FirstName.ToLower().StartsWith(searchOptions.TextSearch.ToLower())
                    || e.LastName.ToLower().StartsWith(searchOptions.TextSearch.ToLower())
                    || e.ContactEmail.ToLower().StartsWith(searchOptions.TextSearch.ToLower())
                    || e.VoicePhone.ToLower().StartsWith(searchOptions.TextSearch.ToLower())
                    // Since DisplayId is a derived field we can not query on it. And we
                    // don't want to have to grab all Enrollees and filter on the front end.
                    || (e.Id + Enrollee.DISPLAY_OFFSET).ToString().Equals(searchOptions.TextSearch)
                    || e.FirstName.ToLower().StartsWith(searchOptions.TextSearch.ToLower())
                    || e.Certifications.Any(c => c.LicenseNumber.ToLower().StartsWith(searchOptions.TextSearch.ToLower()))
                );
            }

            IEnumerable<Enrollee> items = await query.ToListAsync();

            if (searchOptions?.StatusCode != null)
            {
                // TODO refactor see Jira PRIME-251
                items = items.Where(e => e.CurrentStatus.StatusCode == searchOptions.StatusCode);
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

        public async Task<int> CreateEnrolleeAsync(Enrollee enrollee)
        {
            if (enrollee == null)
            {
                throw new ArgumentNullException(nameof(enrollee), "Could not create an enrollee, the passed in Enrollee cannot be null.");
            }

            enrollee.AddEnrolmentStatus(EnrolmentStatusType.Active);
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

            // Remove existing, and recreate if necessary
            this.ReplaceExistingAddress(_enrolleeDb.MailingAddress, enrolleeProfile.MailingAddress, enrolleeProfile, enrolleeId);
            this.ReplaceExistingItems(_enrolleeDb.Certifications, enrolleeProfile.Certifications, enrolleeProfile, enrolleeId);
            this.ReplaceExistingItems(_enrolleeDb.Jobs, enrolleeProfile.Jobs, enrolleeProfile, enrolleeId);
            this.ReplaceExistingItems(_enrolleeDb.Organizations, enrolleeProfile.Organizations, enrolleeProfile, enrolleeId);

            var enrolleeTrack = await _context.Enrollees
                .Where(e => e.Id == enrolleeId)
                .SingleOrDefaultAsync();

            _context.Entry(enrolleeTrack).CurrentValues.SetValues(enrolleeProfile); // reflection

            // If profileCompleted is true, this is the first time the enrollee
            // has completed their profile by traversing the wizard, and indicates
            // a change in routing for the enrollee
            if (profileCompleted)
            {
                enrolleeTrack.ProfileCompleted = profileCompleted;
            }

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

        public async Task<IEnumerable<EnrolmentStatus>> GetEnrolmentStatusesAsync(int enrolleeId)
        {
            IQueryable<EnrolmentStatus> query = _context.EnrolmentStatuses
                .Include(es => es.Status)
                .Where(es => es.EnrolleeId == enrolleeId);

            var items = await query.ToListAsync();

            return items;
        }

        public async Task<bool> IsEnrolleeInStatusAsync(int enrolleeId, params EnrolmentStatusType[] statusCodesToCheck)
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

            return statusCodesToCheck.Cast<int>().Contains(currentStatusCode);
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
                    .ThenInclude(ap => ap.Privilege)
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
                NoteDate = DateTimeOffset.Now
            };

            _context.AdjudicatorNotes.Add(adjudicatorNote);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create adjudicator note.");
            }
            else
            {
                await _businessEventService.CreateNoteEventAsync(enrolleeId, "Added Adjudicator Note: " + note);
            }

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
                    dbNote.NoteDate = DateTimeOffset.Now;
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
            else
            {
                await _businessEventService.CreateNoteEventAsync(enrolleeId, "Updated Limits and Conditions Note: " + newNote);
            }

            return newNote;
        }

        public async Task<int> GetEnrolleeCountAsync()
        {
            return await _context.Enrollees
                   .CountAsync();
        }

        public async Task<Enrollee> UpdateEnrolleeAdjudicator(int enrolleeId, Guid adjudicatorUserId = default(Guid))
        {
            var enrollee = await GetBaseEnrolleeQuery()
                .Include(e => e.Adjudicator)
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            // Admin is set to null if no adjudicatorUserId is provided
            var admin = await _context.Admins
                .SingleOrDefaultAsync(a => a.UserId == adjudicatorUserId);

            enrollee.Adjudicator = admin;

            _context.Update(enrollee);

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not update the enrollee adjudicator.");
            }

            return enrollee;
        }

    }
}
