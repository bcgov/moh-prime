using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.ViewModels.Sites;

namespace Prime.Services
{
    public interface IExportService
    {
        /// <summary>
        /// Export remote users to CSV format
        /// </summary>
        Task<byte[]> ExportRemoteUsersToCSVAsync(int siteId);

        /// <summary>
        /// Export remote users to Excel format
        /// </summary>
        Task<byte[]> ExportRemoteUsersToExcelAsync(int siteId);
    }
}
