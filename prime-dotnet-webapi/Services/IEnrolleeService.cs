using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.Models;

namespace Prime.Services
{
    public interface IEnrolleeService
    {
        Task<IEnumerable<Enrollee>> GetEnrolleesAsync();
        
        Task<IEnumerable<Enrollee>> GetEnrolleesForUserIdAsync(
            Guid userId);

    }
}