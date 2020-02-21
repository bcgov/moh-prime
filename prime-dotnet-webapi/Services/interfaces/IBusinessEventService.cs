using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IBusinessEventService
    {
        Task CreateBusinessEventAsync(BusinessEvent businessEvent);
    }
}
