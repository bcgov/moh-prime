using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Prime.Models;
using Prime.Models.Api;
using Prime.ViewModels;
using Prime.ViewModels.Agreements;
using Prime.HttpClients;
using Prime.HttpClients.DocumentManagerApiDefinitions;
using Prime.Services.Razor;

namespace Prime.Services
{
    public class AgreementService : BaseService, IAgreementService
    {
        private static readonly TimeSpan AGREEMENT_EXPIRY = TimeSpan.FromDays(365);

        private readonly IMapper _mapper;
        private readonly IPdfService _pdfService;
        private readonly IRazorConverterService _razorConverterService;
        private readonly IDocumentManagerClient _documentClient;

        public AgreementService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            IPdfService pdfService,
            IRazorConverterService razorConverterService,
            IDocumentManagerClient documentClient)
            : base(context, httpContext)
        {
            _mapper = mapper;
            _pdfService = pdfService;
            _razorConverterService = razorConverterService;
            _documentClient = documentClient;
        }

        /// <summary>
        /// Gets the agreement for an enrollee by ID, if it exists (No Tracking).
        /// </summary>
        public async Task<Agreement> GetEnrolleeAgreementAsync(int enrolleeId, int agreementId, bool includeText = false)
        {
            var agreement = await _context.Agreements
                .AsNoTracking()
                .Where(at => at.Id == agreementId)
                .Where(at => at.EnrolleeId == enrolleeId)
                .If(includeText, q => q.Include(at => at.AgreementVersion).Include(at => at.LimitsConditionsClause))
                .SingleOrDefaultAsync();

            if (includeText)
            {
                await RenderHtml(agreement);
            }

            return agreement;
        }

        /// <summary>
        /// Get the list of agreements for an enrollee, using filters (No Tracking).
        /// </summary>
        public async Task<IEnumerable<Agreement>> GetEnrolleeAgreementsAsync(int enrolleeId, AgreementFilters filters)
        {
            filters ??= new AgreementFilters();

            var agreements = await _context.Agreements
                .AsNoTracking()
                .Where(at => at.EnrolleeId == enrolleeId)
                .OrderByDescending(at => at.CreatedDate)
                .If(filters.OnlyLatest, q => q.Take(1))
                .If(filters.Accepted == true || filters.YearAccepted.HasValue, q => q.Where(at => at.AcceptedDate.HasValue))
                .If(filters.Accepted == false, q => q.Where(at => !at.AcceptedDate.HasValue))
                .If(filters.IncludeText, q => q.Include(at => at.AgreementVersion).Include(at => at.LimitsConditionsClause))
                .ToArrayAsync();

            if (filters.YearAccepted.HasValue)
            {
                // NpgSQL does not support DateTimeOffset operations, this filtering must be done after fetching all the data :(
                agreements = agreements
                    .Where(at => at.AcceptedDate.Value.Year == filters.YearAccepted)
                    .ToArray();
            }

            if (filters.IncludeText)
            {
                await RenderHtml(agreements);
            }

            return agreements;
        }

