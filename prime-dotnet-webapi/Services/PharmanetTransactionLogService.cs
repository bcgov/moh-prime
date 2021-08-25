using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Npgsql;
using Npgsql.Bulk;
using NpgsqlTypes;
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

            _logger.LogInformation("Preparing to save ...");

            var connString = System.Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            // See https://www.npgsql.org/doc/copy.html#binary-copy
            using (var writer = conn.BeginBinaryImport(
                @"COPY ""PharmanetTransactionLog""(""TransactionId"", ""TxDateTime"", ""UserId"", ""SourceIpAddress"", ""LocationIpAddress"", ""PharmacyId"", ""TransactionType"", ""TransactionSubType"", ""PractitionerId"", ""CollegePrefix"", ""TransactionOutcome"", ""ProviderSoftwareId"", ""ProviderSoftwareVersion"")
                FROM STDIN (FORMAT BINARY)"))
            {
                foreach (PharmanetTransactionLog log in logs)
                {
                    writer.StartRow();
                    writer.Write(log.TransactionId, NpgsqlDbType.Bigint);
                    writer.Write(log.TxDateTime, NpgsqlDbType.Timestamp);
                    writer.Write(log.UserId, NpgsqlDbType.Text);
                    writer.Write(log.SourceIpAddress, NpgsqlDbType.Text);
                    writer.Write(log.LocationIpAddress, NpgsqlDbType.Text);
                    writer.Write(log.PharmacyId, NpgsqlDbType.Text);
                    writer.Write(log.TransactionType, NpgsqlDbType.Text);
                    writer.Write(log.TransactionSubType, NpgsqlDbType.Text);
                    writer.Write(log.PractitionerId, NpgsqlDbType.Text);
                    writer.Write(log.CollegePrefix, NpgsqlDbType.Text);
                    writer.Write(log.TransactionOutcome, NpgsqlDbType.Text);
                    writer.Write(log.ProviderSoftwareId, NpgsqlDbType.Text);
                    writer.Write(log.ProviderSoftwareVersion, NpgsqlDbType.Text);
                }
                writer.Complete();
            }

            _logger.LogInformation("... save completed.");
            return logs.Last().TransactionId;
        }
    }
}
