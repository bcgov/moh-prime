using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Prime.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Prime.Models;

namespace Prime.Services
{
    public class IndividualDeviceProviderService : BaseService, IIndividualDeviceProviderService
    {
        private readonly IMapper _mapper;

        public IndividualDeviceProviderService(
            ApiDbContext context,
            ILogger<HealthAuthoritySiteService> logger,
            IMapper mapper
        ) : base(context, logger)
        {
            _mapper = mapper;
        }

        public async Task<IndividualDeviceProviderViewModel> CreateProviderAsync(int communitySiteId, IndividualDeviceProviderCreateOrUpdateModel createModel)
        {
            var newProvider = _mapper.Map<IndividualDeviceProvider>(createModel);
            newProvider.CommunitySiteId = communitySiteId;

            _context.IndividualDeviceProviders.Add(newProvider);

            await _context.SaveChangesAsync();

            return _mapper.Map<IndividualDeviceProviderViewModel>(newProvider);
        }

        public async Task<IEnumerable<IndividualDeviceProviderViewModel>> GetProvidersAsync(int communitySiteId)
        {
            return await _context.IndividualDeviceProviders
                .Where(p => p.CommunitySiteId == communitySiteId)
                .ProjectTo<IndividualDeviceProviderViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task RemoveProviderAsync(int providerId)
        {
            var provider = await _context.IndividualDeviceProviders
                .SingleOrDefaultAsync(b => b.Id == providerId);

            _context.IndividualDeviceProviders.Remove(provider);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProviderAsync(int providerId, IndividualDeviceProviderCreateOrUpdateModel updateModel)
        {
            var provider = await _context.IndividualDeviceProviders
                .SingleOrDefaultAsync(b => b.Id == providerId);

            _context.Entry(provider).CurrentValues.SetValues(updateModel);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ProviderExistsOnSiteAsync(int communitySiteId, int providerId)
        {
            return await _context.IndividualDeviceProviders.AnyAsync(p => p.Id == providerId && p.CommunitySiteId == communitySiteId);
        }
    }
}
