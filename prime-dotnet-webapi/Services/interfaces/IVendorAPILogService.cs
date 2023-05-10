using System.Threading.Tasks;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IVendorAPILogService
    {
        Task<int> CreateLogAsync(string userId, string endPoint, string input);
        void UpdateLogAsync(int id, string output, string errorMessage = null);
    }
}
