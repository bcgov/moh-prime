using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.HttpClients
{
    public interface IPrimeOdrClient
    {
        /// <summary>
        /// Calls external PRIME-ODR web service and retrieves latest logs as <c>Result</c>
        /// and <c>ExistsMoreLogs</c> (using C# 7.0 tuple syntax) indicates whether the PRIME-ODR web service indicated there are more logs that can be retrieved
        /// </summary>
        /// <param name="lastKnownTxId">Last known Pharmanet Transaction ID (note, not PRIME internal ID)</param>
        Task<(List<PharmanetTransactionLog> Logs, bool ExistsMoreLogs)> RetrieveLatestPharmanetTxLogsAsync(long lastKnownTxId);
    }
}
