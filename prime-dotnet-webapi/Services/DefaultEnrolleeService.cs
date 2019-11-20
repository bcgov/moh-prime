using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class DefaultEnrolleeService : BaseService, IEnrolleeService
    {
        public DefaultEnrolleeService(
            ApiDbContext context, IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task<IEnumerable<Enrollee>> GetEnrolleesAsync()
        {
            return await _context.Enrollees
                .Include(e => e.PhysicalAddress)
                .Include(e => e.MailingAddress)
                .ToListAsync();
        }

        public async Task<Enrollee> GetEnrolleeForUserIdAsync(Guid userId)
        {
            return await _context.Enrollees
                .Include(e => e.PhysicalAddress)
                .Include(e => e.MailingAddress)
                .Where(e => e.UserId == userId)
                .SingleOrDefaultAsync();
        }
    }
}
