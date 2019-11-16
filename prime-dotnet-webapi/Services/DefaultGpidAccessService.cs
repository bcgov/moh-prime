using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class DefaultGpidAccessService : BaseService, IGpidAccessService
    {
        public DefaultGpidAccessService(
            ApiDbContext context, IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task<GpidAccessTicket> CreateGpidAccessTicketAsync(int enrolleeId)
        {
            GpidAccessTicket ticket = new GpidAccessTicket()
            {
                EnrolleeId = enrolleeId,
                ViewCount = 0,
                Active = true
            };

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create GPID access ticket.");
            }

            return ticket;
        }
    }
}
