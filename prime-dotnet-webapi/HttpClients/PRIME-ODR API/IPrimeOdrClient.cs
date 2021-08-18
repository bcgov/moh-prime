using System.Threading.Tasks;

namespace Prime.HttpClients
{
    public interface IPrimeOdrClient
    {
        /// <summary>
        /// Calls external PRIME-ODR web service, retrieves latest logs and saves to PRIME database,
        /// then returns id of last transaction retrieved and saved to PRIME database.
        /// </summary>
        Task<long> RetrieveLatestPharmanetTxLogsAsync();
    }
}