        public async Task CreateEnrolleeAgreementAsync(int enrolleeId)
        {
            var dto = await _context.Enrollees
                .AsNoTracking()
                .Where(e => e.Id == enrolleeId)
                .Select(e => new
                {
                    NewestAssignedAgreement = e.Submissions
                        .OrderByDescending(s => s.CreatedDate)
                        .Select(s => s.AgreementType)
                        .FirstOrDefault(),
                    e.AccessAgreementNote
                })
                .SingleAsync();

            if (dto.NewestAssignedAgreement == null)
            {
                throw new InvalidOperationException("Agreement type is required to approve an enrollee");
            }

            var agreement = new Agreement
            {
                EnrolleeId = enrolleeId,
                AgreementVersionId = await FetchNewestAgreementVersionIdOfType(dto.NewestAssignedAgreement.Value),
                LimitsConditionsClause = LimitsConditionsClause.FromAgreementNote(dto.AccessAgreementNote),
                CreatedDate = DateTimeOffset.Now
            };

            _context.Add(agreement);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the Enrollee's newest Agreement
        /// </summary>
        public async Task<Agreement> GetCurrentAgreementAsync(int enrolleeId)
        {
            return await _context.Agreements
                .OrderByDescending(at => at.CreatedDate)
                .FirstAsync(at => at.EnrolleeId == enrolleeId);
        }

        /// <summary>
        /// Accepts the Enrollee's newest Agreement, if it hasn't already been accepted.
        /// </summary>
        public async Task AcceptCurrentEnrolleeAgreementAsync(int enrolleeId)
        {
            var agreement = await GetCurrentAgreementAsync(enrolleeId);

            if (agreement.AcceptedDate == null)
            {
                agreement.AcceptedDate = DateTimeOffset.Now;
                agreement.ExpiryDate = DateTimeOffset.Now.Add(AGREEMENT_EXPIRY);

                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Expires the Enrollee's most recently accepted Agreement.
        /// </summary>
        public async Task ExpireCurrentEnrolleeAgreementAsync(int enrolleeId)
        {
            var agreement = await _context.Agreements
                .OrderByDescending(a => a.CreatedDate)
                .Where(a => a.EnrolleeId == enrolleeId)
                .FirstOrDefaultAsync(a => a.AcceptedDate.HasValue);

            if (agreement != null)
            {
                agreement.ExpiryDate = DateTimeOffset.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AgreementViewModel>> GetOrgAgreementsAsync(int organizationId)
        {
            return await _context.Agreements
                .AsNoTracking()
                .OrderByDescending(a => a.CreatedDate)
                .Where(a => a.OrganizationId == organizationId)
                .ProjectTo<AgreementViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the Agreement + text of a given Org Agreement, optionally with the text in the form of a Base 64 encoded PDF.
        /// Returns null if the Agreement does not exist on the given organization.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="agreementId"></param>
        /// <param name="asEncodedPdf"></param>
        /// <returns></returns>
        public async Task<AgreementViewModel> GetOrgAgreementAsync(int organizationId, int agreementId, bool asEncodedPdf = false)
        {
            var agreementVm = await _context.Agreements
                .AsNoTracking()
                .Where(a => a.Id == agreementId && a.OrganizationId == organizationId)
                .ProjectTo<AgreementViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            if (agreementVm == null)
            {
                return null;
            }

            var orgName = await GetOrganizationName(organizationId);
            var html = await RenderOrgAgreementHtmlAsync(agreementVm.AgreementType, orgName, agreementVm.AcceptedDate, asEncodedPdf, false);

            if (asEncodedPdf)
            {
                var pdf = _pdfService.Generate(html);
                agreementVm.AgreementContent = Convert.ToBase64String(pdf);
            }
            else
            {
                agreementVm.AgreementContent = html;
            }

            return agreementVm;
        }

        public async Task<string> RenderOrgAgreementHtmlAsync(AgreementType type, string orgName, DateTimeOffset? acceptedDate, bool forPdf, bool withSignature)
        {
            var template = (type, forPdf) switch
            {
                (AgreementType.CommunityPharmacyOrgAgreement, false) => RazorTemplates.OrgAgreements.CommunityPharmacy,
                (AgreementType.CommunityPharmacyOrgAgreement, true) => RazorTemplates.OrgAgreements.CommunityPharmacyPdf,
                (AgreementType.CommunityPracticeOrgAgreement, false) => RazorTemplates.OrgAgreements.CommunityPractice,
                (AgreementType.CommunityPracticeOrgAgreement, true) => RazorTemplates.OrgAgreements.CommunityPracticePdf,
                _ => throw new ArgumentException($"Invalid AgreementType {type} in {nameof(RenderOrgAgreementHtmlAsync)}")
            };

            var displayDate = acceptedDate ?? DateTimeOffset.Now;
            // Converting to BC time here since we aren't localizing this time in the web client
            displayDate = displayDate.ToOffset(new TimeSpan(-7, 0, 0));

            return await _razorConverterService.RenderTemplateToStringAsync(template, new OrgAgreementRazorViewModel(orgName, displayDate, withSignature));
        }

        /// <summary>
        /// Returns a Base64 encoded PDF of a given org agreement for signing.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<string> GetSignableOrgAgreementAsync(int organizationId, AgreementType type)
        {
            var orgName = await GetOrganizationName(organizationId);
            var html = await RenderOrgAgreementHtmlAsync(type, orgName, DateTimeOffset.Now, true, true);
            var pdf = _pdfService.Generate(html);
            return Convert.ToBase64String(pdf);
        }

        public async Task<SignedAgreementDocument> GetSignedAgreementDocumentAsync(int agreementId)
        {
            return await _context.Agreements
                .Where(a => a.Id == agreementId)
                .Select(a => a.SignedAgreement)
                .SingleOrDefaultAsync();
        }

        public async Task<SignedAgreementDocument> AddSignedAgreementDocumentAsync(int agreementId, Guid documentGuid)
        {
            var filename = await _documentClient.FinalizeUploadAsync(documentGuid, DestinationFolders.SignedAgreements);
            if (string.IsNullOrWhiteSpace(filename))
            {
                return null;
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

        /// <summary>
        /// Renders the HTML text of the Agreement for viewing on the frontend.
        /// </summary>
        private async Task RenderHtml(params Agreement[] agreements)
        {
            foreach (var agreement in agreements)
            {
                if (agreement != null)
                {
                    agreement.AgreementContent = await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Agreements.Base, agreement);
                }
            }
        }

        private async Task<string> GetOrganizationName(int organizationId)
        {
            return await _context.Organizations
                .Where(o => o.Id == organizationId)
                .Select(o => o.Name)
                .SingleAsync();
        }

        public async Task<IEnumerable<AgreementVersionListViewModel>> GetLatestEnrolleeAgreementVersionsAsync()
        {
            var agreementVersionList = new List<AgreementVersion>();
            foreach (var type in AgreementTypeExtensions.EnrolleeAgreementTypes())
            {
                agreementVersionList.Add(await FetchNewestAgreementVersionOfTypeAsync(type));
            }
            return _mapper.Map<IEnumerable<AgreementVersionListViewModel>>(agreementVersionList);
        }

        public async Task<AgreementVersionViewModel> GetAgreementVersionById(int agreementId)
        {
            return await _context.AgreementVersions
               .AsNoTracking()
               .Where(av => av.Id == agreementId)
               .ProjectTo<AgreementVersionViewModel>(_mapper.ConfigurationProvider)
               .SingleOrDefaultAsync();
        }

        private async Task<AgreementVersion> FetchNewestAgreementVersionOfTypeAsync(AgreementType type)
        {
            return await _context.AgreementVersions
                .AsNoTracking()
                .Where(av => av.AgreementType == type)
                .OrderByDescending(av => av.EffectiveDate)
                .FirstOrDefaultAsync();
        }

        private async Task<int> FetchNewestAgreementVersionIdOfType(AgreementType type)
        {
            return await _context.AgreementVersions
                .AsNoTracking()
                .OrderByDescending(a => a.EffectiveDate)
                .Where(a => a.AgreementType == type)
                .Select(a => a.Id)
                .FirstAsync();
        }
    }
}
