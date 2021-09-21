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
using Prime.Models.Api;
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


        public async Task<IEnumerable<AgreementVersionListViewModel>> GetAgreementVersionsAsync(AgreementVersionFilters filters)
        {
            filters ??= new AgreementVersionFilters();

            var query = filters.Latest switch
            {
                true => _context.AgreementVersions
                    .Select(av => av.AgreementType) // In EF 5 we should be able to do a GroupBy on AgreementVersion instead of the double select.
                    .Distinct()
                    .If(filters.HasTypeFilter, q => q.Where(at => filters.FilteredTypes().Contains(at)))
                    .Select(at => _context.AgreementVersions
                        .Where(av => av.AgreementType == at)
                        .OrderByDescending(av => av.EffectiveDate)
                        .First()),

                false => _context.AgreementVersions
                    .If(filters.HasTypeFilter, q => q.Where(av => filters.FilteredTypes().Contains(av.AgreementType)))
            };

            return await query
                .AsNoTracking()
                .OrderBy(av => av.EffectiveDate)
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

        public async Task<string> CompareAgreementsAsync(AgreementCompareViewModel compare)
        {
            var results = await _context.AgreementVersions
                .AsNoTracking()
                .Where(av => av.Id == compare.InitialId)
                .Select(initial => new
                {
                    InitialText = initial.Text,
                    FinalText = _context.AgreementVersions
                        .AsNoTracking()
                        .Where(av => av.Id == compare.FinalId)
                        .Select(av => av.Text)
                        .Single()
                })
                .SingleAsync();

            var diff = new HtmlDiff.HtmlDiff(results.InitialText, results.FinalText)
            {
                IgnoreWhitespaceDifferences = true
            };

            return diff.Build();
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
