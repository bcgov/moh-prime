using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IBusinessEventService
    {
        Task<BusinessEvent> CreateBusinessEventAsync(int enrolleeId, short eventTypeCode, string description, int? adminId = null);
    }
}
