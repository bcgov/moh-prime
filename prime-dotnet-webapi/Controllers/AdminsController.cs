using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    // User needs the ADMIN role to use this controller
    [Authorize(Policy = PrimeConstants.ADMIN_POLICY)]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService;
        }
    }
}
