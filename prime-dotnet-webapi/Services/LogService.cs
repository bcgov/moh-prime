using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prime.Models;

namespace Prime.Services
{
    public class LogService : BaseService, ILogService
    {
        public LogService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task CreateLogAsync(LogType logType, string log)
        {
            var newLog = new FrontEndLog
            {
                LogType = logType,
                Log = log
            };
            _context.FrontEndLogs.Add(newLog);

            await _context.SaveChangesAsync();
        }
    }
}
