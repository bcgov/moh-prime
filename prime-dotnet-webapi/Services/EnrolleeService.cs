using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DelegateDecompiler.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Prime.Auth;
using Prime.Models;
using Prime.ViewModels;
using Prime.Models.Api;
using Prime.HttpClients;

namespace Prime.Services
{
    public class EnrolleeService : BaseService, IEnrolleeService
    {
        private readonly IMapper _mapper;
        private readonly IBusinessEventService _businessEventService;
        private readonly IDocumentManagerClient _documentClient;

        public EnrolleeService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            IBusinessEventService businessEventService,
            IDocumentManagerClient documentClient)
            : base(context, httpContext)
        {
            _mapper = mapper;
            _businessEventService = businessEventService;
            _documentClient = documentClient;
        }

        public async Task<bool> EnrolleeExistsAsync(int enrolleeId)
        {
            return await _context.Enrollees
                .AsNoTracking()
                .AnyAsync(e => e.Id == enrolleeId);
        }

        public async Task<bool> UserIdExistsAsync(Guid userId)
        {
            return await _context.Enrollees
                .AsNoTracking()
                .AnyAsync(e => e.UserId == userId);
        }

        public async Task<bool> GpidExistsAsync(string gpid)
        {
            return await _context.Enrollees
                .AsNoTracking()
                .AnyAsync(e => e.GPID == gpid);
        }

        public async Task<PermissionsRecord> GetPermissionsRecordAsync(int enrolleeId)
        {
            return await _context.Enrollees
                .AsNoTracking()
                .Where(e => e.Id == enrolleeId)
                .Select(e => new PermissionsRecord { UserId = e.UserId })
                .SingleOrDefaultAsync();
        }

        public async Task<EnrolleeViewModel> GetEnrolleeAsync(int enrolleeId, bool isAdmin = false)
        {
            IQueryable<Enrollee> query = GetBaseEnrolleeQuery()
                .Include(e => e.Submissions);

            if (isAdmin)
            {
                // TODO create an enrollee admin view model
                query = query.Include(e => e.Adjudicator)
                    .Include(e => e.EnrolmentStatuses)
                        .ThenInclude(es => es.EnrolmentStatusReference)
                            .ThenInclude(esan => esan.AdjudicatorNote)
                    .Include(e => e.EnrolmentStatuses)
                        .ThenInclude(es => es.EnrolmentStatusReference)
                            .ThenInclude(esr => esr.Adjudicator);
            }

            var enrollee = await query
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);
            var newestAgreementIds = await _context.AgreementVersions
                .Select(a => a.AgreementType)
                .Distinct()
                .Select(type => _context.AgreementVersions
                    .OrderByDescending(a => a.EffectiveDate)
                    .First(a => a.AgreementType == type)
                    .Id
                )
                .ToListAsync();

            if (isAdmin)
            {
                var notes = await _context.EnrolleeNotes
                    .Where(en => en.EnrolleeId == enrolleeId)
                    .Include(en => en.EnrolmentEscalation)
                    .ToListAsync();

                enrollee.AdjudicatorNotes = notes;
            }

