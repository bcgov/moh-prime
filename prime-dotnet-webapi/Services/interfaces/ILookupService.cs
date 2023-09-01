using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models.Api;
using Prime.Models;

namespace Prime.Services
{
    public interface ILookupService
    {
        Task<LookupEntity> GetLookupsAsync();
        Task<int> GetCareSettingCountAsync();
        Task<List<SelfDeclarationVersion>> GetSelfDeclarationVersion(DateTimeOffset targetDate, bool isDeviceProvider);
    }
}
