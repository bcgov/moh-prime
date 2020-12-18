using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Prime.Models;
using Prime.ViewModels;
using Prime.HttpClients;

namespace Prime.Services
{
    public class SiteService : BaseService, ISiteService
    {
        private readonly IMapper _mapper;
        private readonly IBusinessEventService _businessEventService;
        private readonly IPartyService _partyService;
        private readonly IOrganizationService _organizationService;
        private readonly IDocumentManagerClient _documentClient;

        public SiteService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            IBusinessEventService businessEventService,
            IPartyService partyService,
            IOrganizationService organizationService,
            IDocumentManagerClient documentClient)
            : base(context, httpContext)
        {
            _mapper = mapper;
            _businessEventService = businessEventService;
            _partyService = partyService;
            _organizationService = organizationService;
            _documentClient = documentClient;
        }

        public async Task<IEnumerable<Site>> GetSitesAsync(int? organizationId = null)
        {
            IQueryable<Site> query = this.GetBaseSiteQuery();

            if (organizationId != null)
            {
                query = query.Where(s => s.OrganizationId == organizationId);
            }

            return await query.ToListAsync();
        }

        public async Task<Site> GetSiteAsync(int siteId)
        {
            return await this.GetBaseSiteQuery()
                .SingleOrDefaultAsync(s => s.Id == siteId);
        }

        public async Task<int> CreateSiteAsync(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);

            if (organization == null)
            {
                throw new ArgumentException("Could not create a site, the passed in Organization doesnt exist.", nameof(organizationId));
            }

            // Site provisionerId should be equal to organization signingAuthorityId
            var site = new Site
            {
                ProvisionerId = organization.SigningAuthorityId,
                OrganizationId = organization.Id
            };

