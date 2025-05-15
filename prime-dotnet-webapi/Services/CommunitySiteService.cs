using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelegateDecompiler.EntityFrameworkCore;
using Prime.HttpClients;
using Prime.HttpClients.DocumentManagerApiDefinitions;
using Prime.Models;
using Prime.ViewModels;
using AutoMapper.QueryableExtensions;
using Prime.Models.Api;
using System.Text;

namespace Prime.Services
{
    public class CommunitySiteService : BaseService, ICommunitySiteService
    {
        private readonly IBusinessEventService _businessEventService;
        private readonly IEmailService _emailService;
        private readonly IDocumentManagerClient _documentClient;
        private readonly IMapper _mapper;

        public CommunitySiteService(
            ApiDbContext context,
            ILogger<CommunitySiteService> logger,
            IBusinessEventService businessEventService,
            IEmailService emailService,
            IDocumentManagerClient documentClient,
            IMapper mapper)
            : base(context, logger)
        {
            _businessEventService = businessEventService;
            _emailService = emailService;
            _documentClient = documentClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommunitySite>> GetSitesAsync(int? organizationId = null)
        {
            IQueryable<CommunitySite> query = GetBaseSiteQuery();

            if (organizationId != null)
            {
                query = query.Where(s => s.OrganizationId == organizationId && s.Organization.DeletedDate == null);
            }

            IEnumerable<CommunitySite> sites = await query.ToListAsync();
            // For SQL performance reasons, retrieve these 1-to-many related entities separately from base query
            foreach (var site in sites)
            {
                site.RemoteUsers = GetRemoteUsersOfSite(site.Id);
                site.SiteStatuses = GetStatusesOfSite(site.Id);
            }
            return sites;
        }

        public async Task<PaginatedList<CommunitySiteAdminListViewModel>> GetSitesAsync(OrganizationSearchOptions searchOptions)
        {
            searchOptions ??= new OrganizationSearchOptions();

            var query = _context.CommunitySites
                .AsNoTracking()
                .Where(s => s.DeletedDate == null && s.Organization.DeletedDate == null)
                .If(searchOptions.OrganizationId.HasValue, q => q
                    .Where(s => s.OrganizationId == searchOptions.OrganizationId))
                .If(searchOptions.CareSettingCode.HasValue, q => q
                    .Where(s => s.CareSettingCode == searchOptions.CareSettingCode)
                )
                .If(!string.IsNullOrWhiteSpace(searchOptions.TextSearch), q => q
                    .Search(
                        s => s.DoingBusinessAs,
                        s => s.PEC,
                        s => s.Organization.Name,
                        s => s.Organization.DisplayId.ToString(),
                        s => s.Organization.SigningAuthority.FirstName + " " + s.Organization.SigningAuthority.LastName)
                    .Containing(searchOptions.TextSearch)
                )
                .If(searchOptions.Status.HasValue, q => q
                    .Where(s => (int)s.SiteStatuses.OrderByDescending(ss => ss.StatusDate)
                        .FirstOrDefault().StatusType == searchOptions.Status))
                .ProjectTo<CommunitySiteAdminListViewModel>(_mapper.ConfigurationProvider)
                .OrderBy(s => s.DisplayId).ThenByDescending(s => s.SubmittedDate.HasValue).ThenBy(s => s.SubmittedDate)
                .DecompileAsync();

            var paginatedList = await PaginatedList<CommunitySiteAdminListViewModel>.CreateAsync(query, searchOptions.Page ?? 1);


            foreach (var site in paginatedList)
            {
                //check for duplicate site id
                site.DuplicatePecSiteCount = await GetDuplicatePecCount(site.CareSettingCode, site.PEC, site.Id);
                // Related to another Site?
                site.IsLinked = await IsLinkedSite(site.Id);
            }

            GroupSitesToOrgVisually(paginatedList);
            return paginatedList;
        }

        /// <summary>
        /// Visually group sites to their organizations by reducing the redundant information which is obscuring
        /// </summary>
        private static void GroupSitesToOrgVisually(PaginatedList<CommunitySiteAdminListViewModel> paginatedList)
        {
            int currentOrgId = int.MinValue;
            foreach (var orgSiteViewModel in paginatedList)
            {
                if (currentOrgId != int.MinValue)
                {
                    if (orgSiteViewModel.OrganizationId == currentOrgId)
                    {
                        // Remove redundant clutter
                        orgSiteViewModel.OrganizationName = String.Empty;
                        orgSiteViewModel.SigningAuthorityName = String.Empty;
                    }
                    else
                    {
                        currentOrgId = orgSiteViewModel.OrganizationId;
                    }
                }
                else
                {
                    currentOrgId = orgSiteViewModel.OrganizationId;
                }
            }
        }

        private async Task<int> GetDuplicatePecCount(int? careSettingCode, string pec, int originalSiteId)
        {
            return await _context.Sites
                    .Where(s => s.PEC != null && s.PEC == pec && s.CareSettingCode == careSettingCode && originalSiteId != s.Id)
                    .CountAsync();
        }

        private async Task<bool> IsLinkedSite(int siteId)
        {
            var intStream = _context.Database.SqlQueryRaw<int>(
                "SELECT count(*) AS \"Value\" FROM \"PredecessorSiteToSuccessorSite\" pstss WHERE pstss.\"PredecessorSiteId\" = {0} OR pstss.\"SuccessorSiteId\" = {0}",
                siteId);
            // grab the first (and only) row
            var count = await intStream.FirstAsync();
            return count > 0;
        }

        public async Task<CommunitySite> GetSiteAsync(int siteId)
        {
            var site = await GetBaseSiteQuery()
                .SingleOrDefaultAsync(s => s.Id == siteId);
            // For SQL performance reasons, retrieve these 1-to-many related entities separately from base query
            site.RemoteUsers = GetRemoteUsersOfSite(site.Id);
            site.SiteStatuses = GetStatusesOfSite(site.Id);

            if (site.CareSettingCode.HasValue &&
                site.CareSettingCode.Value == (int)CareSettingType.CommunityPractice &&
                site.Organization != null &&
                site.Organization.RegistrationId != null)
            {
                var eras = await matchExceptionRemoteAccessSite(site.PEC, site.Organization.RegistrationId);
                site.RemoteAccessTypeCode = eras != null ? eras.RemoteAccessTypeCode : (int)RemoteAccessTypeEnum.PrivateCommunityHealthPractice;
            }

            return site;
        }

        public async Task<List<Vendor>> GetVendorsAsync()
        {
            return await _context.Vendors.ToListAsync();
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

            var updateDetail = new List<string>();
            if (currentSite.SubmittedDate == null)
            {
                var vendors = await GetVendorsAsync();
                // Returned change details are discarded because they aren't needed if site hasn't been submitted yet,
                // and in the case of post-submit/approval, at this time, vendor can't be updated due to business rules
                UpdateVendors(currentSite, updatedSite, vendors);
            }

            var addressUpdate = UpdateAddress(currentSite, updatedSite);
            var contactsUpdate = UpdateContacts(currentSite, updatedSite);
            var businessHoursUpdate = UpdateBusinessHours(currentSite, updatedSite);
            var updateRemoteUserResult = UpdateRemoteUsers(currentSite, updatedSite.RemoteUsers);

            if (currentSite.SubmittedDate != null)
            {
                updateDetail.AddRange(addressUpdate);
                updateDetail.AddRange(contactsUpdate);
                updateDetail.AddRange(businessHoursUpdate);
                updateDetail.AddRange(updateRemoteUserResult);
            }

            await UpdateIndividualDeviceProviders(siteId, updatedSite.IndividualDeviceProviders);

            var logMessage = new StringBuilder("Site Updated");
            if (updateDetail.Count > 0)
            {
                logMessage.Append($"{Environment.NewLine + "- "}{string.Join(Environment.NewLine + "- ", updateDetail.ToArray())}");
            }
            await _businessEventService.CreateSiteEventAsync(currentSite.Id, logMessage.ToString());

            try
            {
                await _context.SaveChangesAsync();
                //send email only when the site is completed and org. agreement should have been signed.
                if (updateRemoteUserResult.Count > 0 && currentSite.Completed)
                {
                    var site = await GetSiteAsync(siteId);
                    // Send HIBC an email when remote users are updated for a submitted site
                    await _emailService.SendRemoteUsersUpdatedAsync(site, updateRemoteUserResult);
                    await _businessEventService.CreateSiteEmailEventAsync(siteId, "Sent remote user(s) updated notification");
                }
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
                .Where(s => s.Id == siteId && s.DeletedDate == null)
                .Select(s => new PermissionsRecord { Username = s.Organization.SigningAuthority.Username })
                .SingleOrDefaultAsync();
        }

        private List<string> UpdateAddress(Site current, CommunitySiteUpdateModel updated)
        {
            var result = new List<string>();
            if (updated.PhysicalAddress == null)
            {
                return result;
            }

            if (current.PhysicalAddress == null)
            {
                current.PhysicalAddress = updated.PhysicalAddress;
                result.Add($"Physical Address '{AddressToString(updated.PhysicalAddress)}' was added.");
            }
            else
            {
                var fromAddressStr = AddressToString(current.PhysicalAddress);
                var toAddressStr = AddressToString(updated.PhysicalAddress);
                if (!fromAddressStr.Equals(toAddressStr))
                {
                    result.Add($"Physical Address changed from {Environment.NewLine}   {fromAddressStr}{Environment.NewLine}  to{Environment.NewLine}   {toAddressStr}.");
                }
                _context.Entry(current.PhysicalAddress).CurrentValues.SetValues(updated.PhysicalAddress);
            }
            return result;
        }

        private string AddressToString(PhysicalAddress address)
        {
            return $"{address.Street} {address.Street2} {address.City} {address.ProvinceCode} {address.CountryCode} {address.Postal}";
        }

        private List<string> UpdateContacts(CommunitySite current, CommunitySiteUpdateModel updated)
        {
            var result = new List<string>();
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
                    result.Add($"New {TranslateContactType(contactType)} was added.");
                }
                else
                {
                    var contactDiff = CompareContact(currentContact, updatedContact);
                    if (contactDiff != null)
                    {
                        result.Add($"{TranslateContactType(contactType)} was updated.{contactDiff}");
                    }
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
            return result;
        }

        private static string TranslateContactType(string contactType)
        {
            return contactType switch
            {
                "AdministratorPharmaNet" => "PharmaNet Administrator",
                "PrivacyOfficer" => "Privacy Officer",
                "TechnicalSupport" => "Technical Support",
                _ => ""
            };
        }

        private string CompareContact(Contact currentContact, Contact newContact)
        {
            var result = new List<string>();
            var propertyNames = new[,]
            {
                {nameof(currentContact.FirstName), "First Name"},
                {nameof(currentContact.LastName), "Last Name"},
                {nameof(currentContact.JobRoleTitle), "Job Title"},
                {nameof(currentContact.Email), "Email"},
                {nameof(currentContact.Phone), "Phone"},
                {nameof(currentContact.Fax), "Fax"},
                {nameof(currentContact.SMSPhone), "SMS Phone"},
            };

            for (var i = 0; i < (propertyNames.Length / 2); i++)
            {
                var property = currentContact.GetType().GetProperty(propertyNames[i, 0]);

                string currentPropertyValue = property.GetValue(currentContact, null) as string;
                string newPropertyValue = property.GetValue(newContact, null) as string;

                if (currentPropertyValue != null || newPropertyValue != null)
                {
                    if ((currentPropertyValue != null && newPropertyValue == null) ||
                    (currentPropertyValue == null && newPropertyValue != null) ||
                    !currentPropertyValue.Equals(newPropertyValue))
                    {
                        result.Add($"   {propertyNames[i, 1]} has changed from '{currentPropertyValue}' to '{newPropertyValue}'.");
                    }
                }
            }

            var currentAddress = currentContact.PhysicalAddress == null ?
                "Same address as the site's address" : AddressToString(currentContact.PhysicalAddress);
            var newAddress = newContact.PhysicalAddress == null ?
                "Same address as the site's address" : AddressToString(newContact.PhysicalAddress);

            if (!currentAddress.Equals(newAddress))
            {
                result.Add($"   Address has changed from {Environment.NewLine + currentAddress + Environment.NewLine} To {Environment.NewLine + newAddress}.");
            }
            if (result.Count > 0)
            {
                return $"{Environment.NewLine}   {string.Join(Environment.NewLine + "   ", result)}";
            }
            else
            {
                return null;
            }
        }

        private List<string> UpdateBusinessHours(Site current, CommunitySiteUpdateModel updated)
        {
            var result = new List<string>();
            if (updated.BusinessHours == null)
            {
                return result;
            }

            var currentHourStr = "";
            if (current.BusinessHours != null)
            {
                foreach (var businessHour in current.BusinessHours)
                {
                    var endTime = businessHour.EndTime.Days == 1 ? "24:00" : $"{businessHour.EndTime.Hours:D2}:{businessHour.EndTime.Minutes:D2}";
                    currentHourStr += $"     {businessHour.Day}   {businessHour.StartTime.Hours:D2}:{businessHour.StartTime.Minutes:D2} - {endTime}" + Environment.NewLine;
                    _context.Remove(businessHour);
                }
            }

            var newHourStr = "";
            foreach (var businessHour in updated.BusinessHours)
            {
                var endTime = businessHour.EndTime.Days == 1 ? "24:00" : $"{businessHour.EndTime.Hours:D2}:{businessHour.EndTime.Minutes:D2}";

                newHourStr += $"     {businessHour.Day}   {businessHour.StartTime.Hours:D2}:{businessHour.StartTime.Minutes:D2} - {endTime}" + Environment.NewLine;
                businessHour.SiteId = current.Id;
                _context.Entry(businessHour).State = EntityState.Added;
            }

            if (!currentHourStr.Equals(newHourStr))
            {
                result.Add($"Hours updated from {Environment.NewLine}{currentHourStr}  to{Environment.NewLine}{newHourStr}");
            }

            return result;
        }

        /// <summary>
        /// Returns whether there were any changes to the site's remote users
        /// </summary>
        private List<string> UpdateRemoteUsers(Site current, IEnumerable<SiteRemoteUserUpdateModel> updateRemoteUsers)
        {
            var result = new List<string>();
            if (updateRemoteUsers == null)
            {
                return result;
            }

            // All RemoteUserCertifications will be dropped and re-added, so we must set all incoming PKs/FKs to 0
            // This can be removed when / if the updated Certs become a View Model without FKs.
            foreach (var cert in updateRemoteUsers.Select(x => x.RemoteUserCertification))
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

                    // Only considered an update if incoming and existing aren't equal
                    if (!updatedUser.Equals(existing))
                    {
                        updatedUser.SiteId = current.Id;
                        _context.Entry(existing).CurrentValues.SetValues(updatedUser);

                        _context.RemoteUserCertifications.Remove(existing.RemoteUserCertification);

                        updatedUser.RemoteUserCertification.RemoteUserId = updatedUser.Id;
                        _context.RemoteUserCertifications.Add(updatedUser.RemoteUserCertification);

                        result.Add($"Remote user '{updatedUser.FirstName} {updatedUser.LastName}' was updated.");
                    }
                }
                else
                {
                    var newRemoteUser = _mapper.Map<RemoteUser>(updatedUser);
                    newRemoteUser.Id = 0;
                    newRemoteUser.SiteId = current.Id;
                    _context.RemoteUsers.Add(newRemoteUser);

                    result.Add($"Remote user '{updatedUser.FirstName} {updatedUser.LastName}' was added.");
                }
            }

            //remove RemoteAccessSites and RemoteAccessLocation records for remote users that will be deleted
            var enrolleeIds = _context.EnrolleeRemoteUsers.Where(u => existingUsers.Keys.Contains(u.RemoteUserId))
                                                .Select(u => u.EnrolleeId)
                                                .ToList();
            var remoteAccessSites = _context.RemoteAccessSites.Where(s => enrolleeIds.Contains(s.EnrolleeId) && s.SiteId == current.Id)
                                                .Select(s => s)
                                                .ToList();
            _context.RemoteAccessSites.RemoveRange(remoteAccessSites);

            foreach (var enrolleeId in enrolleeIds)
            {
                // Check if the enrollee has only one remote access site. if so, remove the remote access location
                if (_context.RemoteAccessSites.Where(s => s.EnrolleeId == enrolleeId).Count() == 1)
                {
                    _context.RemoteAccessLocations.Where(l => l.EnrolleeId == enrolleeId).ExecuteDelete();
                }
            }

            foreach (var pendingToRemoveUser in existingUsers.Values)
            {
                var message = $"Remote user '{pendingToRemoveUser.FirstName} {pendingToRemoveUser.LastName}', " +
                    $"{GetRemoteUserCollegeLicenseInfo(pendingToRemoveUser.RemoteUserCertification)} was removed.";
                result.Add(message);
            }
            _context.RemoteUsers.RemoveRange(existingUsers.Values);

            return result;
        }

