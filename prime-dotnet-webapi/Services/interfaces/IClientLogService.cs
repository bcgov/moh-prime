using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IClientLogService
    {
        Task CreateLogAsync(ClientLogViewModel log);
    }
}
