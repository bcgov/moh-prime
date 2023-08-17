using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Prime.Models;
using Prime.Models.Api;
using Prime.Models.VerifiableCredentials;
using Prime.ViewModels;

namespace Prime.Services{

    public interface IDeviceProviderService
    {
        Task<DeviceProviderSiteViewModel> GetDeviceProviderSiteAsync(string deviceProviderId);
        Task<bool> CertificationNumberExist(string certificationNumber);
    }
}
