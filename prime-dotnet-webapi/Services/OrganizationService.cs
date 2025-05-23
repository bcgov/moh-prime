using AutoMapper;
using AutoMapper.QueryableExtensions;
using DelegateDecompiler.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Prime.HttpClients;
using Prime.HttpClients.DocumentManagerApiDefinitions;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public class OrganizationService : BaseService, IOrganizationService
    {
        private readonly IBusinessEventService _businessEventService;
        private readonly IDocumentManagerClient _documentClient;
        private readonly IMapper _mapper;
        private readonly IOrganizationClaimService _organizationClaimService;
        private readonly IPartyService _partyService;
        private readonly IOrgBookClient _orgBookClient;

        public OrganizationService(
            ApiDbContext context,
            ILogger<OrganizationService> logger,
            IBusinessEventService businessEventService,
            IDocumentManagerClient documentClient,
            IMapper mapper,
            IOrganizationClaimService organizationClaimService,
            IPartyService partyService,
            IOrgBookClient orgBookClient)
            : base(context, logger)
        {
            _businessEventService = businessEventService;
            _documentClient = documentClient;
            _mapper = mapper;
            _organizationClaimService = organizationClaimService;
            _partyService = partyService;
            _orgBookClient = orgBookClient;
        }

        public async Task<bool> OrganizationExistsAsync(int organizationId)
        {
            return await _context.Organizations
                .AsNoTracking()
                .AnyAsync(o => o.Id == organizationId && o.DeletedDate == null);
        }

        public async Task<IEnumerable<OrganizationListViewModel>> GetOrganizationsByPartyIdAsync(int partyId)
        {
            return await _context.Organizations
                .Include(o => o.SigningAuthority)
                    .ThenInclude(sa => sa.Addresses)
                        .ThenInclude(pa => pa.Address)
                .Include(o => o.Sites.Where(s => s.DeletedDate == null))
                .Where(o => o.SigningAuthorityId == partyId && o.DeletedDate == null)
                .ProjectTo<OrganizationListViewModel>(_mapper.ConfigurationProvider)
                .DecompileAsync()
                .ToListAsync();
        }

        public async Task<IEnumerable<OrganizationAdminListViewModel>> GetOrganizationAdminListViewAsync(string searchText)
        {
            return await _context.Organizations
                .AsNoTracking()
                .Where(o => o.DeletedDate == null)
                .If(!string.IsNullOrWhiteSpace(searchText), q => q
                    .Search(e => e.Name,
                    e => e.DoingBusinessAs,
                    e => e.SigningAuthority.FirstName,
                    e => e.SigningAuthority.LastName)
                    .Containing(searchText))
                .ProjectTo<OrganizationAdminListViewModel>(_mapper.ConfigurationProvider)
                .DecompileAsync()
                .ToListAsync();
        }

        public async Task<OrganizationAdminListViewModel> GetOrganizationAdminListViewByIdAsync(int id)
        {
            return await _context.Organizations
                .AsNoTracking()
                .Where(o => o.Id == id && o.DeletedDate == null)
                .ProjectTo<OrganizationAdminListViewModel>(_mapper.ConfigurationProvider)
                .DecompileAsync()
                .SingleOrDefaultAsync();
        }

        public async Task<Organization> GetOrganizationAsync(int organizationId)
        {
            return await GetBaseOrganizationQuery()
                .Include(o => o.Sites.Where(s => s.DeletedDate == null))
                    .ThenInclude(s => s.SiteStatuses)
                .SingleOrDefaultAsync(o => o.Id == organizationId);
        }

        public async Task<int> GetOrganizationSigningAuthorityIdAsync(int organizationId)
        {
            return await _context.Organizations
                .AsNoTracking()
                .Where(o => o.Id == organizationId && o.DeletedDate == null)
                .Select(o => o.SigningAuthorityId)
                .SingleOrDefaultAsync();
        }

        public async Task<Organization> GetOrganizationByPecAsync(string pec)
        {
            return await GetBaseOrganizationQuery()
                .Include(o => o.Sites)
                .Where(o => o.Sites.Any(s => s.PEC == pec && s.ArchivedDate == null && s.DeletedDate == null))
                .SingleOrDefaultAsync();
        }

        public async Task<int> CreateOrganizationAsync(int partyId)
        {
            var organization = new Organization
            {
                SigningAuthorityId = partyId
            };

            _context.Organizations.Add(organization);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create Organization.");
            }

            await _businessEventService.CreateOrganizationEventAsync(organization.Id, partyId, "Organization Created");

            return organization.Id;
        }


        public async Task<int> UpdateOrganizationAsync(int organizationId, OrganizationUpdateModel updatedOrganization)
        {
            var currentOrganization = await GetOrganizationAsync(organizationId);
            _context.Entry(currentOrganization).CurrentValues.SetValues(updatedOrganization);

            await _businessEventService.CreateOrganizationEventAsync(currentOrganization.Id, currentOrganization.SigningAuthorityId, "Organization Updated");

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public async Task<int> UpdateCompletedAsync(int organizationId)
        {
            var organization = await GetOrganizationOnlyAsync(organizationId);

            organization.Completed = true;

            _context.Update(organization);

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not update the organization.");
            }

            return updated;
        }

        public async Task DeleteOrganizationAsync(int organizationId)
        {
            var organization = await GetBaseOrganizationQuery()
                .SingleOrDefaultAsync(o => o.Id == organizationId);

            if (organization == null)
            {
                return;
            }

            var deletedDate = DateTime.UtcNow;
            organization.DeletedDate = deletedDate;

            foreach (var site in organization.Sites)
            {
                site.DeletedDate = deletedDate;
            }

            _context.Update(organization);

            await _context.SaveChangesAsync();
        }

        public async Task<Organization> GetOrganizationNoTrackingAsync(int organizationId)
        {
            return await GetBaseOrganizationQuery()
                .AsNoTracking()
                .SingleOrDefaultAsync(o => o.Id == organizationId);
        }

        /// <summary>
        /// Creates a new Org Agreement of type appropriate for the indicated site if none exist or a newer version is available.
        /// Otherwise, returns the newest existing Agreement of that type.
        /// Returns null if the Site doesn't exist on the Organization.
        /// </summary>
        public async Task<Agreement> EnsureUpdatedOrgAgreementAsync(int organizationId, int careSettingCode, int signingAuthorityId)
        {
            var siteExists = await _context.CommunitySites
                .AsNoTracking()
                .Where(s => s.CareSettingCode == careSettingCode && s.OrganizationId == organizationId && s.DeletedDate == null)
                .AnyAsync();

            if (!siteExists)
            {
                return null;
            }

            var agreementType = OrgAgreementTypeForSiteSetting(careSettingCode);

            var newestVersionId = await _context.AgreementVersions
                .AsNoTracking()
                .OrderByDescending(v => v.EffectiveDate)
                .Where(v => v.AgreementType == agreementType)
                .Select(v => v.Id)
                .FirstAsync();

            var newestAgreement = await _context.Agreements
                .OrderByDescending(a => a.CreatedDate)
                .FirstOrDefaultAsync(a => a.OrganizationId == organizationId && a.AgreementVersionId == newestVersionId && a.PartyId == signingAuthorityId);

            if (newestAgreement != null)
            {
                return newestAgreement;
            }
            else
            {
                // Either no Agreement, or only outdated versions. Create a new one
                var newAgreement = new Agreement
                {
                    OrganizationId = organizationId,
                    AgreementVersionId = newestVersionId,
                    CreatedDate = DateTimeOffset.Now,
                    PartyId = signingAuthorityId
                };

                _context.Agreements.Add(newAgreement);
                await _context.SaveChangesAsync();
                return newAgreement;
            }
        }

        public async Task<IEnumerable<CareSettingType>> GetCareSettingCodesForPendingTransferAsync(int organizationId, int signingAuthorityId)
        {
            // Get a list of the care settings used on sites that exist for an organization
            var oganizationCareSettings = await _context.CommunitySites
                .AsNoTracking()
                .Where(s => s.OrganizationId == organizationId)
                .Select(s => s.CareSettingCode)
                .ToListAsync();

            var agreements = await _context.Agreements
                .AsNoTracking()
                .Where(a => a.OrganizationId == organizationId)
                .OrderByDescending(a => a.CreatedDate)
                .Select(a => new
                {
                    a.AgreementVersion.AgreementType,
                    a.PartyId,
                    a.AcceptedDate
                })
                .ToListAsync();

            // Get Care Settings for agreements signed by previous signing authority and unsigned agreements from current signing authority
            var agreementSettings = agreements
                .GroupBy(a => a.AgreementType)
                .Select(a => a.FirstOrDefault())
                .Where(a => a.PartyId != signingAuthorityId || (a.PartyId == signingAuthorityId && a.AcceptedDate == null))
                .Select(a => SiteSettingForOrgAgreementType(a.AgreementType));

            return agreementSettings
                .Where(code => oganizationCareSettings.Contains((int)code))
                .ToList();
        }

        public async Task<bool> IsOrganizationTransferCompleteAsync(int organizationId)
        {
            var signingAuthorityId = await _context.Organizations
                .Where(o => o.Id == organizationId && o.DeletedDate == null)
                .Select(o => o.SigningAuthorityId)
                .SingleOrDefaultAsync();

            var pendingCodes = await GetCareSettingCodesForPendingTransferAsync(organizationId, signingAuthorityId);

            return !pendingCodes.Any();
        }

        public async Task FlagPendingTransferIfOrganizationAgreementsRequireSignaturesAsync(int organizationId)
        {
            var organization = await GetOrganizationOnlyAsync(organizationId);

            var pendingCodes = await GetCareSettingCodesForPendingTransferAsync(organizationId, organization.SigningAuthorityId);
            if (pendingCodes.Any())
            {
                organization.PendingTransfer = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task FinalizeTransferAsync(int organizationId)
        {
            var organization = await GetOrganizationOnlyAsync(organizationId);

            organization.PendingTransfer = false;
            await _context.SaveChangesAsync();
        }

        public async Task AcceptOrgAgreementAsync(int organizationId, int agreementId)
        {
            var agreement = await _context.Agreements
                .SingleOrDefaultAsync(a => a.Id == agreementId && a.OrganizationId == organizationId);

            if (agreement != null)
            {
                agreement.AcceptedDate = DateTimeOffset.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<SignedAgreementDocument> AddSignedAgreementAsync(int organizationId, int agreementId, Guid documentGuid, string filename = "")
        {
            if (filename == "")
            {
                filename = await _documentClient.FinalizeUploadAsync(documentGuid, DestinationFolders.SignedOrgAgreements);
                if (string.IsNullOrWhiteSpace(filename))
                {
                    return null;
                }
            }

            var signedAgreement = new SignedAgreementDocument
            {
                DocumentGuid = documentGuid,
                AgreementId = agreementId,
                Filename = filename,
                UploadedDate = DateTimeOffset.Now
            };
            _context.SignedAgreementDocuments.Add(signedAgreement);

            await _context.SaveChangesAsync();

            return signedAgreement;
        }

        public async Task<SignedAgreementDocument> GetLatestSignedAgreementAsync(int organizationId)
        {
            return await _context.Organizations
                .Where(o => o.Id == organizationId && o.DeletedDate == null)
                .SelectMany(o => o.Agreements)
                .Select(a => a.SignedAgreement)
                .OrderByDescending(sa => sa.UploadedDate)
                .FirstOrDefaultAsync();
        }

        public AgreementType OrgAgreementTypeForSiteSetting(int careSettingCode)
        {
            return (CareSettingType)careSettingCode switch
            {
                CareSettingType.CommunityPractice => AgreementType.CommunityPracticeOrgAgreement,
                CareSettingType.CommunityPharmacy => AgreementType.CommunityPharmacyOrgAgreement,
                CareSettingType.DeviceProvider => AgreementType.DeviceProviderOrgAgreement,
                _ => throw new InvalidOperationException($"Did not recognize care setting code {careSettingCode} in {nameof(OrgAgreementTypeForSiteSetting)}"),
            };
        }

        public CareSettingType SiteSettingForOrgAgreementType(AgreementType agreementTypeCode)
        {
            return agreementTypeCode switch
            {
                AgreementType.CommunityPharmacyOrgAgreement => CareSettingType.CommunityPharmacy,
                AgreementType.CommunityPracticeOrgAgreement => CareSettingType.CommunityPractice,
                AgreementType.DeviceProviderOrgAgreement => CareSettingType.DeviceProvider,
                _ => throw new InvalidOperationException($"Did not recognize agreement type code {agreementTypeCode} in {nameof(SiteSettingForOrgAgreementType)}"),
            };
        }

        private IQueryable<Organization> GetBaseOrganizationQuery()
        {
            return _context.Organizations
                .Where(o => o.DeletedDate == null)
                .Include(o => o.Agreements)
                    .ThenInclude(a => a.SignedAgreement)
                .Include(o => o.SigningAuthority)
                    .ThenInclude(sa => sa.Addresses)
                        .ThenInclude(pa => pa.Address)
                .Include(o => o.Claims);
        }

        public async Task SwitchSigningAuthorityAsync(int organizationId, int newSigningAuthorityId)
        {
            var organization = await GetOrganizationOnlyAsync(organizationId);

            organization.SigningAuthorityId = newSigningAuthorityId;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveUnsignedOrganizationAgreementsAsync(int organizationId)
        {
            // Delete all non-accepted agreements
            var pendingAgreements = await _context.Agreements
                .Where(a => a.OrganizationId == organizationId && a.AcceptedDate == null)
                .ToListAsync();

            _context.RemoveRange(pendingAgreements);
            await _context.SaveChangesAsync();
        }

        // update organization registration ID calling OrgBook API with organization name in PRIME, then return the number of organizations updated
        public async Task<int> UpdateMissingRegistrationIds()
        {
            var targetOrganizations = await _context.Organizations.Where(o => o.RegistrationId == null && o.DeletedDate == null)
                .OrderBy(o => o.Id)
                .ToListAsync();
            int numUpdated = 0;
            if (targetOrganizations.Any())
            {
                foreach (var org in targetOrganizations)
                {
                    string registrationId = await _orgBookClient.GetOrgBookRegistrationIdAsync(org.Name);
                    if (registrationId != null)
                    {
                        org.RegistrationId = registrationId;
                        numUpdated++;
                        _logger.LogInformation($"Organization (ID:{org.Id}) registration ID is set to {registrationId}.");
                    }
                }
                await _context.SaveChangesAsync();
            }

            return numUpdated;
        }

        private async Task<Organization> GetOrganizationOnlyAsync(int organizationId)
        {
            // Does not Include any related entities and
            // is suitable for updates
            return await _context.Organizations
                .Where(o => o.DeletedDate == null)
                .SingleOrDefaultAsync(o => o.Id == organizationId);
        }
    }
}
