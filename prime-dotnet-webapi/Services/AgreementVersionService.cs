using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;
using Prime.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Prime.Services
{
    public class AgreementVersionService : BaseService, IAgreementVersionService
    {

        private readonly IMapper _mapper;
        public AgreementVersionService(
            ApiDbContext context,
            IMapper mapper,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<AgreementVersionViewModel>> GetLatestEnrolleeAgreementVersionsAsync()
        {
            var agreementVersionList = new List<AgreementVersion>();
            foreach (var type in AgreementTypeExtensions.EnrolleeAgreementTypes())
            {
                agreementVersionList.Add(await GetLatestEnrolleeAgreementVersionsByTypeAsync(type));
            }
            return agreementVersionList.AsQueryable()
            .ProjectTo<AgreementVersionViewModel>(_mapper.ConfigurationProvider)
            .ToList();
        }

        private async Task<AgreementVersion> GetLatestEnrolleeAgreementVersionsByTypeAsync(AgreementType type)
        {
            return await _context.AgreementVersions
            .Where(av => av.AgreementType == type)
            .OrderByDescending(av => av.EffectiveDate)
            .FirstOrDefaultAsync();
        }
    }
}
