using AutoMapper;
using AutoMapper.QueryableExtensions;
using DelegateDecompiler.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

using Prime.Auth;
using Prime.HttpClients;
using Prime.HttpClients.DocumentManagerApiDefinitions;
using Prime.Models;
using Prime.Models.Api;
using Prime.Models.VerifiableCredentials;
using Prime.ViewModels;

namespace Prime.Services
{
    public class EnrolleeService : BaseService, IEnrolleeService
    {
        private readonly IBusinessEventService _businessEventService;
        private readonly IDocumentManagerClient _documentClient;
        private readonly IMapper _mapper;

        public EnrolleeService(
            ApiDbContext context,
            ILogger<EnrolleeService> logger,
            IBusinessEventService businessEventService,
            IDocumentManagerClient documentClient,
            IMapper mapper)
            : base(context, logger)
        {
            _businessEventService = businessEventService;
            _documentClient = documentClient;
            _mapper = mapper;
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
                            .ThenInclude(esr => esr.AdjudicatorNote)
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
                .If(searchOptions.StatusCode.HasValue && searchOptions.StatusCode != 42, q => q
                    .Where(e => e.CurrentStatus.StatusCode == searchOptions.StatusCode.Value)
                )
                // MacGyver paper enrollee Filter into status Filter. arbitrarily chose 42.
                // search-form.component.ts constructor() has other reference to this value.
                .If(searchOptions.StatusCode.HasValue && searchOptions.StatusCode == 42, q => q
                    .Where(e => e.GPID.StartsWith("NOBCSC"))
                )
                .ProjectTo<EnrolleeListViewModel>(_mapper.ConfigurationProvider, new { newestAgreementIds })
                .DecompileAsync() // Needed to allow selecting into computed properties like DisplayId and CurrentStatus
                .OrderBy(e => e.Id)
                .ToListAsync();
        }

        public async Task<EnrolleeNavigation> GetAdjacentEnrolleeIdAsync(int enrolleeId)
        {
            var nextId = await _context.Enrollees
                .Where(e => e.Id > enrolleeId)
                .OrderBy(e => e.Id)
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            var previousId = await _context.Enrollees
                .Where(e => e.Id < enrolleeId)
                .OrderByDescending(e => e.Id)
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            return new EnrolleeNavigation { NextId = nextId, PreviousId = previousId };
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
            enrollee.Addresses = new List<EnrolleeAddress>();

            UpdateAddress(enrollee, createModel.MailingAddress);
            UpdateAddress(enrollee, createModel.PhysicalAddress);
            UpdateAddress(enrollee, createModel.VerifiedAddress);

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
                .Include(e => e.Addresses)
                    .ThenInclude(ea => ea.Address)
                .Include(e => e.Certifications)
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
            }

            UpdateAddress(enrollee, updateModel.PhysicalAddress);
            UpdateAddress(enrollee, updateModel.MailingAddress);
            UpdateAddress(enrollee, updateModel.VerifiedAddress);
            ReplaceExistingItems(enrollee.Certifications, updateModel.Certifications, enrolleeId);
            ReplaceExistingItems(enrollee.EnrolleeCareSettings, updateModel.EnrolleeCareSettings, enrolleeId);
            ReplaceExistingItems(enrollee.SelfDeclarations, updateModel.SelfDeclarations, enrolleeId);
            ReplaceExistingItems(enrollee.EnrolleeHealthAuthorities, updateModel.EnrolleeHealthAuthorities, enrolleeId);

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

