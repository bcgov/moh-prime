using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IAdminService
    {
        Task<bool> AdminExistsAsync(int adminId);

        Task<bool> UserIdExistsAsync(Guid userId);

        Task<Admin> GetAdminAsync(int adminId);

        Task<Admin> GetAdminAsync(Guid userId);

        Task<IEnumerable<Admin>> GetAdminsAsync();

        Task<int> CreateAdminAsync(Admin admin);

        Task<int> UpdateAdminAsync(int adminId, Admin admin);

        Task DeleteAdminAsync(int adminId);
    }
}
