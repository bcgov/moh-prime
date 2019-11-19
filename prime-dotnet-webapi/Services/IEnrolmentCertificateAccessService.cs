using System;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IEnrolmentCertificateAccessService
    {
        Task<EnrolmentCertificateAccessToken> CreateEnrolmentCertificateAccessTokenAsync(int enrolleeId);
        // Task<Guid?> CreateGpidAccessTicketAsync(Enrollee enrollee);
    }
}
