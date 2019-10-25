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
            IQueryable<Enrollee> query = _context.Enrollees
                .Include(e => e.PhysicalAddress)
                .Include(e => e.MailingAddress)
                ;

            var items = await query.ToListAsync();

            return items;
        }

        public async Task<IEnumerable<Enrollee>> GetEnrolleesForUserIdAsync(Guid userId)
        {
            IQueryable<Enrollee> query = _context.Enrollees
                .Include(e => e.PhysicalAddress)
                .Include(e => e.MailingAddress)
                .Where(e => e.UserId == userId)
                ;

            var items = await query.ToListAsync();

            return items;
        }
    }
}
