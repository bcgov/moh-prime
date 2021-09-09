using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public class ClientLogService : BaseService, IClientLogService
    {
        public ClientLogService(
            ApiDbContext context,
            ILogger<ClientLogService> logger)
            : base(context, logger)
        { }

        public async Task<int> CreateLogAsync(ClientLogViewModel log)
        {
            var newLog = new ClientLog
            {
                LogType = log.LogType,
                Message = log.Message,
                Data = log.Data,
                CreatedDate = DateTime.UtcNow
            };

            _context.ClientLogs.Add(newLog);

            await _context.SaveChangesAsync();

            return newLog.Id;
        }
    }
}
