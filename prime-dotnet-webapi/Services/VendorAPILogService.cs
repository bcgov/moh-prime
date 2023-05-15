using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

using Prime.Models;

namespace Prime.Services
{
    public class VendorAPILogService : BaseService, IVendorAPILogService
    {
        public VendorAPILogService(
            ApiDbContext context,
            ILogger<VendorAPILogService> logger)
            : base(context, logger)
        { }

        public async Task<int> CreateLogAsync(string userId, string endPoint, string input)
        {
            var newLog = new VendorApiLog
            {
                UserId = userId,
                EndPoint = endPoint,
                Input = input,
            };

            _context.VendorApiLogs.Add(newLog);

            await _context.SaveChangesAsync();

            return newLog.Id;
        }

        public async Task UpdateLogAsync(int id, string output, string errorMessage = null)
        {
            var log = await _context.VendorApiLogs.SingleOrDefaultAsync(l => l.Id == id);

            log.Output = output;
            log.ErrorMessage = errorMessage;

            _context.VendorApiLogs.Update(log);

            await _context.SaveChangesAsync();
        }
    }
}
