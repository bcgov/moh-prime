using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IEnrolmentCertificateService
    {
        Task<EnrolmentCertificate> GetEnrolmentCertificateAsync(Guid accessTokenId);

        Task<EnrolmentCertificateAccessToken> CreateCertificateAccessTokenWithCareSettingAsync(int enrolleeId, int careSetting, int? healthAuthorityCode, int[] remoteAccessSiteIds = null);

        Task<IEnumerable<EnrolmentCertificateAccessToken>> GetCertificateAccessTokensForUsernameAsync(string username);
    }
}
