using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;
using Prime.ViewModels;
using Prime.Models.Api;

namespace Prime.Services
{
    public class EnrolleeService : BaseService, IEnrolleeService
    {
        private readonly ISubmissionRulesService _automaticAdjudicationService;
        private readonly IEmailService _emailService;
        private readonly IEnrolleeProfileVersionService _enroleeProfileVersionService;
        private readonly IBusinessEventService _businessEventService;

        public EnrolleeService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            ISubmissionRulesService automaticAdjudicationService,
            IEmailService emailService,
            IEnrolleeProfileVersionService enroleeProfileVersionService,
            IBusinessEventService businessEventService)
            : base(context, httpContext)
        {
            _automaticAdjudicationService = automaticAdjudicationService;
            _emailService = emailService;
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

        public async Task<bool> EnrolleeGpidExistsAsync(string gpid)
        {
            return await _context.Enrollees
                .AnyAsync(e => e.GPID == gpid);
        }

        public async Task<Enrollee> GetEnrolleeAsync(Guid userId)
        {
            return await this.GetBaseEnrolleeQuery()
                .SingleOrDefaultAsync(e => e.UserId == userId);
        }

        public async Task<Enrollee> GetEnrolleeAsync(int enrolleeId, bool isAdmin = false)
        {
            IQueryable<Enrollee> query = this.GetBaseEnrolleeQuery();

            if (isAdmin)
            {
                query = query.Include(e => e.Adjudicator)
                    .Include(e => e.EnrolmentStatuses)
                        .ThenInclude(es => es.EnrolmentStatusReference)
                            .ThenInclude(esan => esan.AdjudicatorNote)
                    .Include(e => e.EnrolmentStatuses)
                        .ThenInclude(es => es.EnrolmentStatusReference)
                            .ThenInclude(esr => esr.Adjudicator);
            }

            var entity = await query
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            if (entity != null)
            {
                // TODO: This is an interm fix for making a different view model for enrollee based on isAdmin
                if (isAdmin)
                {
                    entity.isAdminView = true;
                }
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

            return items;
        }

        public async Task<Enrollee> GetEnrolleeForUserIdAsync(Guid userId, bool excludeDecline = false)
        {
            Enrollee enrollee = await this.GetBaseEnrolleeQuery()
                .SingleOrDefaultAsync(e => e.UserId == userId);

            if (enrollee == null
                || (excludeDecline && enrollee.CurrentStatus.IsType(StatusType.Declined)))
            {
                return null;
            }

            return enrollee;
        }

        public async Task<int> CreateEnrolleeAsync(Enrollee enrollee)
        {
            enrollee.ThrowIfNull(nameof(enrollee));

            enrollee.AddEnrolmentStatus(StatusType.Editable);
            _context.Enrollees.Add(enrollee);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create enrollee.");
            }

            await this._businessEventService.CreateEnrolleeEventAsync(enrollee.Id, "Enrollee Created");

            return enrollee.Id;
        }

        public async Task<int> UpdateEnrolleeAsync(int enrolleeId, EnrolleeUpdateModel enrolleeProfile, bool profileCompleted = false)
        {
            var enrollee = await _context.Enrollees
                .Include(e => e.MailingAddress)
                .Include(e => e.Certifications)
                .Include(e => e.Jobs)
                .Include(e => e.EnrolleeCareSettings)
                .Include(e => e.SelfDeclarations)
                .SingleAsync(e => e.Id == enrolleeId);

            _context.Entry(enrollee).CurrentValues.SetValues(enrolleeProfile);

            UpdateMailingAddress(enrollee, enrolleeProfile.MailingAddress);
            ReplaceExistingItems(enrollee.Certifications, enrolleeProfile.Certifications, enrolleeId);
            ReplaceExistingItems(enrollee.Jobs, enrolleeProfile.Jobs, enrolleeId);
            ReplaceExistingItems(enrollee.EnrolleeCareSettings, enrolleeProfile.EnrolleeCareSettings, enrolleeId);
            ReplaceExistingItems(enrollee.SelfDeclarations, enrolleeProfile.SelfDeclarations, enrolleeId);

            // If profileCompleted is true, this is the first time the enrollee
            // has completed their profile by traversing the wizard, and indicates
            // a change in routing for the enrollee
            if (profileCompleted)
            {
                enrollee.ProfileCompleted = true;
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

        private void UpdateMailingAddress(Enrollee dbEnrollee, MailingAddress newAddress)
        {
            if (dbEnrollee.MailingAddress != null)
            {
                _context.Addresses.Remove(dbEnrollee.MailingAddress);
            }

            dbEnrollee.MailingAddress = newAddress;
        }

        private void ReplaceExistingItems<T>(ICollection<T> dbCollection, ICollection<T> newCollection, int enrolleeId) where T : class, IEnrolleeNavigationProperty
        {
            // Remove existing items
            foreach (var item in dbCollection)
            {
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

        public async Task<bool> IsEnrolleeInStatusAsync(int enrolleeId, params StatusType[] statusCodesToCheck)
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
                .Include(e => e.EnrolleeCareSettings)
                .Include(e => e.EnrolmentStatuses)
                    .ThenInclude(es => es.Status)
                .Include(e => e.EnrolmentStatuses)
                    .ThenInclude(es => es.EnrolmentStatusReasons)
                        .ThenInclude(esr => esr.StatusReason)
                .Include(e => e.AccessAgreementNote)
                .Include(e => e.SelfDeclarations)
                .Include(e => e.SelfDeclarationDocuments)
                .Include(e => e.AssignedPrivileges)
                    .ThenInclude(ap => ap.Privilege)
                .Include(e => e.AccessTerms)
                .Include(e => e.Credential);
        }

        public async Task<Enrollee> GetEnrolleeNoTrackingAsync(int enrolleeId)
        {
            var entity = await this.GetBaseEnrolleeQuery()
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            return entity;
        }

        public async Task<IEnumerable<AdjudicatorNote>> GetEnrolleeAdjudicatorNotesAsync(Enrollee enrollee)
        {
            return await _context.AdjudicatorNotes
                .Where(an => an.EnrolleeId == enrollee.Id)
                .Include(an => an.Adjudicator)
                .OrderByDescending(an => an.NoteDate)
                .ToListAsync();
        }

        public async Task<AdjudicatorNote> CreateEnrolleeAdjudicatorNoteAsync(int enrolleeId, string note, int adminId)
        {
            var adjudicatorNote = new AdjudicatorNote
            {
                EnrolleeId = enrolleeId,
                AdjudicatorId = adminId,
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

        public async Task<EnrolmentStatusReference> CreateEnrolmentStatusReferenceAsync(int statusId, int adminId)
        {
            var reference = new EnrolmentStatusReference
            {
                EnrolmentStatusId = statusId,
                AdminId = adminId,
            };

            _context.EnrolmentStatusReference.Add(reference);

            await _context.SaveChangesAsync();

            return reference;
        }

        public async Task<EnrolmentStatusReference> AddAdjudicatorNoteToReferenceIdAsync(int statusId, int noteId)
        {
            var reference = await _context.EnrolmentStatusReference.Where(esr => esr.EnrolmentStatusId == statusId).SingleAsync();

            reference.AdjudicatorNoteId = noteId;

            _context.EnrolmentStatusReference.Update(reference);

            await _context.SaveChangesAsync();

            return reference;
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
                await _businessEventService.CreateNoteEventAsync(enrolleeId, "Updated Limits and Conditions Note: " + newNote.Note);
            }

            return newNote;
        }

        public async Task<int> GetEnrolleeCountAsync()
        {
            return await _context.Enrollees
                .CountAsync();
        }

        public async Task<Enrollee> UpdateEnrolleeAdjudicator(int enrolleeId, Admin admin = null)
        {
            var enrollee = await GetBaseEnrolleeQuery()
                .Include(e => e.Adjudicator)
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            enrollee.Adjudicator = admin;

            _context.Update(enrollee);

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not update the enrollee adjudicator.");
            }

            return enrollee;
        }

        public async Task<IEnumerable<BusinessEvent>> GetEnrolleeBusinessEvents(int enrolleeId)
        {
            return await _context.BusinessEvents
                .Include(e => e.Admin)
                .Where(e => e.EnrolleeId == enrolleeId)
                .OrderByDescending(e => e.EventDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<HpdidLookup>> HpdidLookupAsync(IEnumerable<string> hpdids)
        {
            hpdids.ThrowIfNull(nameof(hpdids));

            hpdids = hpdids.Where(h => !string.IsNullOrWhiteSpace(h));

            return await _context.Enrollees
                .Include(e => e.AccessTerms)
                .Where(e => hpdids.Contains(e.HPDID))
                .Where(e => !e.CurrentStatus.IsType(StatusType.Declined))
                .Select(e => HpdidLookup.FromEnrollee(e))
                .ToListAsync();
        }

        public async Task<GpidValidationResponse> ValidateProvisionerDataAsync(string gpid, GpidValidationParameters parameters)
        {
            var enrollee = await _context.Enrollees
                .Include(e => e.Certifications)
                    .ThenInclude(c => c.College)
                .SingleOrDefaultAsync(e => e.GPID == gpid);

            if (enrollee == null)
            {
                return null;
            }

            return parameters.ValidateAgainst(enrollee);
        }

        public async Task<SelfDeclarationDocument> AddSelfDeclarationDocumentAsync(int enrolleeId, SelfDeclarationDocument selfDeclarationDocument)
        {
            selfDeclarationDocument.EnrolleeId = enrolleeId;
            selfDeclarationDocument.UploadedDate = DateTimeOffset.Now;

            _context.SelfDeclarationDocuments.Add(selfDeclarationDocument);

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not add Self Declaration Documents.");
            }

            return selfDeclarationDocument;
        }

    }
}
