using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class AdminService : BaseService, IAdminService
    {
        public AdminService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task<bool> AdminExistsAsync(int adminId)
        {
            return await _context.Admins
                .AnyAsync(a => a.Id == adminId);
        }

        public async Task<bool> AdminUserIdExistsAsync(Guid userId)
        {
            return await _context.Admins
                .AnyAsync(a => a.UserId == userId);
        }

        public Task<int?> CreateAdminAsync(Admin admin)
        {
            if (admin == null)
            {
                throw new ArgumentNullException(nameof(admin), "Could not create an admin, the passed in Admin cannot be null.");
            }

            return this.CreateAdminInternalAsync(admin);
        }

        private async Task<int?> CreateAdminInternalAsync(Admin admin)
        {
            _context.Admins.Add(admin);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create admin.");
            }

            return admin.Id;
        }

        public async Task DeleteAdminAsync(int adminId)
        {
            var admin = await _context.Admins
                .SingleOrDefaultAsync(a => a.Id == adminId);

            if (admin == null)
            {
                return;
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
        }

        public Task<Admin> GetAdminAsync(int adminId)
        {
            return _context.Admins
                .SingleOrDefaultAsync(a => a.Id == adminId);
        }

        public async Task<Admin> GetAdminForUserIdAsync(Guid userId)
        {
            return await _context.Admins
                .SingleOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<IEnumerable<Admin>> GetAdminsAsync()
        {
            return await _context.Admins
                .ToListAsync();
        }

        public async Task<int> UpdateAdminAsync(int adminId, Admin admin)
        {
            var _adminDb = await _context.Admins
                .AsNoTracking()
                .Where(a => a.Id == adminId)
                .SingleOrDefaultAsync();

            _context.Entry(admin).CurrentValues.SetValues(admin); // reflection

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }
    }
}
