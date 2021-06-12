using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeAdministrant)]
    public class AdminsController : PrimeControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IMetabaseService _metabaseService;

        public AdminsController(IAdminService adminService, IMetabaseService metabaseService)
        {
            _adminService = adminService;
            _metabaseService = metabaseService;
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
                ModelState.AddModelError("Admin", "Could not create an admin, the passed in Admin cannot be null.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }
            if (!admin.PermissionsRecord().MatchesUserIdOf(User))
            {
                return Forbid();
            }

            // Check to see if this userId is already an admin, if so, reject creating another
            if (await _adminService.UserIdExistsAsync(admin.UserId))
            {
                return Ok(admin);
            }

            var createdAdminId = await _adminService.CreateAdminAsync(admin);

            return CreatedAtAction(
                nameof(GetAdminById),
                new { adminId = createdAdminId },
                admin
            );
        }

        // GET: api/Admins
        /// <summary>
        /// Gets all the admins.
        /// </summary>
        [HttpGet(Name = nameof(GetAdmins))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<Admin>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            var admins = await _adminService.GetAdminsAsync();
            return Ok(admins);
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
                return NotFound($"Admin not found with id {adminId}");
            }

            return Ok(admin);
        }

        // GET: api/Admins/embedded-metabase-url
        /// <summary>
        /// Returns a time sensitive metabase embedded url string
        /// </summary>
        [HttpGet("embedded-metabase-url", Name = nameof(GetMetabaseEmbeddedString))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<Admin>), StatusCodes.Status200OK)]
        public ActionResult<string> GetMetabaseEmbeddedString()
        {
            var token = _metabaseService.BuildMetabaseEmbeddedString();

            return Ok(token);
        }
    }
}
