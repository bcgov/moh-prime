using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public class AdminService : BaseService, IAdminService
    {
        public AdminService(
            ApiDbContext context,
            ILogger<AdminService> logger)
            : base(context, logger)
        { }

        public async Task<bool> AdminExistsAsync(int adminId)
        {
            return await _context.Admins
                .AnyAsync(a => a.Id == adminId);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Admins
                .AnyAsync(a => a.Username == username);
        }

        public async Task<int> CreateAdminAsync(Admin admin)
        {
            admin.ThrowIfNull(nameof(admin));

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

        public Task<Admin> GetAdminAsync(int id)
        {
            return _context.Admins
                .SingleOrDefaultAsync(a => a.Id == id);
        }

        public Task<Admin> GetAdminByUserIdAsync(string userId)
        {
            return _context.Admins
                .SingleOrDefaultAsync(a => a.UserId == Guid.Parse(userId));
        }

        public async Task<Admin> GetAdminAsync(string username)
        {
            return await _context.Admins
                .SingleOrDefaultAsync(a => a.Username == username);
        }

        public async Task<string> GetAdminIdirAsync(int adminId)
        {
            return await _context.Admins
                .Where(a => a.Id == adminId)
                .Select(a => a.IDIR)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Admin>> GetAdminsAsync()
        {
            return await _context.Admins
                .ToListAsync();
        }

        public async Task<IEnumerable<AdminUserViewModel>> GetAdminUserListAsync()
        {

            return await _context.Admins.Select(a =>
                new AdminUserViewModel
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Email = a.Email,
                    Username = a.Username,
                    Status = a.Status,
                    SitesAssigned = a.Sites.Count(),
                    EnrolleesAssigned = a.Enrollees.Count()
                }
            ).OrderBy(a => a.FirstName).ToListAsync();
        }

        public async Task<int> UpdateAdminAsync(int adminId, Admin admin)
        {
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

        public async Task<Admin> SetAdminEnable(int adminId, bool enabled)
        {
            var admin = await _context.Admins
                .Where(admin => admin.Id == adminId)
                .Select(admin => admin)
                .SingleAsync();

            admin.Status = enabled ? AdminStatusType.Enabled : AdminStatusType.Disabled;

            await _context.SaveChangesAsync();

            return admin;
        }

    }
}
