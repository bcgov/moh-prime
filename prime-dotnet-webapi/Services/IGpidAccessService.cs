using System;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IGpidAccessService
    {
        Task<GpidAccessTicket> CreateGpidAccessTicketAsync(int enrolleeId);
        // Task<Guid?> CreateGpidAccessTicketAsync(Enrollee enrollee);
    }
}
