using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public class SiteService : BaseService, ISiteService
    {
        private readonly IBusinessEventService _businessEventService;
        private readonly IPartyService _partyService;
        private readonly IOrganizationService _organizationService;

        public SiteService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IBusinessEventService businessEventService,
            IPartyService partyService,
            IOrganizationService organizationService)
            : base(context, httpContext)
        {
            _businessEventService = businessEventService;
            _partyService = partyService;
            _organizationService = organizationService;
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

            UpdateParties(currentSite, updatedSite);
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

        private void UpdateParties(Site current, SiteUpdateModel updated)
        {
            string[] partyTypes = new string[] {
                nameof(current.AdministratorPharmaNet),
                nameof(current.PrivacyOfficer),
                nameof(current.TechnicalSupport)
            };

            foreach (var partyType in partyTypes)
            {
                var partyIdName = $"{partyType}Id";
                Party currentParty = _context.Entry(current).Reference(partyType).CurrentValue as Party;
                Party updatedParty = typeof(SiteUpdateModel).GetProperty(partyType).GetValue(updated) as Party;

                if (updatedParty != null)
                {
                    if (updatedParty.UserId != Guid.Empty)
                    {
                        _context.Entry(current).Property(partyIdName).CurrentValue = updatedParty.Id;
                    }
                    else
                    {
                        if (currentParty == null)
                        {
                            _context.Entry(current).Reference(partyType).CurrentValue = updatedParty;
                            currentParty = _context.Entry(current).Reference(partyType).CurrentValue as Party;
                        }
                        else
                        {
                            this._context.Entry(currentParty).CurrentValues.SetValues(updatedParty);
                        }

                        _partyService.UpdatePartyPhysicalAddress(currentParty, updatedParty);
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
                foreach (var location in remoteUser.RemoteUserLocations)
                {
                    _context.Remove(location.PhysicalAddress);
                    _context.Remove(location);
                }

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
                    var remoteUserLocations = new List<RemoteUserLocation>();

                    foreach (var location in remoteUser.RemoteUserLocations)
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
                        var newLocation = new RemoteUserLocation
                        {
                            RemoteUser = remoteUser,
                            InternetProvider = location.InternetProvider,
                            PhysicalAddress = newAddress
                        };
                        _context.Entry(newAddress).State = EntityState.Added;
                        _context.Entry(newLocation).State = EntityState.Added;
                        remoteUserLocations.Add(newLocation);
                    }
                    remoteUser.RemoteUserLocations = remoteUserLocations;

                    var remoteUserCertifications = new List<RemoteUserCertification>();

                    foreach (var certification in remoteUser.RemoteUserCertifications)
                    {
                        var newCertification = new RemoteUserCertification
                        {
                            RemoteUser = remoteUser,
                            CollegeCode = certification.CollegeCode,
                            LicenseNumber = certification.LicenseNumber
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

                DeletePartyFromSite(site.AdministratorPharmaNet);
                DeletePartyFromSite(site.TechnicalSupport);
                DeletePartyFromSite(site.PrivacyOfficer);

                _context.Sites.Remove(site);

                await _businessEventService.CreateSiteEventAsync(siteId, (int)provisionerId, "Site Deleted");

                await _context.SaveChangesAsync();
            }
        }

        private void DeletePartyFromSite(Party party)
        {
            if (party != null)
            {
                if (party.PhysicalAddress != null)
                {
                    _context.Addresses.Remove(party.PhysicalAddress);
                }
                _context.Parties.Remove(party);
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

        public async Task<BusinessLicenceDocument> AddBusinessLicenceAsync(int siteId, Guid documentGuid, string filename)
        {
            var businessLicence = new BusinessLicenceDocument
            {
                DocumentGuid = documentGuid,
                SiteId = siteId,
                Filename = filename,
                UploadedDate = DateTimeOffset.Now
            };

            _context.BusinessLicenceDocuments.Add(businessLicence);

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not add business licence.");
            }

            return businessLicence;
        }

        public async Task<IEnumerable<BusinessLicenceDocument>> GetBusinessLicencesAsync(int siteId)
        {
            return await _context.BusinessLicenceDocuments
                .Where(bl => bl.SiteId == siteId)
                .ToListAsync();
        }

        public async Task<BusinessLicenceDocument> GetLatestBusinessLicenceAsync(int siteId)
        {
            return await _context.BusinessLicenceDocuments
                .Where(bl => bl.SiteId == siteId)
                .OrderByDescending(bl => bl.UploadedDate)
                .FirstOrDefaultAsync();
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
                    .ThenInclude(r => r.RemoteUserLocations)
                        .ThenInclude(rul => rul.PhysicalAddress)
                .Include(s => s.RemoteUsers)
                    .ThenInclude(r => r.RemoteUserCertifications)
                .Include(s => s.BusinessLicenceDocuments);
        }
    }
}
