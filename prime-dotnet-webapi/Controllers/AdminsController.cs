using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Configuration.Auth;
using Prime.Models;
using Prime.Services;
using Prime.ViewModels;

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
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<Admin>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResultResponse<Admin>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateAdmin(Admin admin)
        {
            if (admin == null)
            {
                return BadRequest("Could not create an admin, the passed in Admin cannot be null.");
            }
            if (!admin.PermissionsRecord().MatchesUsernameOf(User))
            {
                return Forbid();
            }

            // Check to see if this userId is already an admin, if so, reject creating another
            if (await _adminService.UsernameExistsAsync(admin.Username))
            {
                return Ok(admin);
            }

            var createdAdminId = await _adminService.CreateAdminAsync(admin);

            return CreatedAtAction(
                nameof(GetAdminByUserId),
                new { userId = admin.UserId },
                admin
            );
        }

        // GET: api/Admins
        /// <summary>
        /// Gets all the admins.
        /// </summary>
        [HttpGet(Name = nameof(GetAdmins))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<Admin>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAdmins()
        {
            var admins = await _adminService.GetAdminsAsync();
            return Ok(admins);
        }

        // GET: api/Admins/adminusers
        /// <summary>
        /// Gets all the admins.
        /// </summary>
        [HttpGet("adminusers", Name = nameof(GetAdminUserList))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<AdminUserViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAdminUserList()
        {
            var admins = await _adminService.GetAdminUserListAsync();
            return Ok(admins);
        }

        // GET: api/Admins/5c77e6ad-023f-4742-b95e-8beeb3052942
        /// <summary>
        /// Gets a specific Admin.
        /// </summary>
        /// <param name="userId"></param>
        [HttpGet("{userId}", Name = nameof(GetAdminByUserId))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Admin>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAdminByUserId(string userId)
        {
            var admin = await _adminService.GetAdminByUserIdAsync(userId);
            if (admin == null)
            {
                return NotFound($"Admin not found with user id {userId}");
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
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public ActionResult GetMetabaseEmbeddedString()
        {
            return Ok(_metabaseService.BuildMetabaseEmbeddedUrl());
        }

        // PUT: api/Admins/5/disable
        /// <summary>
        /// Disable admin user.
        /// </summary>
        /// <param name="adminId"></param>
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [HttpPut("{adminId}/disable", Name = nameof(DisableAdminById))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Admin>), StatusCodes.Status200OK)]
        public async Task<ActionResult> DisableAdminById(int adminId)
        {
            var admin = await _adminService.GetAdminAsync(adminId);
            if (admin == null)
            {
                return NotFound($"Admin not found with id {adminId}");
            }
            admin = await _adminService.SetAdminEnable(adminId, false);

            return Ok(admin);
        }

        // PUT: api/Admins/5/enable
        /// <summary>
        /// Enable admin user.
        /// </summary>
        /// <param name="adminId"></param>
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [HttpPut("{adminId}/enable", Name = nameof(EnableAdminById))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Admin>), StatusCodes.Status200OK)]
        public async Task<ActionResult> EnableAdminById(int adminId)
        {
            var admin = await _adminService.GetAdminAsync(adminId);
            if (admin == null)
            {
                return NotFound($"Admin not found with id {adminId}");
            }
            admin = await _adminService.SetAdminEnable(adminId, true);

            return Ok(admin);
        }
    }
}
