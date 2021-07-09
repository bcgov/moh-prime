using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public class LogService : BaseService, ILogService
    {
        public LogService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task CreateLogAsync(LogType logType, FrontEndLogViewModel log)
        {
            var newLog = new FrontEndLog
            {
                LogType = logType,
                Msg = log.Msg,
                Data = log.Data
            };
            _context.FrontEndLogs.Add(newLog);

            await _context.SaveChangesAsync();
        }
    }
}
