using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prime.Models;
using Prime.Models.Api;
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

        // POST: api/Admins
        /// <summary>
        /// Creates a new Admin.
        /// </summary>
        [HttpPost(Name = nameof(CreateAdmin))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<Admin>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResultResponse<Admin>), StatusCodes.Status201Created)]
        public async Task<ActionResult<Admin>> CreateAdmin(Admin admin)
        {
            if (admin == null)
            {
                this.ModelState.AddModelError("Admin", "Could not create an admin, the passed in Admin cannot be null.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            // Check to see if this userId is already an admin, if so, reject creating another
            if (await _adminService.AdminUserIdExistsAsync(admin.UserId))
            {
                return Ok(ApiResponse.Result(admin));
            }

            Guid PrimeUserId = User.GetPrimeUserId();
            if (PrimeUserId.Equals(Guid.Empty) || !PrimeUserId.Equals(admin.UserId))
            {
                this.ModelState.AddModelError("Admin", "Could not create an admin, permission denied.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            var createdAdminId = await _adminService.CreateAdminAsync(admin);

            return CreatedAtAction(
                nameof(GetAdminById),
                new { adminId = createdAdminId },
                ApiResponse.Result(admin)
            );
        }


        // GET: api/Admins/5
        /// <summary>
        /// Gets a specific Admin.
        /// </summary>
        /// <param name="adminId"></param>
        [HttpGet("{adminId}", Name = nameof(GetAdminById))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Admin>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Admin>> GetAdminById(int adminId)
        {
            var admin = await _adminService.GetAdminAsync(adminId);

            if (admin == null)
            {
                return NotFound(ApiResponse.Message($"Admin not found with id {adminId}"));
            }

            return Ok(ApiResponse.Result(admin));
        }
    }
}
