using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IBusinessEventService
    {
        Task<BusinessEvent> CreateStatusChangeEventAsync(int enrolleeId, string description, int? adminId = null);
    }
}