            _context.Sites.Add(site);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create Site.");
            }

            await _businessEventService.CreateSiteEventAsync(site.Id, organization.SigningAuthorityId, "Site Created");

            return site.Id;
        }

        public async Task<int> UpdateSiteAsync(int siteId, SiteUpdateModel updatedSite)
        {
            var currentSite = await this.GetSiteAsync(siteId);

            _context.Entry(currentSite).CurrentValues.SetValues(updatedSite);

            if (currentSite.SubmittedDate == null)
            {
                UpdateAddress(currentSite, updatedSite);
                UpdateVendors(currentSite, updatedSite);
            }

            UpdateContacts(currentSite, updatedSite);
            UpdateBusinessHours(currentSite, updatedSite);
            UpdateRemoteUsers(currentSite, updatedSite);

            await _businessEventService.CreateSiteEventAsync(currentSite.Id, currentSite.Provisioner.Id, "Site Updated");

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        private void UpdateAddress(Site current, SiteUpdateModel updated)
        {
            if (updated?.PhysicalAddress != null)
            {
                if (current.PhysicalAddress == null)
                {
                    current.PhysicalAddress = updated.PhysicalAddress;
                }
                else
                {
                    this._context.Entry(current.PhysicalAddress).CurrentValues.SetValues(updated.PhysicalAddress);
                }
            }
        }

        private void UpdateContacts(Site current, SiteUpdateModel updated)
        {
            string[] contactTypes = new string[] {
                nameof(current.AdministratorPharmaNet),
                nameof(current.PrivacyOfficer),
                nameof(current.TechnicalSupport)
            };

            foreach (var contactType in contactTypes)
            {
                var contactIdName = $"{contactType}Id";
                Contact currentContact = _context.Entry(current).Reference(contactType).CurrentValue as Contact;
                Contact updatedContact = typeof(SiteUpdateModel).GetProperty(contactType).GetValue(updated) as Contact;

                if (updatedContact != null)
                {
                    if (updatedContact.Id != 0)
                    {
                        _context.Entry(current).Property(contactIdName).CurrentValue = updatedContact.Id;
                    }
                    else
                    {
                        if (currentContact == null)
                        {
                            _context.Entry(current).Reference(contactType).CurrentValue = updatedContact;
                            currentContact = _context.Entry(current).Reference(contactType).CurrentValue as Contact;
                        }
                        else
                        {
                            this._context.Entry(currentContact).CurrentValues.SetValues(updatedContact);
                        }

                        if (updated.PhysicalAddress != null && current.PhysicalAddress != null)
                        {
                            this._context.Entry(current.PhysicalAddress).CurrentValues.SetValues(updated.PhysicalAddress);
                        }
                        else
                        {
                            current.PhysicalAddress = updated.PhysicalAddress;
                        }
                    }
                }
            }
        }

        private void UpdateBusinessHours(Site current, SiteUpdateModel updated)
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

        private void UpdateRemoteUsers(Site current, SiteUpdateModel updated)
        {
            // Wholesale replace the remote users
            foreach (var remoteUser in current.RemoteUsers)
            {
                foreach (var certification in remoteUser.RemoteUserCertifications)
                {
                    _context.Remove(certification);
                }
                _context.RemoteUsers.Remove(remoteUser);
            }

            if (updated?.RemoteUsers != null && updated?.RemoteUsers.Count() != 0)
            {
                foreach (var remoteUser in updated.RemoteUsers)
                {
                    remoteUser.SiteId = current.Id;
                    var remoteUserCertifications = new List<RemoteUserCertification>();

                    foreach (var certification in remoteUser.RemoteUserCertifications)
                    {
                        var newCertification = new RemoteUserCertification
                        {
                            RemoteUser = remoteUser,
                            CollegeCode = certification.CollegeCode,
                            LicenseNumber = certification.LicenseNumber,
                            LicenseCode = certification.LicenseCode
                        };
                        _context.Entry(newCertification).State = EntityState.Added;
                        remoteUserCertifications.Add(newCertification);
                    }
                    remoteUser.RemoteUserCertifications = remoteUserCertifications;

                    _context.Entry(remoteUser).State = EntityState.Added;
                }
            }
        }

        private void UpdateVendors(Site current, SiteUpdateModel updated)
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

        public async Task<int> UpdateCompletedAsync(int siteId)
        {
            var site = await this.GetBaseSiteQuery()
                .SingleOrDefaultAsync(s => s.Id == siteId);

            site.Completed = true;

            this._context.Update(site);

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not update the site.");
            }

            return updated;
        }

        public async Task<Site> UpdateSiteAdjudicator(int siteId, int? adminId = null)
        {
            var site = await _context.Sites.Where(s => s.Id == siteId).SingleOrDefaultAsync();
            site.AdjudicatorId = adminId;
            await _context.SaveChangesAsync();

            return site;
        }

        public async Task<Site> UpdatePecCode(int siteId, string pecCode)
        {
            var site = await this.GetBaseSiteQuery()
                .SingleOrDefaultAsync(s => s.Id == siteId);

            site.PEC = pecCode;

            this._context.Update(site);

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not update the site.");
            }

            await _businessEventService.CreateSiteEventAsync(site.Id, site.Organization.SigningAuthorityId, "Site ID (PEC Code) associated with site");

            return site;
        }

        public async Task DeleteSiteAsync(int siteId)
        {
            var site = await this.GetBaseSiteQuery()
                .SingleOrDefaultAsync(s => s.Id == siteId);

            if (site != null)
            {
                var provisionerId = site.ProvisionerId;

                if (site.PhysicalAddress != null)
                {
                    _context.Addresses.Remove(site.PhysicalAddress);
                }

                DeleteContactFromSite(site.AdministratorPharmaNet);
                DeleteContactFromSite(site.TechnicalSupport);
                DeleteContactFromSite(site.PrivacyOfficer);

                _context.Sites.Remove(site);

                await _businessEventService.CreateSiteEventAsync(siteId, (int)provisionerId, "Site Deleted");

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Site> ApproveSite(int siteId)
        {
            var site = await _context.Sites.SingleOrDefaultAsync(s => s.Id == siteId);

            if (site.Status != SiteStatusType.Approved)
            {
                site.Status = SiteStatusType.Approved;
                site.ApprovedDate = DateTimeOffset.Now;
                await _context.SaveChangesAsync();
            }

            await _businessEventService.CreateSiteEventAsync(site.Id, site.Organization.SigningAuthorityId, "Site Approved");

            return site;
        }

        public async Task<Site> DeclineSite(int siteId)
        {
            var site = await _context.Sites.SingleOrDefaultAsync(s => s.Id == siteId);
            site.Status = SiteStatusType.Declined;
            site.ApprovedDate = null;
            await _context.SaveChangesAsync();

            await _businessEventService.CreateSiteEventAsync(site.Id, site.Organization.SigningAuthorityId, "Site Declined");

            return site;
        }

        private void DeleteContactFromSite(Contact contact)
        {
            if (contact != null)
            {
                if (contact.PhysicalAddress != null)
                {
                    _context.Addresses.Remove(contact.PhysicalAddress);
                }
                _context.Contacts.Remove(contact);
            }
        }

        public async Task<Site> SubmitRegistrationAsync(int siteId)
        {
            var site = await GetSiteAsync(siteId);
            site.SubmittedDate = DateTimeOffset.Now;
            _context.Update(site);

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not submit the site.");
            }

            await _businessEventService.CreateSiteEventAsync(site.Id, site.Organization.SigningAuthorityId, "Site Submitted");

            return site;
        }

        public async Task<Site> GetSiteNoTrackingAsync(int siteId)
        {
            return await this.GetBaseSiteQuery()
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.Id == siteId);
        }

        public async Task<IEnumerable<BusinessEvent>> GetSiteBusinessEvents(int siteId)
        {
            return await _context.BusinessEvents
                .Where(e => e.SiteId == siteId)
                .OrderByDescending(e => e.EventDate)
                .ToListAsync();
        }

        public async Task<Vendor> GetVendorAsync(int vendorCode)
        {
            return await _context.Vendors
                .SingleOrDefaultAsync(v => v.Code == vendorCode);
        }

        public async Task<BusinessLicence> AddBusinessLicenceAsync(int siteId, BusinessLicence businessLicence, Guid documentGuid)
        {
            businessLicence.SiteId = siteId;

            if (documentGuid != Guid.Empty)
            {
                businessLicence.BusinessLicenceDocument = await CreateBusinessLicenceDocument(documentGuid);
            }

            _context.BusinessLicences.Add(businessLicence);
            await _context.SaveChangesAsync();

            return businessLicence;
        }

        public async Task<BusinessLicence> UpdateBusinessLicenceAsync(int siteId, BusinessLicence updateBusinessLicence)
        {
            var businessLicence = await _context.BusinessLicences.Where(bl => bl.SiteId == siteId).SingleOrDefaultAsync();

            businessLicence.DeferredLicenceReason = updateBusinessLicence.DeferredLicenceReason;

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

            var filename = await _documentClient.FinalizeUploadAsync(documentGuid, "business_licences");
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

        public async Task DeleteBusinessLicenceDocumentAsync(int siteId)
        {
            var businessLicence = await _context.BusinessLicences.Where(bl => bl.SiteId == siteId).SingleOrDefaultAsync();
            if (businessLicence.BusinessLicenceDocument != null)
            {
                _context.BusinessLicenceDocuments.Remove(businessLicence.BusinessLicenceDocument);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<BusinessLicenceDocument> CreateBusinessLicenceDocument(Guid documentGuid)
        {
            var filename = await _documentClient.FinalizeUploadAsync(documentGuid, "business_licences");
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

        public async Task<BusinessLicence> GetBusinessLicenceAsync(int siteId)
        {
            return await _context.BusinessLicences
                .Where(bl => bl.SiteId == siteId)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Site>> GetSitesByRemoteUserInfoAsync(IEnumerable<Certification> enrolleeCerts)
        {
            var sites = await this.GetBaseSiteQuery()
                .Where(s => s.ApprovedDate != null)
                .ToListAsync();

            sites = sites.FindAll(s => s.RemoteUsers.Any(ru => ru.RemoteUserCertifications.Any(ruc => enrolleeCerts.Any(c => c.FullLicenseNumber == ruc.FullLicenseNumber))));
            foreach (var site in sites)
            {
                site.RemoteUsers = site.RemoteUsers.Where(ru => ru.RemoteUserCertifications.Any(ruc => enrolleeCerts.Any(c => c.FullLicenseNumber == ruc.FullLicenseNumber))).ToList();
            }
            return sites;
        }

        public async Task<SiteRegistrationNote> CreateSiteRegistrationNoteAsync(int siteId, string note, int adminId)
        {
            var SiteRegistrationNote = new SiteRegistrationNote
            {
                SiteId = siteId,
                AdjudicatorId = adminId,
                Note = note,
                NoteDate = DateTimeOffset.Now
            };

            _context.SiteRegistrationNotes.Add(SiteRegistrationNote);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create site registration note.");
            }

            return SiteRegistrationNote;
        }

        public async Task<IEnumerable<SiteRegistrationNoteViewModel>> GetSiteRegistrationNotesAsync(Site site)
        {
            return await _context.SiteRegistrationNotes
                .Where(srn => srn.SiteId == site.Id)
                .Include(srn => srn.Adjudicator)
                .OrderByDescending(srn => srn.NoteDate)
                .ProjectTo<SiteRegistrationNoteViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessEvent>> GetSiteBusinessEventsAsync(int siteId, IEnumerable<int> businessEventTypeCodes)
        {
            return await _context.BusinessEvents
                .Include(e => e.Admin)
                .Where(e => e.SiteId == siteId && businessEventTypeCodes.Any(c => c == e.BusinessEventTypeCode))
                .OrderByDescending(e => e.EventDate)
                .ToListAsync();
        }

        public async Task<SiteAdjudicationDocument> AddSiteAdjudicationDocumentAsync(int siteId, Guid documentGuid, int adminId)
        {
            var filename = await _documentClient.FinalizeUploadAsync(documentGuid, "site_adjudication_document");
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

        private IQueryable<Site> GetBaseSiteQuery()
        {
            return _context.Sites
                .Include(s => s.Provisioner)
                .Include(s => s.SiteVendors)
                    .ThenInclude(v => v.Vendor)
                .Include(s => s.CareSetting)
                .Include(s => s.Organization)
                    .ThenInclude(o => o.SigningAuthority)
                        .ThenInclude(p => p.PhysicalAddress)
                .Include(s => s.Organization)
                    .ThenInclude(o => o.SigningAuthority)
                        .ThenInclude(p => p.MailingAddress)
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
                .Include(s => s.BusinessLicence)
                    .ThenInclude(bl => bl.BusinessLicenceDocument)
                .Include(s => s.Adjudicator);
        }
    }
}
