using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class AgreementService : BaseService, IAgreementService
    {
        private readonly IDocumentManagerClient _documentClient;
        private readonly IMapper _mapper;

        public AgreementService(
            ApiDbContext context,
            ILogger<AgreementService> logger,
            IDocumentManagerClient documentClient,
            IMapper mapper)
            : base(context, logger)
        {
            _documentClient = documentClient;
            _mapper = mapper;
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

        public async Task<int> GetLatestAgreementVersionIdOfTypeAsync(AgreementType type)
        {
            return await _context.AgreementVersions
                .AsNoTracking()
                .OrderByDescending(a => a.EffectiveDate)
                .Where(a => a.AgreementType == type)
                .Select(a => a.Id)
                .FirstAsync();
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
    }
}
