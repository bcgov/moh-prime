using System.Threading.Tasks;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IClientLogService
    {
        Task<int> CreateLogAsync(ClientLogViewModel log);
    }
}
