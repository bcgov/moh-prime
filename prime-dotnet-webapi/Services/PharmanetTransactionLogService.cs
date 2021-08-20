using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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

        public Task<long> SaveLogs(ICollection<PharmanetTransactionLog> logs)
        {
            throw new System.NotImplementedException();
        }
    }
}
