using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Prime.Models;
using Prime.ViewModels;
using Prime.HttpClients;
using Prime.HttpClients.DocumentManagerApiDefinitions;

namespace Prime.Services
{
    public class AgreementService : BaseService, IAgreementService
    {
        private readonly IMapper _mapper;
        private readonly IDocumentManagerClient _documentClient;

        public AgreementService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            IDocumentManagerClient documentClient)
            : base(context, httpContext)
        {
            _mapper = mapper;
            _documentClient = documentClient;
        }


        public async Task<IEnumerable<AgreementVersionListViewModel>> GetLatestAgreementVersionsAsync(AgreementGroup? group)
        {
            // In EF 5 we should be able to do a GroupBy on AgreementVersion instead of the double select.
            return await _context.AgreementVersions
                .AsNoTracking()
                .Select(av => av.AgreementType)
                .Distinct()
                .If(group.HasValue, q => q.Where(at => group.Value.AgreementTypes().Contains(at)))
                .Select(at => _context.AgreementVersions
                    .Where(av => av.AgreementType == at)
                    .OrderByDescending(av => av.EffectiveDate)
                    .First())
                .ProjectTo<AgreementVersionListViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<AgreementVersionViewModel> GetAgreementVersionAsync(int agreementVersionId)
        {
            return await _context.AgreementVersions
               .AsNoTracking()
               .Where(av => av.Id == agreementVersionId)
               .ProjectTo<AgreementVersionViewModel>(_mapper.ConfigurationProvider)
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

        public async Task<SignedAgreementDocument> GetSignedAgreementDocumentAsync(int agreementId)
        {
            return await _context.Agreements
                .Where(a => a.Id == agreementId)
                .Select(a => a.SignedAgreement)
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
    }
}
