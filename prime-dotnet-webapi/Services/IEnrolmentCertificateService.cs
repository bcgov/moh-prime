using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IEnrolmentCertificateService
    {
        Task<EnrolmentCertificate> GetEnrolmentCertificateAsync(Guid accessTokenId);
        
        Task<EnrolmentCertificateAccessToken> CreateCertificateAccessTokenAsync(Guid userId);
        Task<EnrolmentCertificateAccessToken> GetCertificateAccessTokenAsync(Guid accessTokenId);
        Task<IEnumerable<EnrolmentCertificateAccessToken>> GetCertificateAccessTokensForUserIdAsync(Guid userId);
    }
}
