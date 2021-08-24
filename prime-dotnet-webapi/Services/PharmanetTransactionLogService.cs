using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Npgsql.Bulk;
using Prime.Models;

namespace Prime.Services
{
    public class PharmanetTransactionLogService : BaseService, IPharmanetTransactionLogService
    {
        private readonly ILogger _logger;

        public PharmanetTransactionLogService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            ILogger<PlrProviderService> logger)
            : base(context, httpContext)
        {
            _logger = logger;
        }


        public long GetMostRecentTransactionId()
        {
            try
            {
                return _context.PharmanetTransactionLogs.Max(l => l.TransactionId);
            }
            catch (InvalidOperationException e)
            {
                // Likely the initial case of "Sequence contains no elements."
                _logger.LogError("Unable to obtain max transaction id", e);
                return 0;
            }
        }

        public async Task<long> SaveLogsAsync(ICollection<PharmanetTransactionLog> logs)
        {
            if (logs == null || logs.Count == 0)
            {
                return -1;
            }

            _logger.LogInformation("Adding to Context ...");

            // foreach (PharmanetTransactionLog log in logs)
            // {
            //     _context.PharmanetTransactionLogs.Add(log);
            // }
            // await _context.SaveChangesAsync();

            // Don't use injected DB Context
            ApiDbContext dbContext = new ApiDbContextFactory().CreateDbContext(new string[] { });

            var uploader = new NpgsqlBulkUploader(dbContext);
            await uploader.InsertAsync(logs);

            await dbContext.DisposeAsync();

            _logger.LogInformation("... Save Changes completed.");
            return logs.Last().TransactionId;
        }
    }
}
