using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IPharmanetTransactionLogService
    {
        public long GetMostRecentTransactionId();

        /// <summary>
        /// Returns transaction ID of last log saved
        /// </summary>
        /// <param name="logs"></param>
        public Task<long> SaveLogsAsync(ICollection<PharmanetTransactionLog> logs);
    }
}
