using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using Prime.Models;
using Prime.ViewModels.SpecialAuthorityTransformation;

namespace Prime.Services
{
    public class SatEnrolmentService : BaseService, ISatEnrolmentService
    {
        private readonly IPartyService _partyService;

        private readonly IMapper _mapper;

        public SatEnrolmentService(
            ApiDbContext context,
            ILogger<SatEnrolmentService> logger,
            IPartyService partyService,
            IMapper mapper)
            : base(context, logger)
        {
            _partyService = partyService;

            _mapper = mapper;
        }

        public async Task<int> CreateOrUpdateEnrolleeAsync(SatEnrolleeDemographicChangeModel changeModel, ClaimsPrincipal user)
        {
            changeModel.ThrowIfNull(nameof(changeModel));

            var currentParty = await _partyService.GetPartyForUserIdAsync(user.GetPrimeUserId());
            if (currentParty == null)
            {
                currentParty = new Party
                {
                    Addresses = new List<PartyAddress>()
                };
                _context.Parties.Add(currentParty);
            }
            currentParty = changeModel.UpdateParty(currentParty, user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return InvalidId;
            }

            return currentParty.Id;
        }

        public async Task<Party> GetEnrolleeAsync(int satId)
        {
            return await _partyService.GetPartyAsync(satId);
        }

        public async Task UpdateDemographicsAsync(int satId, SatEnrolleeDemographicChangeModel viewModel, ClaimsPrincipal user)
        {
            var enrollee = await _partyService.GetPartyAsync(satId);
            viewModel.UpdateParty(enrollee, user);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCertificationsAsync(int satId, IEnumerable<SatEnrolleeCertificationViewModel> viewModels)
        {
            var newCerts = _mapper.Map<IEnumerable<PartyCertification>>(viewModels);

            var oldItems = await _context.PartyCertifications
                .Where(x => x.PartyId == satId)
                .ToListAsync();

            var itemList = newCerts.ToList();
            itemList.ForEach(x => x.PartyId = satId);

            _context.PartyCertifications.RemoveRange(oldItems);
            _context.PartyCertifications.AddRange(itemList);

            await _context.SaveChangesAsync();
        }
    }
}