using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public class ClientLogService : BaseService, IClientLogService
    {
        public ClientLogService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task CreateLogAsync(ClientLogViewModel log)
        {
            var newLog = new ClientLog
            {
                LogType = log.LogType,
                Msg = log.Message,
                Data = log.Data,
                CreatedDate = DateTime.Now
            };

            _context.FrontEndLogs.Add(newLog);

            await _context.SaveChangesAsync();
        }
    }
}