            return _mapper.Map<Enrollee, EnrolleeViewModel>(enrollee,
                opt => opt.AfterMap((src, dest) => dest.HasNewestAgreement = newestAgreementIds.Any(n => n == src.CurrentAgreementId)));
        }

        public async Task<IEnumerable<EnrolleeListViewModel>> GetEnrolleesAsync(EnrolleeSearchOptions searchOptions = null)
        {
            searchOptions ??= new EnrolleeSearchOptions();

            IQueryable<int> newestAgreementIds = _context.AgreementVersions
                .Select(a => a.AgreementType)
                .Distinct()
                .Select(type => _context.AgreementVersions
                    .OrderByDescending(a => a.EffectiveDate)
                    .First(a => a.AgreementType == type)
                    .Id
                );

            return await _context.Enrollees
                .AsNoTracking()
                .If(!string.IsNullOrWhiteSpace(searchOptions.TextSearch), q => q
                    .Search(e => e.FirstName,
                        e => e.LastName,
                        e => e.FullName,
                        e => e.Email,
                        e => e.Phone,
                        e => e.DisplayId.ToString())
                    .SearchCollections(e => e.Certifications.Select(c => c.LicenseNumber))
                    .Containing(searchOptions.TextSearch)
                )
                .If(searchOptions.StatusCode.HasValue, q => q
                    .Where(e => e.CurrentStatus.StatusCode == searchOptions.StatusCode.Value)
                )
                .ProjectTo<EnrolleeListViewModel>(_mapper.ConfigurationProvider, new { newestAgreementIds })
                .DecompileAsync() // Needed to allow selecting into computed properties like DisplayId and CurrentStatus
                .OrderBy(e => e.Id)
                .ToListAsync();
        }

        public async Task<Enrollee> GetEnrolleeForUserIdAsync(Guid userId, bool excludeDecline = false)
        {
            Enrollee enrollee = await GetBaseEnrolleeQuery()
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.UserId == userId);

            if (enrollee == null
                || (excludeDecline && enrollee.CurrentStatus.IsType(StatusType.Declined)))
            {
                return null;
            }

            return enrollee;
        }

        public async Task<int> CreateEnrolleeAsync(EnrolleeCreateModel createModel)
        {
            createModel.ThrowIfNull(nameof(createModel));

            var enrollee = _mapper.Map<Enrollee>(createModel);
            enrollee.AddEnrolmentStatus(StatusType.Editable);
            _context.Enrollees.Add(enrollee);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create enrollee.");
            }

            await _businessEventService.CreateEnrolleeEventAsync(enrollee.Id, "Enrollee Created");

            return enrollee.Id;
        }

        public async Task<int> UpdateEnrolleeAsync(int enrolleeId, EnrolleeUpdateModel updateModel, bool profileCompleted = false)
        {
            var enrollee = await _context.Enrollees
                .Include(e => e.PhysicalAddress)
                .Include(e => e.MailingAddress)
                .Include(e => e.Certifications)
                .Include(e => e.Jobs)
                .Include(e => e.EnrolleeRemoteUsers)
                .Include(e => e.RemoteAccessSites)
                .Include(e => e.RemoteAccessLocations)
                    .ThenInclude(ral => ral.PhysicalAddress)
                .Include(e => e.EnrolleeCareSettings)
                .Include(e => e.EnrolleeHealthAuthorities)
                .Include(e => e.SelfDeclarations)
                .Include(e => e.OboSites)
                    .ThenInclude(s => s.PhysicalAddress)
                .SingleAsync(e => e.Id == enrolleeId);

            _context.Entry(enrollee).CurrentValues.SetValues(updateModel);

            // TODO currently doesn't update the date of birth
            if (enrollee.IdentityProvider != AuthConstants.BCServicesCard)
            {
                enrollee.FirstName = updateModel.PreferredFirstName;
                enrollee.LastName = updateModel.PreferredLastName;
                enrollee.GivenNames = $"{updateModel.PreferredFirstName} {updateModel.PreferredMiddleName}";
                UpdatePhysicalAddress(enrollee, new PhysicalAddress
                {
                    CountryCode = updateModel.MailingAddress.CountryCode,
                    ProvinceCode = updateModel.MailingAddress.ProvinceCode,
                    Street = updateModel.MailingAddress.Street,
                    Street2 = updateModel.MailingAddress.Street2,
                    City = updateModel.MailingAddress.City,
                    Postal = updateModel.MailingAddress.Postal
                });
            }

            UpdateMailingAddress(enrollee, updateModel.MailingAddress);
            ReplaceExistingItems(enrollee.Certifications, updateModel.Certifications, enrolleeId);
            ReplaceExistingItems(enrollee.Jobs, updateModel.Jobs, enrolleeId);
            ReplaceExistingItems(enrollee.EnrolleeCareSettings, updateModel.EnrolleeCareSettings, enrolleeId);
            ReplaceExistingItems(enrollee.SelfDeclarations, updateModel.SelfDeclarations, enrolleeId);
            // Removed Temporarily
            // ReplaceExistingItems(enrollee.EnrolleeHealthAuthorities, updateModel.EnrolleeHealthAuthorities, enrolleeId);

            UpdateEnrolleeRemoteUsers(enrollee, updateModel);
            UpdateRemoteAccessSites(enrollee, updateModel);
            UpdateRemoteAccessLocations(enrollee, updateModel);

            UpdateOboSites(enrollee, updateModel);

            // If profileCompleted is true, this is the first time the enrollee
            // has completed their profile by traversing the wizard, and indicates
            // a change in routing for the enrollee
            if (profileCompleted)
            {
                enrollee.ProfileCompleted = true;
            }

            // This is the temporary way we are adding self declaration documents until this gets refactored.
            await CreateSelfDeclarationDocuments(enrolleeId, updateModel.SelfDeclarations);

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        private void UpdatePhysicalAddress(Enrollee dbEnrollee, PhysicalAddress newAddress)
        {
            if (dbEnrollee.PhysicalAddress != null && newAddress != null)
            {
                newAddress.Id = dbEnrollee.PhysicalAddress.Id;
                _context.Entry(dbEnrollee.PhysicalAddress).CurrentValues.SetValues(newAddress);
            }
            else if (newAddress != null)
            {
                dbEnrollee.PhysicalAddress = newAddress;
            }
        }

        private void UpdateMailingAddress(Enrollee dbEnrollee, MailingAddress newAddress)
        {
            if (dbEnrollee.MailingAddress != null)
            {
                _context.Addresses.Remove(dbEnrollee.MailingAddress);
            }

            if (newAddress != null)
            {
                var address = new MailingAddress
                {
                    CountryCode = newAddress.CountryCode,
                    ProvinceCode = newAddress.ProvinceCode,
                    Street = newAddress.Street,
                    Street2 = newAddress.Street2,
                    City = newAddress.City,
                    Postal = newAddress.Postal
                };

                dbEnrollee.MailingAddress = address;
            }
        }

        private void ReplaceExistingItems<T>(ICollection<T> dbCollection, ICollection<T> newCollection, int enrolleeId) where T : class, IEnrolleeNavigationProperty
        {
            // Remove existing items
            foreach (var item in dbCollection)
            {
                _context.Remove(item);
            }

            if (newCollection == null)
            {
                return;
            }

            // Create new items
            foreach (var item in newCollection)
            {
                // Prevent the ID from being changed by the incoming changes
                item.EnrolleeId = enrolleeId;
                _context.Entry(item).State = EntityState.Added;
            }
        }

        private void UpdateRemoteAccessLocations(Enrollee dbEnrollee, EnrolleeUpdateModel updateEnrollee)
        {
            // Wholesale replace the remote access locations
            foreach (var location in dbEnrollee.RemoteAccessLocations)
            {
                _context.Remove(location.PhysicalAddress);
                _context.Remove(location);
            }

            if (updateEnrollee.RemoteAccessLocations == null || !updateEnrollee.RemoteAccessLocations.Any())
            {
                return;
            }

            var remoteAccessLocations = new List<RemoteAccessLocation>();

            foreach (var location in updateEnrollee.RemoteAccessLocations)
            {
                var newAddress = new PhysicalAddress
                {
                    CountryCode = location.PhysicalAddress.CountryCode,
                    ProvinceCode = location.PhysicalAddress.ProvinceCode,
                    Street = location.PhysicalAddress.Street,
                    Street2 = location.PhysicalAddress.Street2,
                    City = location.PhysicalAddress.City,
                    Postal = location.PhysicalAddress.Postal
                };
                var newLocation = new RemoteAccessLocation
                {
                    Enrollee = dbEnrollee,
                    InternetProvider = location.InternetProvider,
                    PhysicalAddress = newAddress
                };
                _context.Entry(newAddress).State = EntityState.Added;
                _context.Entry(newLocation).State = EntityState.Added;
                remoteAccessLocations.Add(newLocation);
            }

            updateEnrollee.RemoteAccessLocations = remoteAccessLocations;
        }

        private void UpdateEnrolleeRemoteUsers(Enrollee dbEnrollee, EnrolleeUpdateModel updateEnrollee)
        {
            if (dbEnrollee.EnrolleeRemoteUsers != null)
            {
                foreach (var eru in dbEnrollee.EnrolleeRemoteUsers)
                {
                    _context.EnrolleeRemoteUsers.Remove(eru);
                }
            }

            if (updateEnrollee.EnrolleeRemoteUsers != null)
            {
                foreach (var eru in updateEnrollee.EnrolleeRemoteUsers)
                {
                    eru.EnrolleeId = dbEnrollee.Id;
                    _context.Entry(eru).State = EntityState.Added;
                }
            }
        }

        private void UpdateRemoteAccessSites(Enrollee dbEnrollee, EnrolleeUpdateModel updateEnrollee)
        {
            if (dbEnrollee.RemoteAccessSites != null)
            {
                foreach (var ras in dbEnrollee.RemoteAccessSites)
                {
                    _context.RemoteAccessSites.Remove(ras);
                }
            }

            if (updateEnrollee.RemoteAccessSites != null)
            {
                foreach (var ras in updateEnrollee.RemoteAccessSites)
                {
                    var remoteAccessSite = new RemoteAccessSite
                    {
                        EnrolleeId = dbEnrollee.Id,
                        SiteId = ras.SiteId
                    };

                    _context.Entry(remoteAccessSite).State = EntityState.Added;
                }
            }
        }

        private void UpdateOboSites(Enrollee dbEnrollee, EnrolleeUpdateModel updateEnrollee)
        {
            // Wholesale replace the obo sites
            foreach (var site in dbEnrollee.OboSites)
            {
                _context.Remove(site.PhysicalAddress);
                _context.Remove(site);
            }

            if (updateEnrollee?.OboSites != null && updateEnrollee?.OboSites.Count() != 0)
            {
                var oboSites = new List<OboSite>();

                foreach (var site in updateEnrollee.OboSites)
                {
                    var newAddress = new PhysicalAddress
                    {
                        CountryCode = site.PhysicalAddress.CountryCode,
                        ProvinceCode = site.PhysicalAddress.ProvinceCode,
                        Street = site.PhysicalAddress.Street,
                        Street2 = site.PhysicalAddress.Street2,
                        City = site.PhysicalAddress.City,
                        Postal = site.PhysicalAddress.Postal
                    };
                    var newSite = new OboSite
                    {
                        Enrollee = dbEnrollee,
                        CareSettingCode = site.CareSettingCode,
                        PhysicalAddress = newAddress,
                        SiteName = site.SiteName,
                        PEC = site.PEC,
                        FacilityName = site.FacilityName
                    };
                    _context.Entry(newAddress).State = EntityState.Added;
                    _context.Entry(newSite).State = EntityState.Added;
                    oboSites.Add(newSite);
                }
                updateEnrollee.OboSites = oboSites;
            }
        }

        private async Task CreateSelfDeclarationDocuments(int enrolleeId, ICollection<SelfDeclaration> newDeclarations)
        {
            if (newDeclarations == null)
            {
                return;
            }

            foreach (var declaration in newDeclarations.Where(d => d.DocumentGuids != null))
            {
                foreach (var documentGuid in declaration.DocumentGuids)
                {
                    var filename = await _documentClient.FinalizeUploadAsync(documentGuid, "self_declarations");
                    if (string.IsNullOrWhiteSpace(filename))
                    {
                        throw new InvalidOperationException($"Could not find a document upload with GUID {documentGuid}");
                    }

                    _context.SelfDeclarationDocuments.Add(new SelfDeclarationDocument
                    {
                        EnrolleeId = enrolleeId,
                        SelfDeclarationTypeCode = declaration.SelfDeclarationTypeCode,
                        DocumentGuid = documentGuid,
                        Filename = filename,
                        UploadedDate = DateTimeOffset.Now
                    });
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

        public async Task AssignToaAgreementType(int enrolleeId, AgreementType? agreementType)
        {
            var submission = await _context.Submissions
                .OrderByDescending(s => s.CreatedDate)
                .FirstOrDefaultAsync(e => e.EnrolleeId == enrolleeId);

            submission.AgreementType = agreementType;
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

            if (enrollee == null || enrollee.CurrentStatus == null)
            {
                return false;
            }

            return statusCodesToCheck.Contains(enrollee.CurrentStatus.GetStatusType());
        }

        private IQueryable<Enrollee> GetBaseEnrolleeQuery()
        {
            return _context.Enrollees
                .Include(e => e.PhysicalAddress)
                .Include(e => e.MailingAddress)
                .Include(e => e.Certifications)
                    .ThenInclude(c => c.License)
                .Include(e => e.Jobs)
                .Include(e => e.OboSites)
                    .ThenInclude(s => s.PhysicalAddress)
                .Include(e => e.EnrolleeCareSettings)
                .Include(e => e.EnrolleeHealthAuthorities)
                .Include(e => e.EnrolleeRemoteUsers)
                .Include(e => e.RemoteAccessSites)
                    .ThenInclude(ras => ras.Site)
                        .ThenInclude(ras => ras.PhysicalAddress)
                .Include(e => e.RemoteAccessSites)
                    .ThenInclude(ras => ras.Site)
                        .ThenInclude(ras => ras.SiteVendors)
                .Include(r => r.RemoteAccessLocations)
                    .ThenInclude(rul => rul.PhysicalAddress)
                .Include(e => e.EnrolmentStatuses)
                    .ThenInclude(es => es.Status)
                .Include(e => e.EnrolmentStatuses)
                    .ThenInclude(es => es.EnrolmentStatusReasons)
                        .ThenInclude(esr => esr.StatusReason)
                .Include(e => e.AccessAgreementNote)
                .Include(e => e.SelfDeclarations)
                .Include(e => e.SelfDeclarationDocuments)
                .Include(e => e.IdentificationDocuments)
                .Include(e => e.Agreements)
                .Include(e => e.EnrolleeCredentials)
                    .ThenInclude(ec => ec.Credential);
        }

        public async Task<Enrollee> GetEnrolleeNoTrackingAsync(int enrolleeId)
        {
            var entity = await GetBaseEnrolleeQuery()
                .Include(e => e.RemoteAccessSites)
                    .ThenInclude(ras => ras.Site)
                        .ThenInclude(site => site.PhysicalAddress)
                .Include(e => e.RemoteAccessSites)
                    .ThenInclude(ras => ras.Site)
                        .ThenInclude(site => site.SiteVendors)
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            return entity;
        }

        public async Task<IEnumerable<EnrolleeNoteViewModel>> GetEnrolleeAdjudicatorNotesAsync(int enrolleeId)
        {
            return await _context.EnrolleeNotes
                .Where(an => an.EnrolleeId == enrolleeId)
                .Include(an => an.Adjudicator)
                .OrderByDescending(an => an.NoteDate)
                .ProjectTo<EnrolleeNoteViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<EnrolleeNoteViewModel> GetEnrolleeAdjudicatorNoteAsync(int enrolleeId, int noteId)
        {
            return await _context.EnrolleeNotes
                .Where(an => an.EnrolleeId == enrolleeId)
                .Include(an => an.Adjudicator)
                .Include(an => an.EnrolmentEscalation)
                    .ThenInclude(ee => ee.Admin)
                .ProjectTo<EnrolleeNoteViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(n => n.Id == noteId);
        }

        public async Task<EnrolleeNote> CreateEnrolleeAdjudicatorNoteAsync(int enrolleeId, string note, int adminId)
        {
            var adjudicatorNote = new EnrolleeNote
            {
                EnrolleeId = enrolleeId,
                AdjudicatorId = adminId,
                Note = note,
                NoteDate = DateTimeOffset.Now
            };

            _context.EnrolleeNotes.Add(adjudicatorNote);

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

        public async Task<EnrolmentEscalation> CreateEnrolmentEscalationAsync(int EnrolleeNoteId, int adminId, int assigneeId)
        {
            var escalation = new EnrolmentEscalation
            {
                EnrolleeNoteId = EnrolleeNoteId,
                AdminId = adminId,
                AssigneeId = assigneeId,
            };

            _context.EnrolmentEscalations.Add(escalation);

            await _context.SaveChangesAsync();

            return escalation;
        }

        public async Task RemoveEnrolmentEscalationAsync(int enrolmentEscalationId)
        {
            var escalation = await _context.EnrolmentEscalations
                .SingleOrDefaultAsync(ee => ee.Id == enrolmentEscalationId);
            if (escalation == null)
            {
                return;
            }
            _context.EnrolmentEscalations.Remove(escalation);
            await _context.SaveChangesAsync();
        }

        public async Task<EnrolmentEscalation> GetEnrolmentEscalationAsync(int enrolmentEscalationId)
        {
            return await _context.EnrolmentEscalations
                .SingleOrDefaultAsync(ee => ee.Id == enrolmentEscalationId);
        }

        public async Task<EnrolmentStatusReference> AddAdjudicatorNoteToReferenceIdAsync(int statusId, int noteId)
        {
            var reference = await _context.EnrolmentStatusReference.Where(esr => esr.EnrolmentStatusId == statusId).SingleAsync();

            reference.AdjudicatorNoteId = noteId;

            _context.EnrolmentStatusReference.Update(reference);

            await _context.SaveChangesAsync();

            return reference;
        }

        public async Task<IBaseEnrolleeNote> UpdateEnrolleeNoteAsync(int enrolleeId, IBaseEnrolleeNote newNote)
        {
            var enrollee = await _context.Enrollees
                .Include(e => e.AccessAgreementNote)
                .Where(e => e.Id == enrolleeId)
                .SingleOrDefaultAsync();

            IBaseEnrolleeNote dbNote = null;

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

        public async Task<EnrolleeViewModel> UpdateEnrolleeAdjudicator(int enrolleeId, int? adminId = null)
        {
            var enrollee = await _context.Enrollees
                .Where(e => e.Id == enrolleeId)
                .SingleOrDefaultAsync();

            enrollee.AdjudicatorId = adminId;
            await _context.SaveChangesAsync();

            return _mapper.Map<EnrolleeViewModel>(enrollee);
        }

        public async Task<IEnumerable<BusinessEvent>> GetEnrolleeBusinessEventsAsync(int enrolleeId, IEnumerable<int> businessEventTypeCodes)
        {
            return await _context.BusinessEvents
                .Include(e => e.Admin)
                .Where(e => e.EnrolleeId == enrolleeId && businessEventTypeCodes.Any(c => c == e.BusinessEventTypeCode))
                .OrderByDescending(e => e.EventDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<HpdidLookup>> HpdidLookupAsync(IEnumerable<string> hpdids)
        {
            hpdids.ThrowIfNull(nameof(hpdids));

            hpdids = hpdids.Where(h => !string.IsNullOrWhiteSpace(h));

            return await _context.Enrollees
                .Where(e => hpdids.Contains(e.HPDID))
                .Where(e => e.CurrentStatus.StatusCode != (int)StatusType.Declined)
                .Select(e => new HpdidLookup
                {
                    Gpid = e.GPID,
                    Hpdid = e.HPDID,
                    RenewalDate = e.ExpiryDate
                })
                .DecompileAsync()
                .ToListAsync();
        }

        public async Task<GpidValidationResponse> ValidateProvisionerDataAsync(string gpid, GpidValidationParameters parameters)
        {
            var enrollee = await _context.Enrollees
                .Include(e => e.Certifications)
                    .ThenInclude(c => c.License)
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

        public async Task<IdentificationDocument> CreateIdentificationDocument(int enrolleeId, Guid documentGuid, string filename)
        {
            var identificationDocument = new IdentificationDocument
            {
                DocumentGuid = documentGuid,
                EnrolleeId = enrolleeId,
                Filename = filename,
                UploadedDate = DateTimeOffset.Now
            };
            _context.IdentificationDocuments.Add(identificationDocument);

            await _context.SaveChangesAsync();

            return identificationDocument;
        }

        public async Task<EnrolleeAdjudicationDocument> AddEnrolleeAdjudicationDocumentAsync(int enrolleeId, Guid documentGuid, int adminId)
        {
            var filename = await _documentClient.FinalizeUploadAsync(documentGuid, "enrollee_adjudication_document");
            if (string.IsNullOrWhiteSpace(filename))
            {
                return null;
            }

            var document = new EnrolleeAdjudicationDocument
            {
                DocumentGuid = documentGuid,
                EnrolleeId = enrolleeId,
                Filename = filename,
                UploadedDate = DateTimeOffset.Now,
                AdjudicatorId = adminId
            };
            _context.EnrolleeAdjudicationDocuments.Add(document);

            await _context.SaveChangesAsync();

            return document;
        }

        public async Task<IEnumerable<EnrolleeAdjudicationDocument>> GetEnrolleeAdjudicationDocumentsAsync(int enrolleeId)
        {
            return await _context.EnrolleeAdjudicationDocuments
               .Where(bl => bl.EnrolleeId == enrolleeId)
               .Include(bl => bl.Adjudicator)
                .OrderByDescending(bl => bl.UploadedDate)
               .ToListAsync();
        }

        public async Task<EnrolleeAdjudicationDocument> GetEnrolleeAdjudicationDocumentAsync(int documentId)
        {
            return await _context.EnrolleeAdjudicationDocuments
               .SingleOrDefaultAsync(d => d.Id == documentId);
        }

        public async Task DeleteEnrolleeAdjudicationDocumentAsync(int documentId)
        {
            var document = await _context.EnrolleeAdjudicationDocuments
                .SingleOrDefaultAsync(d => d.Id == documentId);
            if (document == null)
            {
                return;
            }
            _context.EnrolleeAdjudicationDocuments.Remove(document);
            await _context.SaveChangesAsync();
        }

        public async Task<EnrolmentStatus> GetEnrolleeCurrentStatusAsync(int enrolleeId)
        {
            var enrollee = await _context.Enrollees
                .Include(e => e.EnrolmentStatuses)
                        .ThenInclude(es => es.EnrolmentStatusReasons)
                            .ThenInclude(esr => esr.StatusReason)
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);
            if (enrollee != null)
            {
                return enrollee.CurrentStatus;
            }
            return null;
        }
    }
}
