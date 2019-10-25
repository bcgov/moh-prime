using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Models;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    // User needs at least the ADMIN or ENROLMENT role to use this controller
    [Authorize(Policy = PrimeConstants.PRIME_USER_POLICY)]
    public class EnrolleesController : ControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;

        public EnrolleesController(IEnrolleeService enrolleeService)
        {
            _enrolleeService = enrolleeService;
        }

        // GET: api/Enrollees
        /// <summary>
        /// Gets all of the enrollee records for the user, or all enrollee records if user has ADMIN role.
        /// </summary>
        [HttpGet(Name = nameof(GetEnrollees))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiOkResponse<IEnumerable<Enrolment>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Enrolment>>> GetEnrollees()
        {
            IEnumerable<Enrollee> enrollees = null;

            // User must have the ADMIN role to see all enrollees
            if (User.IsInRole(PrimeConstants.PRIME_ADMIN_ROLE))
            {
                enrollees = await _enrolleeService.GetEnrolleesAsync();
            }
            else
            {
                enrollees = await _enrolleeService.GetEnrolleesForUserIdAsync(
                                        PrimeUtils.PrimeUserId(User));
            }

            return Ok(new ApiOkResponse<IEnumerable<Enrollee>>(enrollees.ToList()));
        }
    }
}
