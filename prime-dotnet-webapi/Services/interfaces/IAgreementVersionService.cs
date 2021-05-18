using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IAgreementVersionService
    {
        Task<IEnumerable<AgreementVersionViewModel>> GetLatestEnrolleeAgreementVersionsAsync();
    }
}
