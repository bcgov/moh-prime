using System.Threading.Tasks;
using System;

namespace Prime.Services
{
    /// <summary>
    /// Reporting related functions or processes
    /// </summary>
    public interface IReportingService
    {
        /// <summary>
        /// Query to the unauthorized access practitioner ID from PharmaNet transaction log
        /// and store them into practitioner table
        /// </summary>
        Task<string> PopulatePractitionerTableAsync(DateTime? startDate = null, DateTime? endDate = null);
        /// <summary>
        /// Calling PharmaNet API to get the contact information and store back to practitioner table
        /// </summary>
        Task<string> UpdatePractitionerTableAsync();
        /// <summary>
        /// Copy number of days old records from PharmanetTransactionLog table to PharmanetTransactionLogTemp
        /// </summary>
        /// <returns></returns>
        Task<int> PopulateTransactionLogTempAsync(int numberInDays);
    }
}
