using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;

using Prime;
using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;

namespace PrimeTests.UnitTests
{
    public class AdminServiceTests : InMemoryDbTest
    {

    }
}

//     public interface IAdminService
//     {
//         Task<Admin> GetAdminForUserIdAsync(Guid userId);

//         Task<bool> AdminExistsAsync(int adminId);

//         Task<bool> AdminUserIdExistsAsync(Guid userId);

//         Task<Admin> GetAdminAsync(int adminId);

//         Task<IEnumerable<Admin>> GetAdminsAsync();

//         Task<int> CreateAdminAsync(Admin admin);

//         Task<int> UpdateAdminAsync(int adminId, Admin admin);

//         Task DeleteAdminAsync(int adminId);
//     }
