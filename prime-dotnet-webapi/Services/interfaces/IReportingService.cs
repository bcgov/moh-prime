using System.Threading.Tasks;
using System;

namespace Prime.Services
{
    public interface IReportingService
    {
        Task PopulatePractitionerTableAsync(DateTime? startDate = null, DateTime? endDate = null);
        Task UpdatePractitionerTableAsync();
    }
}
