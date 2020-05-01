using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IHealthbookService
    {
        Task PushBcscInfoAsync(Enrollee enrollee);

        Task PushGpidAsync(Enrollee enrollee);
    }
}