        private void UpdateAddress<T>(Enrollee dbEnrollee, T newAddress) where T : Address
        {
            var existingEnrolleeAddress = dbEnrollee.Addresses
                .SingleOrDefault(ea => ea.Address is T);

            if (existingEnrolleeAddress == null)
            {
                if (newAddress == null)
                {
                    // Noop
                    return;
                }
                else
                {
                    // New
                    newAddress.Id = 0;
                    dbEnrollee.Addresses.Add(new EnrolleeAddress
                    {
                        Enrollee = dbEnrollee,
                        Address = newAddress
                    });
                }
            }
            else
            {
                if (newAddress == null)
                {
                    // Remove
                    _context.Remove(existingEnrolleeAddress.Address);
                    _context.Remove(existingEnrolleeAddress);
                    return;
                }
                else
                {
                    // Update
                    newAddress.Id = existingEnrolleeAddress.AddressId;
                    _context.Entry(existingEnrolleeAddress.Address).CurrentValues.SetValues(newAddress);
                }
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
                        HealthAuthorityCode = site.HealthAuthorityCode,
                        PhysicalAddress = newAddress,
                        SiteName = site.SiteName,
                        PEC = site.PEC,
                        FacilityName = site.FacilityName,
                        JobTitle = site.JobTitle
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
                    var filename = await _documentClient.FinalizeUploadAsync(documentGuid, DestinationFolders.SelfDeclarations);
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
                .Include(e => e.Addresses)
                    .ThenInclude(ea => ea.Address)
                .Include(e => e.Certifications)
                    .ThenInclude(c => c.License)
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
                .Include(e => e.Agreements);
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
                .Include(an => an.EnrolleeNotification)
                    .ThenInclude(ee => ee.Admin)
                .ProjectTo<EnrolleeNoteViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(n => n.Id == noteId);
        }

        public async Task<IEnumerable<EnrolleeNoteViewModel>> GetNotificationsAsync(int enrolleeId, int adminId)
        {
            return await _context.EnrolleeNotes
                .Include(an => an.Adjudicator)
                .Include(an => an.EnrolleeNotification)
                    .ThenInclude(ee => ee.Admin)
                .Where(an => an.EnrolleeId == enrolleeId)
                .Where(an => an.EnrolleeNotification != null && an.EnrolleeNotification.AssigneeId == adminId)
                .ProjectTo<EnrolleeNoteViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
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

        public async Task<EnrolleeNotification> CreateEnrolleeNotificationAsync(int EnrolleeNoteId, int adminId, int assigneeId)
        {
            var notification = new EnrolleeNotification
            {
                EnrolleeNoteId = EnrolleeNoteId,
                AdminId = adminId,
                AssigneeId = assigneeId,
            };

            _context.EnrolleeNotifications.Add(notification);

            await _context.SaveChangesAsync();

            return notification;
        }

        public async Task RemoveEnrolleeNotificationAsync(int enrolleeNotificationId)
        {
            var notification = await _context.EnrolleeNotifications
                .SingleOrDefaultAsync(ee => ee.Id == enrolleeNotificationId);
            if (notification == null)
            {
                return;
            }
            _context.EnrolleeNotifications.Remove(notification);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveNotificationsAsync(int enrolleeId)
        {
            var notifications = await _context.EnrolleeNotifications
                .Where(en => en.EnrolleeNote.EnrolleeId == enrolleeId)
                .ToListAsync();

            _context.EnrolleeNotifications.RemoveRange(notifications);
            await _context.SaveChangesAsync();
        }

        public async Task<EnrolleeNotification> GetEnrolleeNotificationAsync(int enrolleeNotificationId)
        {
            return await _context.EnrolleeNotifications
                .SingleOrDefaultAsync(ee => ee.Id == enrolleeNotificationId);
        }

        public async Task<EnrolmentStatusReference> AddAdjudicatorNoteToReferenceIdAsync(int statusId, int noteId)
        {
            var reference = await _context.EnrolmentStatusReference.Where(esr => esr.EnrolmentStatusId == statusId).SingleAsync();

            reference.AdjudicatorNoteId = noteId;

            _context.EnrolmentStatusReference.Update(reference);

            await _context.SaveChangesAsync();

            return reference;
        }

        public async Task<IBaseEnrolleeNote> UpdateEnrolleeNoteAsync(int enrolleeId, int adminId, IBaseEnrolleeNote newNote)
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
                // Know instance of AccessAgreementNote
                ((AccessAgreementNote)newNote).AdjudicatorId = adminId;
                newNote.NoteDate = DateTimeOffset.Now;
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

        public async Task UpdateEnrolleeAdjudicator(int enrolleeId, int? adminId = null)
        {
            var enrollee = await _context.Enrollees
                .Where(e => e.Id == enrolleeId)
                .SingleAsync();

            enrollee.AdjudicatorId = adminId;
            await _context.SaveChangesAsync();
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
            var filename = await _documentClient.FinalizeUploadAsync(documentGuid, DestinationFolders.EnrolleeAdjudicationDocuments);
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

        public async Task<IEnumerable<int>> GetNotifiedEnrolleeIdsForAdminAsync(ClaimsPrincipal user)
        {
            return await _context.EnrolleeNotes
                .Where(en => en.EnrolleeNotification != null && en.EnrolleeNotification.Assignee.UserId == user.GetPrimeUserId())
                .Select(en => en.EnrolleeId)
                .ToListAsync();
        }

        public async Task<Credential> GetCredentialAsync(int enrolleeId)
        {
            return await _context.Credentials
                .OrderByDescending(c => c.Id)
                .FirstOrDefaultAsync(c => c.EnrolleeId == enrolleeId);
        }

        public async Task<IEnumerable<string>> GetEnrolleeEmails(BulkEmailType bulkEmailType)
        {
            Expression<Func<Enrollee, bool>> predicate = bulkEmailType switch
            {
                BulkEmailType.CommunityPractice => e => e.EnrolleeCareSettings.Any(cs => cs.CareSettingCode == (int)CareSettingType.CommunityPractice),
                BulkEmailType.CommunityPharmacy => e => e.EnrolleeCareSettings.Any(cs => cs.CareSettingCode == (int)CareSettingType.CommunityPharmacy),
                BulkEmailType.HealthAuthority => e => e.EnrolleeCareSettings.Any(cs => cs.CareSettingCode == (int)CareSettingType.HealthAuthority),
                BulkEmailType.RequiresTOA => e => e.CurrentStatus.StatusCode == (int)StatusType.RequiresToa,
                BulkEmailType.RuTOA => e => e.Agreements.OrderByDescending(a => a.AcceptedDate).FirstOrDefault().AgreementVersion.AgreementType == AgreementType.RegulatedUserTOA,
                BulkEmailType.OboTOA => e => e.Agreements.OrderByDescending(a => a.AcceptedDate).FirstOrDefault().AgreementVersion.AgreementType == AgreementType.OboTOA,
                BulkEmailType.PharmRuTOA => e => e.Agreements.OrderByDescending(a => a.AcceptedDate).FirstOrDefault().AgreementVersion.AgreementType == AgreementType.CommunityPharmacistTOA,
                BulkEmailType.PharmOboTOA => e => e.Agreements.OrderByDescending(a => a.AcceptedDate).FirstOrDefault().AgreementVersion.AgreementType == AgreementType.PharmacyOboTOA,
                _ => null,
            };

            return await _context.Enrollees
                .AsNoTracking()
                .Where(predicate)
                .Select(e => e.Email)
                .DecompileAsync()
                .ToListAsync();
        }

        public async Task<bool> IsPotentialPaperEnrolleeReturnee(DateTime dateOfBirth)
        {
            return await _context.Enrollees
                .AsNoTracking()
                .AnyAsync(
                    e => e.GPID.StartsWith("NOBCSC")
                    && e.DateOfBirth.Date == dateOfBirth.Date
                    && !_context.EnrolleeLinkedEnrolments.Any(link => link.PaperEnrolleeId == e.Id)
                );
        }

        public async Task<IEnumerable<Enrollee>> GetPotentialPaperEnrolleeReturnees(DateTime dateOfBirth)
        {
            return await _context.Enrollees
                .AsNoTracking()
                .Where(
                    e => e.GPID.StartsWith("NOBCSC")
                    && e.DateOfBirth.Date == dateOfBirth.Date
                    && !_context.EnrolleeLinkedEnrolments.Any(link => link.PaperEnrolleeId == e.Id)
                )
                .ToListAsync();
        }

        public async Task<bool> LinkEnrolmentToPaperEnrolment(int enrolmentId, int PaperEnrolmentId)
        {
            var enrollee = await _context.Enrollees
                .Where(
                    e => e.Id == enrolmentId
                    && _context.EnrolleeLinkedEnrolments.Any(link => link.EnrolleeId == e.Id)
                )
                .AnyAsync();

            var paperEnrollee = await _context.Enrollees
                .Where(
                    pe => pe.GPID.StartsWith("NOBCSC")
                    && pe.Id == PaperEnrolmentId
                    && _context.EnrolleeLinkedEnrolments.Any(link => link.PaperEnrolleeId == pe.Id)
                )
                .AnyAsync();

            if (enrollee || paperEnrollee)
            {
                return false;
            }

            var newLinkedEnrolment = new EnrolleeLinkedEnrolments
            {
                EnrolleeId = enrolmentId,
                PaperEnrolleeId = PaperEnrolmentId
            };

            _context.Add(newLinkedEnrolment);

            return true;
        }
    }
}
