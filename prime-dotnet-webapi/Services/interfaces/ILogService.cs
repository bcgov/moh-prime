using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface ILogService
    {
        Task CreateLogAsync(LogType logType, FrontEndLogViewModel log);
    }
}
