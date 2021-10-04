using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IMapper _mapper;

        public SatEnrolmentService(
            ApiDbContext context,
            ILogger<SatEnrolmentService> logger,
            IMapper mapper)
            : base(context, logger)
        {
            _mapper = mapper;
        }

        public async Task<Party> CreateEnrolleeAsync(SatEnrolleeDemographicViewModel viewModel)
        {
            viewModel.ThrowIfNull(nameof(viewModel));

            var party = _mapper.Map<Party>(viewModel);

            party.UserId = Guid.NewGuid();
            party.Addresses = new[]
            {
                new PartyAddress
                {
                    Address = _mapper.Map<PhysicalAddress>(viewModel.PhysicalAddress)
                }
            };

            _context.Parties.Add(party);
            await _context.SaveChangesAsync();

            return party;
        }

        public async Task<Party> GetEnrolleeAsync(int satId)
        {
            return await _context.Parties
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == satId);
        }

        public async Task UpdateDemographicsAsync(int satId, SatEnrolleeDemographicViewModel viewModel)
        {
            var enrollee = await _context.Parties
                .Include(p => p.Addresses)
                    .ThenInclude(a => a.Address)
                .SingleOrDefaultAsync(p => p.Id == satId);

            _mapper.Map(viewModel, enrollee);
            _mapper.Map(viewModel.PhysicalAddress, enrollee.PhysicalAddress);

            await _context.SaveChangesAsync();
        }

        // TODO: Do this or something more like `EnrolleePaperSubmissionService.ReplaceCollection`?
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