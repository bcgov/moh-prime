using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IAdminService
    {
        Task<bool> AdminExistsAsync(int adminId);

        Task<bool> UsernameExistsAsync(string username);

        Task<Admin> GetAdminByUserIdAsync(string userId);

        Task<Admin> GetAdminAsync(int id);

        Task<Admin> GetAdminAsync(string username);

        Task<string> GetAdminIdirAsync(int adminId);

        Task<IEnumerable<Admin>> GetAdminsAsync();

        Task<IEnumerable<AdminUserViewModel>> GetAdminUserListAsync();

        Task<int> CreateAdminAsync(Admin admin);

        Task<int> UpdateAdminAsync(int adminId, Admin admin);

        Task DeleteAdminAsync(int adminId);

        Task<Admin> SetAdminEnable(int adminId, bool enabled);
    }
}
