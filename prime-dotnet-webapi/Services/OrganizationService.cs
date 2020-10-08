using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;
using Prime.ViewModels;
using Prime.HttpClients;

namespace Prime.Services
{
    public class OrganizationService : BaseService, IOrganizationService
    {
        private readonly IBusinessEventService _businessEventService;
        private readonly IPartyService _partyService;
        private readonly IDocumentManagerClient _documentClient;

        public OrganizationService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IBusinessEventService businessEventService,
            IPartyService partyService,
            IDocumentManagerClient documentClient)
            : base(context, httpContext)
        {
            _businessEventService = businessEventService;
            _partyService = partyService;
            _documentClient = documentClient;
        }

        public async Task<IEnumerable<Organization>> GetOrganizationsAsync(int? partyId = null)
        {
            IQueryable<Organization> query = this.GetBaseOrganizationQuery();

            if (partyId != null)
            {
                query = query.Where(o => o.SigningAuthorityId == partyId);
            }

            return await query
                .Include(o => o.Sites).ThenInclude(s => s.SiteVendors)
                .Include(o => o.Sites).ThenInclude(s => s.PhysicalAddress)
                .Include(o => o.Sites).ThenInclude(s => s.Adjudicator)
                .Include(o => o.Sites).ThenInclude(s => s.RemoteUsers)
                .ToListAsync();
        }

        public async Task<Organization> GetOrganizationAsync(int organizationId)
        {
            return await this.GetBaseOrganizationQuery()
                .SingleOrDefaultAsync(o => o.Id == organizationId);
        }

        public async Task<int> CreateOrganizationAsync(Party signingAuthority)
        {
            signingAuthority.ThrowIfNull(nameof(signingAuthority));

            var userId = _httpContext.HttpContext.User.GetPrimeUserId();

            var partyExists = await _partyService.PartyUserIdExistsAsync(userId);

            if (!partyExists)
            {
                await _partyService.CreatePartyAsync(signingAuthority);
            }

            signingAuthority = await _partyService.GetPartyForUserIdAsync(userId);

            var organizations = await GetOrganizationsAsync(signingAuthority.Id);
            if (organizations.Count() != 0)
            {
                throw new InvalidOperationException("Could not create Organization. Only one organization can exist for a party.");
            }

            var organization = new Organization
            { SigningAuthorityId = signingAuthority.Id };

            _context.Organizations.Add(organization);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create Organization.");
            }

            await _businessEventService.CreateOrganizationEventAsync(organization.Id, signingAuthority.Id, "Organization Created");

            return organization.Id;
        }

        public async Task<int> UpdateOrganizationAsync(int organizationId, OrganizationUpdateModel updatedOrganization)
        {
            var currentOrganization = await this.GetOrganizationAsync(organizationId);

            // BCSC Fields
            var userId = currentOrganization.SigningAuthority.UserId;

            this._context.Entry(currentOrganization).CurrentValues.SetValues(updatedOrganization);
            this._context.Entry(currentOrganization.SigningAuthority).CurrentValues.SetValues(updatedOrganization.SigningAuthority);

            _partyService.UpdatePartyPhysicalAddress(currentOrganization.SigningAuthority, updatedOrganization.SigningAuthority);

            _partyService.UpdatePartyMailingAddress(currentOrganization.SigningAuthority, updatedOrganization.SigningAuthority);

            // Keep userId the same from BCSC card, do not update
            currentOrganization.SigningAuthority.UserId = userId;

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
            var organization = await this.GetBaseOrganizationQuery()
                .SingleOrDefaultAsync(o => o.Id == organizationId);

            organization.Completed = true;

            this._context.Update(organization);

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not update the organization.");
            }

            return updated;
        }

        public async Task DeleteOrganizationAsync(int organizationId)
        {
            var organization = await this.GetBaseOrganizationQuery()
                .SingleOrDefaultAsync(o => o.Id == organizationId);

            if (organization == null)
            {
                return;
            }

            _context.Organizations.Remove(organization);

            await _context.SaveChangesAsync();
        }

        public async Task<Organization> SubmitRegistrationAsync(int organizationId)
        {
            var organization = await GetOrganizationAsync(organizationId);
            organization.SubmittedDate = DateTimeOffset.Now;
            _context.Update(organization);

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not submit the organization.");
            }

            return organization;
        }

        public async Task<Organization> GetOrganizationNoTrackingAsync(int organizationId)
        {
            return await this.GetBaseOrganizationQuery()
                .AsNoTracking()
                .SingleOrDefaultAsync(o => o.Id == organizationId);
        }

        public async Task<int> AcceptCurrentOrganizationAgreementAsync(int organizationId)
        {
            var organization = await GetOrganizationAsync(organizationId);

            organization.AcceptedAgreementDate = DateTimeOffset.Now;

            return await _context.SaveChangesAsync();
        }

        public async Task<Organization> GetOrganizationByPartyIdAsync(int partyId)
        {
            return await _context.Organizations
                .SingleOrDefaultAsync(o => o.SigningAuthorityId == partyId);
        }

        public async Task<SignedAgreementDocument> AddSignedAgreementAsync(int organizationId, Guid documentGuid)
        {
            var filename = await _documentClient.FinalizeUploadAsync(documentGuid, "signed_org_agreements");
            if (string.IsNullOrWhiteSpace(filename))
            {
                return null;
            }

            var signedAgreement = new SignedAgreementDocument
            {
                DocumentGuid = documentGuid,
                OrganizationId = organizationId,
                Filename = filename,
                UploadedDate = DateTimeOffset.Now
            };
            _context.SignedAgreementDocuments.Add(signedAgreement);

            await _context.SaveChangesAsync();

            return signedAgreement;
        }

        public async Task<SignedAgreementDocument> GetLatestSignedAgreementAsync(int organizationId)
        {
            return await _context.SignedAgreementDocuments
                .Where(sa => sa.OrganizationId == organizationId)
                .OrderByDescending(sa => sa.UploadedDate)
                .FirstOrDefaultAsync();
        }

        private IQueryable<Organization> GetBaseOrganizationQuery()
        {
            return _context.Organizations
                .Include(o => o.Agreements)
                    .ThenInclude(a => a.SignedAgreement)
                .Include(o => o.SigningAuthority)
                    .ThenInclude(p => p.PhysicalAddress)
                .Include(o => o.SigningAuthority)
                    .ThenInclude(p => p.MailingAddress);
        }
    }
}