        private string GetRemoteUserCollegeLicenseInfo(RemoteUserCertification remoteUserCert)
        {
            if (remoteUserCert.CollegeCode == CollegeCode.CPSBC)
            {
                return $"CPSBC, CPSID Number: {remoteUserCert.LicenseNumber}";
            }
            else if (remoteUserCert.CollegeCode == CollegeCode.BCCNM)
            {
                return $"BCCNM, PharmaNet ID: {remoteUserCert.PractitionerId}";
            }
            else
            {
                return $"{remoteUserCert.College.Name}, Registration ID: {remoteUserCert.LicenseNumber}";
            }
        }

        private List<string> UpdateVendors(CommunitySite current, CommunitySiteUpdateModel updated, List<Vendor> vendors)
        {
            var result = new List<string>();
            if (updated?.SiteVendors != null)
            {
                if (current.SiteVendors != null)
                {
                    foreach (var vendor in current.SiteVendors)
                    {
                        var vendorName = vendors.Where(v => v.Code == vendor.VendorCode).Select(v => v.Name).First();
                        result.Add($"{vendorName} was removed.");
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

                    var vendorName = vendors.Where(v => v.Code == vendor.VendorCode).Select(v => v.Name).First();
                    result.Add($"{vendorName} was added.");
                    _context.Entry(siteVendor).State = EntityState.Added;
                }
            }
            return result;
        }

        private async Task UpdateIndividualDeviceProviders(int siteId, IEnumerable<IndividualDeviceProviderChangeModel> updated)
        {
            if (updated == null)
            {
                return;
            }

            var currentProviders = await _context.IndividualDeviceProviders
                .Where(p => p.CommunitySiteId == siteId)
                .ToListAsync();
            _context.IndividualDeviceProviders.RemoveRange(currentProviders);

            foreach (var provider in updated)
            {
                var newModel = _mapper.Map<IndividualDeviceProvider>(provider);
                newModel.CommunitySiteId = siteId;
                _context.IndividualDeviceProviders.Add(newModel);
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
                .Where(s => s.Id == siteId && s.DeletedDate == null)
                .Select(s => s.BusinessLicence)
                .DecompileAsync()
                .SingleOrDefaultAsync();
        }

        public async Task<bool> SiteExistsAsync(int siteId)
        {
            return await _context.CommunitySites
                .AsNoTracking()
                .AnyAsync(s => s.Id == siteId && s.DeletedDate == null);
        }

        public async Task<IEnumerable<IndividualDeviceProviderViewModel>> GetIndividualDeviceProvidersAsync(int siteId)
        {
            return await _context.IndividualDeviceProviders
                .Where(p => p.CommunitySiteId == siteId)
                .ProjectTo<IndividualDeviceProviderViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task UpdateSigningAuthorityForOrganization(int organizationId, int partyId)
        {
            var sites = await _context.CommunitySites
                .Where(s => s.OrganizationId == organizationId && s.DeletedDate == null)
                .ToListAsync();

            foreach (var site in sites)
            {
                site.ProvisionerId = partyId;
            }

            await _context.SaveChangesAsync();
        }

        private IQueryable<CommunitySite> GetBaseSiteQuery()
        {
            return _context.CommunitySites
                .Where(s => s.DeletedDate == null)
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
                .Include(s => s.BusinessHours.OrderBy(bh => bh.Day))
                .Include(s => s.BusinessLicences)
                    .ThenInclude(bl => bl.BusinessLicenceDocument)
                .Include(s => s.Adjudicator)
                .Include(s => s.SiteSubmissions);
        }

        private ICollection<RemoteUser> GetRemoteUsersOfSite(int siteId)
        {
            return _context.RemoteUsers
                .Where(ru => ru.SiteId == siteId)
                .Include(r => r.RemoteUserCertification)
                    .ThenInclude(c => c.College)
                .ToList();
        }

        private ICollection<SiteStatus> GetStatusesOfSite(int siteId)
        {
            return _context.SiteStatuses
                .Where(ss => ss.SiteId == siteId)
                .ToList();
        }

        private async Task<ExceptionRemoteAccessSite> matchExceptionRemoteAccessSite(string siteId, string registrationId)
        {
            return await _context.ExceptionRemoteAccessSites
                .Where(s => s.PEC == siteId && s.RegistrationId == registrationId).SingleOrDefaultAsync();
        }
    }
}
