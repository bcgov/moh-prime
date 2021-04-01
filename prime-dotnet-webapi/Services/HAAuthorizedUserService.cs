using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class HAAuthorizedUserService : BaseService, IHAAuthorizedUserService
    {
        public HAAuthorizedUserService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task<HAAuthorizedUser> CreateAuthorizedUserAsync(HealthAuthorityCode healthAuthorityCode, HAAuthorizedUser user)
        {
            user.HealthAuthorityCode = healthAuthorityCode;
            _context.HAAuthorizedUsers.Add(user);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create HA Authorized User.");
            }

            return user;
        }

        public async Task<HAAuthorizedUser> UpdateAuthorizedUserAsync(int authorizedUserId, HAAuthorizedUser updateModel)
        {
            var user = await _context.HAAuthorizedUsers
                .Where(u => u.Id == authorizedUserId)
                .SingleOrDefaultAsync();

            _context.Entry(user).CurrentValues.SetValues(updateModel); // reflection

            try
            {
                await _context.SaveChangesAsync();
                return user;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }

        public async Task RemoveAuthorizedUserAsync(HealthAuthorityCode healthAuthorityCode, int authorizedUserId)
        {
            var user = await _context.HAAuthorizedUsers
                .Where(u => u.HealthAuthorityCode == healthAuthorityCode)
                .Where(u => u.Id == authorizedUserId)
                .SingleOrDefaultAsync();

            if (user == null)
            {
                return;
            }

            _context.HAAuthorizedUsers.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HAAuthorizedUser>> GetAuthorizedUsersByHACodeAsync(HealthAuthorityCode healthAuthorityCode)
        {
            return await _context.HAAuthorizedUsers.Where(u => u.HealthAuthorityCode == healthAuthorityCode).ToListAsync();
        }

        public async Task<HAAuthorizedUser> GetAuthorizedUserByIdAsync(int authorizedUserId)
        {
            return await _context.HAAuthorizedUsers.Where(u => u.Id == authorizedUserId).SingleOrDefaultAsync();
        }

        public async Task<HAAuthorizedUser> GetAuthorizedUserByIdForHaAsync(HealthAuthorityCode healthAuthorityCode, int authorizedUserId)
        {
            return await _context.HAAuthorizedUsers
                .Where(u => u.HealthAuthorityCode == healthAuthorityCode)
                .Where(u => u.Id == authorizedUserId)
                .SingleOrDefaultAsync();
        }
    }
}
