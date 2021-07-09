using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface ILogService
    {
        Task CreateLogAsync(LogType logType, string log);
    }
}
