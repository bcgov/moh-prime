using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IBusinessEventService
    {
        Task<BusinessEvent> CreateStatusChangeEventAsync(int enrolleeId, string description, int? adminId = null);
        Task<BusinessEvent> CreateEmailEventAsync(int enrolleeId, string description, int? adminId = null);
        Task<BusinessEvent> CreateNoteEventAsync(int enrolleeId, string description, int? adminId = null);
        Task<BusinessEvent> CreateAdminClaimEventAsync(int enrolleeId, string description, int? adminId = null);
    }
}
