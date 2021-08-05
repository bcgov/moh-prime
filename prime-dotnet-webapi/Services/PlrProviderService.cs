using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Prime.Models;
using Prime.ViewModels.Plr;
using Prime.Models.Plr;
using AutoMapper.QueryableExtensions;

namespace Prime.Services
{
    public class PlrProviderService : BaseService, IPlrProviderService
    {
        private readonly ILogger _logger;

        private readonly IMapper _mapper;


        public PlrProviderService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            ILogger<PlrProviderService> logger,
            IMapper mapper)
            : base(context, httpContext)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int> CreateOrUpdatePlrProviderAsync(PlrProvider dataObject, bool expectExists = false)
        {
            await TranslateIdentifierTypeAsync(dataObject);

            var existingPlrProvider = await _context.PlrProviders.SingleOrDefaultAsync(p => dataObject.Ipc == p.Ipc);

            if (existingPlrProvider == null)
            {
                _context.PlrProviders.Add(dataObject);
                if (expectExists)
                {
                    _logger.LogWarning("Expected PLR Provider with IPC of {ipc} to exist but it cannot be found", dataObject.Ipc);
                }
            }
            else
            {
                _mapper.Map(dataObject, existingPlrProvider);
                if (!expectExists)
                {
                    _logger.LogWarning("Did not expect PLR Provider with IPC of {ipc} to exist but it was found with ID of {id}", dataObject.Ipc, existingPlrProvider.Id);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError(e, "Error updating PLR Provider with with ID of {id}", existingPlrProvider.Id);
                return -1;
            }
            return existingPlrProvider == null ? dataObject.Id : existingPlrProvider.Id;
        }

        public async Task<IEnumerable<PlrViewModel>> GetPlrDataByCollegeIdAsync(IEnumerable<string> collegeId)
        {
            IQueryable<PlrRoleType> plrRoleTypes = _context.Set<PlrRoleType>();
            IQueryable<PlrStatusReason> plrStatusReasons = _context.Set<PlrStatusReason>();

            var plr = await _context.PlrProviders
                .AsNoTracking()
                .Where(p => collegeId.Contains(p.CollegeId))
                .ProjectTo<PlrViewModel>(_mapper.ConfigurationProvider, new { plrRoleTypes, plrStatusReasons })
                .ToListAsync();

            // PlrProvider's Expertise array does not play well with automapper ProjectTo, map manuully before return
            return plr.Select(p =>
                {
                    p.Expertise = string.Join(", ", _context.Set<PlrExpertise>().Where(e => p.ExpertiseCode.Contains(e.Code)).Select(e => e.Name));
                    return p;
                });
        }

        private async Task TranslateIdentifierTypeAsync(PlrProvider dataObject)
        {
            var identifierType = await _context.Set<IdentifierType>()
                .AsNoTracking()
                .SingleOrDefaultAsync(idType => idType.Code == dataObject.IdentifierType);

            if (identifierType != null)
            {
                // Translate from "2.16.840.1.113883.3.40.2.20" to "RNPID", for example
                dataObject.IdentifierType = identifierType.Name;
            }
            else
            {
                _logger.LogError("PLR Provider with IPC of {ipc} had an Identifier OID of {oid} that could not be translated", dataObject.Ipc, dataObject.IdentifierType);
            }
        }
    }
}
