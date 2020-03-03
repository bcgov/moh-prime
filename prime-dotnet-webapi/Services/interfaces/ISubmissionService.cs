using System.Threading.Tasks;
using Prime.Models;
using Prime.Models.Api;

namespace Prime.Services
{
    public interface ISubmissionService
    {
        Task<Enrollee> PerformActionAsync(int enrolleeId, SubmissionAction action);

        Task UpdateAlwaysManualAsync(int enrolleeId, bool alwaysManual);
    }
}
