using AutoMapper;
using AutoMapper.QueryableExtensions;
using DelegateDecompiler.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

using Prime.Configuration.Auth;
using Prime.Configuration.Api;
using Prime.HttpClients;
using Prime.HttpClients.DocumentManagerApiDefinitions;
using Prime.Models;
using Prime.Models.Api;
using Prime.Models.VerifiableCredentials;
using Prime.ViewModels;

namespace Prime.Services
{
    public class DeviceProviderService : BaseService, IDeviceProviderService
    {
        private readonly IMapper _mapper;

        public DeviceProviderService(
            ApiDbContext context,
            ILogger<DeviceProviderService> logger,
            IMapper mapper)
        : base(context, logger)
        {
            _mapper = mapper;
        }

        public async Task<DeviceProviderSiteViewModel> GetDeviceProviderSiteAsync(string deviceProviderId)
        {
            var site = await _context.DeviceProviderSites
                .Where(s => s.DpId == deviceProviderId)
                .SingleOrDefaultAsync();
            return _mapper.Map<DeviceProviderSiteViewModel>(site);
        }

        public async Task<bool> CertificationNumberExist(string certificationNumber)
        {
            return await _context.OpcMembers.AsNoTracking()
                .AnyAsync(m => m.CertificationNumber.Contains(certificationNumber));
        }
    }
}
