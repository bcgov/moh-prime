using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.Models;

namespace Prime.Services
{
    public interface IEnrolmentService
    {
        bool EnrolmentExists(int enrolmentId);

        Task<Enrolment> GetEnrolmentAsync(int enrolmentId);

        Task<Enrolment> GetEnrolmentForUserIdAsync(
            string userId);
            
        Task<IEnumerable<Enrolment>> GetEnrolmentsAsync();

        Task<IEnumerable<Enrolment>> GetEnrolmentsForUserIdAsync(
            string userId);

        Task<int?> CreateEnrolmentAsync(Enrolment enrolment);

        Task<int> UpdateEnrolmentAsync(Enrolment enrolment);

        Task DeleteEnrolmentAsync(int enrolmentId);

    }
}