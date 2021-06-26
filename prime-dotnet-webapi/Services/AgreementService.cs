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
    }
}
