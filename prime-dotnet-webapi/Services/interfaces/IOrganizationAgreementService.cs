using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.Models.Api;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IOrganizationAgreementService
    {
        Task<IEnumerable<AgreementViewModel>> GetOrgAgreementsAsync(int organizationId);
        Task<AgreementViewModel> GetOrgAgreementAsync(int organizationId, int agreementId, bool asEncodedPdf = false);
        Task<string> RenderOrgAgreementHtmlAsync(AgreementType type, string orgName, DateTimeOffset? acceptedDate, bool forPdf, bool withSignature = false);
        Task<string> GetSignableOrgAgreementAsync(int organizationId, AgreementType type);
    }
}
