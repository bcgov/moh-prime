using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DelegateDecompiler.EntityFrameworkCore;
using Prime.HttpClients;
using Prime.HttpClients.DocumentManagerApiDefinitions;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public class CommunitySiteService : BaseService, ICommunitySiteService
    {
        private readonly IBusinessEventService _businessEventService;
        private readonly IDocumentManagerClient _documentClient;
        private readonly IMapper _mapper;

        public CommunitySiteService(
            ApiDbContext context,
            ILogger<CommunitySiteService> logger,
            IBusinessEventService businessEventService,
            IDocumentManagerClient documentClient,
            IMapper mapper)
            : base(context, logger)
        {
            _businessEventService = businessEventService;
            _documentClient = documentClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommunitySite>> GetSitesAsync(int? organizationId = null)
        {
            IQueryable<CommunitySite> query = GetBaseSiteQuery();

            if (organizationId != null)
            {
                query = query.Where(s => s.OrganizationId == organizationId);
            }

            return await query.ToListAsync();
        }

        public async Task<CommunitySite> GetSiteAsync(int siteId)
        {
            return await GetBaseSiteQuery()
                .SingleOrDefaultAsync(s => s.Id == siteId);
        }

        public async Task<int> CreateSiteAsync(int organizationId)
        {
            var signingAuthorityId = await _context.Organizations
                .Where(o => o.Id == organizationId)
                .Select(o => o.SigningAuthorityId)
                .SingleOrDefaultAsync();

            if (signingAuthorityId == default)
            {
                throw new ArgumentException("Could not create a site, the passed in Organization doesnt exist.", nameof(organizationId));
            }

            var site = new CommunitySite
            {
                ProvisionerId = signingAuthorityId,
                OrganizationId = organizationId,
            };
            site.AddStatus(SiteStatusType.Editable);

            _context.CommunitySites.Add(site);

            if (await _context.SaveChangesAsync() < 1)
            {
                _logger.LogError($"Could not create Community Site under Organization {organizationId}.");
                return InvalidId;
            }

            await _businessEventService.CreateSiteEventAsync(site.Id, signingAuthorityId, "Site Created");

            return site.Id;
        }

        public async Task UpdateSiteAsync(int siteId, CommunitySiteUpdateModel updatedSite)
        {
            var currentSite = await GetSiteAsync(siteId);

            _context.Entry(currentSite).CurrentValues.SetValues(updatedSite);

            if (currentSite.SubmittedDate == null)
            {
                UpdateVendors(currentSite, updatedSite);
            }

            UpdateAddress(currentSite, updatedSite);
            UpdateContacts(currentSite, updatedSite);
            UpdateBusinessHours(currentSite, updatedSite);
            UpdateRemoteUsers(currentSite, updatedSite.RemoteUsers);

            await _businessEventService.CreateSiteEventAsync(currentSite.Id, currentSite.Provisioner.Id, "Site Updated");

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"DbUpdateConcurrencyException when attempting to update Site {siteId}. Message: {ex.Message}");
            }
        }

        public async Task<PermissionsRecord> GetPermissionsRecordAsync(int siteId)
        {
            return await _context.CommunitySites
                .AsNoTracking()
                .Where(s => s.Id == siteId)
                .Select(s => new PermissionsRecord { UserId = s.Organization.SigningAuthority.UserId })
                .SingleOrDefaultAsync();
        }

        private void UpdateAddress(Site current, CommunitySiteUpdateModel updated)
        {
            if (updated?.PhysicalAddress != null)
            {
                if (current.PhysicalAddress == null)
                {
                    current.PhysicalAddress = updated.PhysicalAddress;
                }
                else
                {
                    _context.Entry(current.PhysicalAddress).CurrentValues.SetValues(updated.PhysicalAddress);
                }
            }
        }

        private void UpdateContacts(CommunitySite current, CommunitySiteUpdateModel updated)
        {
            var contactTypes = new[]
            {
                nameof(current.AdministratorPharmaNet),
                nameof(current.PrivacyOfficer),
                nameof(current.TechnicalSupport)
            };

            foreach (var contactType in contactTypes)
            {
                var currentContact = _context.Entry(current).Reference(contactType).CurrentValue as Contact;

                if (!(typeof(CommunitySiteUpdateModel).GetProperty(contactType)?.GetValue(updated) is Contact updatedContact))
                {
                    continue;
                }

                if (currentContact == null)
                {
                    _context.Entry(current).Reference(contactType).CurrentValue = updatedContact;
                }
                else
                {
                    _context.Entry(currentContact).CurrentValues.SetValues(updatedContact);

                    if (currentContact.PhysicalAddress != null && updatedContact.PhysicalAddress != null)
                    {
                        _context.Entry(currentContact.PhysicalAddress).CurrentValues.SetValues(updatedContact.PhysicalAddress);
                    }
                    else
                    {
                        currentContact.PhysicalAddress = updatedContact.PhysicalAddress;
                    }
                }
            }
        }

        private void UpdateBusinessHours(Site current, CommunitySiteUpdateModel updated)
        {
            if (updated?.BusinessHours != null)
            {
                if (current.BusinessHours != null)
                {
                    foreach (var businessHour in current.BusinessHours)
                    {
                        _context.Remove(businessHour);
                    }
                }

                foreach (var businessHour in updated.BusinessHours)
                {
                    businessHour.SiteId = current.Id;
                    _context.Entry(businessHour).State = EntityState.Added;
                }
            }
        }

        private void UpdateRemoteUsers(Site current, IEnumerable<RemoteUser> updateRemoteUsers)
        {
            if (updateRemoteUsers == null)
            {
                return;
            }

            // All RemoteUserCertifications will be dropped and re-added, so we must set all incoming PKs/FKs to 0
            // This can be removed when / if the updated Certs become a View Model without FKs.
            foreach (var cert in updateRemoteUsers.SelectMany(x => x.RemoteUserCertifications))
            {
                cert.Id = 0;
                cert.RemoteUserId = 0;
            }

            var existingUsers = current.RemoteUsers.ToDictionary(x => x.Id, x => x);

            foreach (var updatedUser in updateRemoteUsers)
            {
                if (existingUsers.TryGetValue(updatedUser.Id, out var existing))
                {
                    existingUsers.Remove(updatedUser.Id);

                    updatedUser.SiteId = current.Id;
                    _context.Entry(existing).CurrentValues.SetValues(updatedUser);

                    _context.RemoteUserCertifications.RemoveRange(existing.RemoteUserCertifications);
                    foreach (var cert in updatedUser.RemoteUserCertifications)
                    {
                        cert.RemoteUserId = updatedUser.Id;
                        _context.RemoteUserCertifications.Add(cert);
                    }
                }
                else
                {
                    updatedUser.Id = 0;
                    updatedUser.SiteId = current.Id;
                    _context.RemoteUsers.Add(updatedUser);
                }
            }

            _context.RemoteUsers.RemoveRange(existingUsers.Values);
        }

        private void UpdateVendors(CommunitySite current, CommunitySiteUpdateModel updated)
        {
            if (updated?.SiteVendors != null)
            {
                if (current.SiteVendors != null)
                {
                    foreach (var vendor in current.SiteVendors)
                    {
                        _context.Remove(vendor);
                    }
                }

                foreach (var vendor in updated.SiteVendors)
                {
                    var siteVendor = new SiteVendor
                    {
                        SiteId = current.Id,
                        VendorCode = vendor.VendorCode
                    };

                    _context.Entry(siteVendor).State = EntityState.Added;
                }
            }
        }

        public async Task<BusinessLicence> AddBusinessLicenceAsync(int siteId, BusinessLicence businessLicence, Guid documentGuid)
        {
            businessLicence.SiteId = siteId;
            businessLicence.UploadedDate = DateTimeOffset.Now;

            if (documentGuid != Guid.Empty)
            {
                businessLicence.BusinessLicenceDocument = await CreateBusinessLicenceDocument(documentGuid);
            }

            _context.BusinessLicences.Add(businessLicence);
            await _context.SaveChangesAsync();

            return businessLicence;
        }

        public async Task<BusinessLicence> UpdateBusinessLicenceAsync(int businessLicenceId, BusinessLicence updateBusinessLicence)
        {
            var businessLicence = await _context.BusinessLicences
                .Where(bl => bl.Id == businessLicenceId)
                .SingleOrDefaultAsync();

            businessLicence.DeferredLicenceReason = updateBusinessLicence.DeferredLicenceReason;
            businessLicence.ExpiryDate = updateBusinessLicence.ExpiryDate;

            _context.BusinessLicences.Update(businessLicence);
            await _context.SaveChangesAsync();

            return businessLicence;
        }

        public async Task<BusinessLicenceDocument> AddOrReplaceBusinessLicenceDocumentAsync(int businessLicenceId, Guid documentGuid)
        {
            var businessLicence = await _context.BusinessLicences
                .Include(bl => bl.BusinessLicenceDocument)
                .SingleOrDefaultAsync(bl => bl.Id == businessLicenceId);
            if (businessLicence.BusinessLicenceDocument != null)
            {
                _context.BusinessLicenceDocuments.Remove(businessLicence.BusinessLicenceDocument);
            }

            var filename = await _documentClient.FinalizeUploadAsync(documentGuid, DestinationFolders.BusinessLicences);
            if (string.IsNullOrWhiteSpace(filename))
            {
                return null;
            }

            var bld = new BusinessLicenceDocument
            {
                DocumentGuid = documentGuid,
                Filename = filename,
                UploadedDate = DateTimeOffset.Now,
                BusinessLicenceId = businessLicence.Id
            };

            _context.BusinessLicenceDocuments.Add(bld);
            await _context.SaveChangesAsync();

            return bld;
        }

        public async Task DeleteBusinessLicenceDocumentAsync(int businessLicenceId)
        {
            var businessLicence = await _context.BusinessLicences.Where(bl => bl.Id == businessLicenceId).SingleOrDefaultAsync();
            if (businessLicence.BusinessLicenceDocument != null)
            {
                _context.BusinessLicenceDocuments.Remove(businessLicence.BusinessLicenceDocument);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<BusinessLicenceDocument> CreateBusinessLicenceDocument(Guid documentGuid)
        {
            var filename = await _documentClient.FinalizeUploadAsync(documentGuid, DestinationFolders.BusinessLicences);
            if (string.IsNullOrWhiteSpace(filename))
            {
                return null;
            }

            return new BusinessLicenceDocument
            {
                DocumentGuid = documentGuid,
                Filename = filename,
                UploadedDate = DateTimeOffset.Now
            };
        }

        public async Task<IEnumerable<BusinessLicence>> GetBusinessLicencesAsync(int siteId)
        {
            return await _context.BusinessLicences
                .Where(bl => bl.SiteId == siteId)
                .ToListAsync();
        }

        public async Task<BusinessLicence> GetLatestBusinessLicenceAsync(int siteId)
        {
            return await _context.CommunitySites
                .Include(s => s.BusinessLicences)
                    .ThenInclude(bl => bl.BusinessLicenceDocument)
                .Where(s => s.Id == siteId)
                .Select(s => s.BusinessLicence)
                .DecompileAsync()
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<BusinessEvent>> GetSiteBusinessEventsAsync(int siteId, IEnumerable<int> businessEventTypeCodes)
        {
            return await _context.BusinessEvents
                .Include(e => e.Admin)
                .Where(e => businessEventTypeCodes.Any(c => c == e.BusinessEventTypeCode))
                .Where(e => e.SiteId == siteId
                        || e.Organization.Sites.Any(s => s.Id == siteId))
                .OrderByDescending(e => e.EventDate)
                .ToListAsync();
        }

        public async Task<SiteAdjudicationDocument> AddSiteAdjudicationDocumentAsync(int siteId, Guid documentGuid, int adminId)
        {
            var filename = await _documentClient.FinalizeUploadAsync(documentGuid, DestinationFolders.SiteAdjudicationDocuments);
            if (string.IsNullOrWhiteSpace(filename))
            {
                return null;
            }

            var document = new SiteAdjudicationDocument
            {
                DocumentGuid = documentGuid,
                SiteId = siteId,
                Filename = filename,
                UploadedDate = DateTimeOffset.Now,
                AdjudicatorId = adminId
            };
            _context.SiteAdjudicationDocuments.Add(document);

            await _context.SaveChangesAsync();

            return document;
        }

        public async Task<IEnumerable<SiteAdjudicationDocument>> GetSiteAdjudicationDocumentsAsync(int siteId)
        {
            return await _context.SiteAdjudicationDocuments
                .Where(bl => bl.SiteId == siteId)
                .Include(bl => bl.Adjudicator)
                .OrderByDescending(bl => bl.UploadedDate)
                .ToListAsync();
        }

        public async Task<SiteAdjudicationDocument> GetSiteAdjudicationDocumentAsync(int documentId)
        {
            return await _context.SiteAdjudicationDocuments
                .SingleOrDefaultAsync(d => d.Id == documentId);
        }

        public async Task DeleteSiteAdjudicationDocumentAsync(int documentId)
        {
            var document = await _context.SiteAdjudicationDocuments
                .SingleOrDefaultAsync(d => d.Id == documentId);
            if (document == null)
            {
                return;
            }
            _context.SiteAdjudicationDocuments.Remove(document);
            await _context.SaveChangesAsync();
        }

        public async Task<SiteNotification> CreateSiteNotificationAsync(int siteRegistrationNoteId, int adminId, int assineeId)
        {
            var notification = new SiteNotification
            {
                SiteRegistrationNoteId = siteRegistrationNoteId,
                AdminId = adminId,
                AssigneeId = assineeId
            };

            _context.SiteNotifications.Add(notification);

            await _context.SaveChangesAsync();

            return notification;
        }

        public async Task RemoveSiteNotificationAsync(int siteNotificationId)
        {
            var notification = await _context.SiteNotifications
                .SingleOrDefaultAsync(se => se.Id == siteNotificationId);
            if (notification == null)
            {
                return;
            }
            _context.SiteNotifications.Remove(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SiteRegistrationNoteViewModel>> GetNotificationsAsync(int siteId, int adminId)
        {
            return await _context.SiteRegistrationNotes
                .Include(n => n.Adjudicator)
                .Include(n => n.SiteNotification)
                    .ThenInclude(ee => ee.Admin)
                .Where(n => n.SiteId == siteId)
                .Where(n => n.SiteNotification != null && n.SiteNotification.AssigneeId == adminId)
                .ProjectTo<SiteRegistrationNoteViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task RemoveNotificationsAsync(int siteId)
        {
            var notifications = await _context.SiteNotifications
                .Where(en => en.SiteRegistrationNote.SiteId == siteId)
                .ToListAsync();

            _context.SiteNotifications.RemoveRange(notifications);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSiteFlag(int siteId, bool flagged)
        {
            var site = await _context.Sites
                .SingleAsync(s => s.Id == siteId);

            site.Flagged = flagged;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<int>> GetNotifiedSiteIdsForAdminAsync(ClaimsPrincipal user)
        {
            return await _context.SiteRegistrationNotes
                .Where(en => en.SiteNotification != null && en.SiteNotification.Assignee.UserId == user.GetPrimeUserId())
                .Select(en => en.SiteId)
                .ToListAsync();
        }

        public async Task<bool> SiteExists(int siteId)
        {
            return await _context.Sites
                .AsNoTracking()
                .AnyAsync(s => s.Id == siteId);
        }

        private IQueryable<CommunitySite> GetBaseSiteQuery()
        {
            return _context.CommunitySites
                .Include(s => s.Provisioner)
                .Include(s => s.SiteVendors)
                    .ThenInclude(v => v.Vendor)
                .Include(s => s.CareSetting)
                .Include(s => s.Organization)
                    .ThenInclude(o => o.SigningAuthority)
                        .ThenInclude(sa => sa.Addresses)
                            .ThenInclude(pa => pa.Address)
                .Include(s => s.PhysicalAddress)
                .Include(s => s.PrivacyOfficer)
                    .ThenInclude(p => p.PhysicalAddress)
                .Include(s => s.AdministratorPharmaNet)
                    .ThenInclude(p => p.PhysicalAddress)
                .Include(s => s.TechnicalSupport)
                    .ThenInclude(p => p.PhysicalAddress)
                .Include(s => s.BusinessHours)
                .Include(s => s.RemoteUsers)
                    .ThenInclude(r => r.RemoteUserCertifications)
                .Include(s => s.BusinessLicences)
                    .ThenInclude(bl => bl.BusinessLicenceDocument)
                .Include(s => s.Adjudicator)
                .Include(s => s.SiteStatuses);
        }
    }
}