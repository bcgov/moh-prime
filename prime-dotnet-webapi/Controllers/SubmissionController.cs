using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Models;
using Prime.Services;
using Prime.Models.Api;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/enrollees")]
    [ApiController]
    // User needs at least the ADMIN or ENROLLEE role to use this controller
    [Authorize(Policy = PrimeConstants.USER_POLICY)]
    public class SubmissionController : ControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IAccessTermService _accessTermService;
        private readonly IEnrolleeProfileVersionService _enrolleeProfileVersionService;

        public SubmissionController(
            IEnrolleeService enrolleeService,
            IAccessTermService accessTermService,
            IEnrolleeProfileVersionService enrolleeProfileVersionService)
        {
            _enrolleeService = enrolleeService;
            _accessTermService = accessTermService;
            _enrolleeProfileVersionService = enrolleeProfileVersionService;
        }

        // GET: api/Enrollees
        /// <summary>
        /// Gets all of the enrollees for the user, or all enrollees if user has ADMIN role.
        /// </summary>
        [HttpGet(Name = nameof(GetEnrollees))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiOkResponse<IEnumerable<Enrollee>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Enrollee>>> GetEnrollees([FromQuery]EnrolleeSearchOptions searchOptions)
        {
            IEnumerable<Enrollee> enrollees = null;

            // User must have the ADMIN role to see all enrollees
            if (User.IsAdmin())
            {
                enrollees = await _enrolleeService.GetEnrolleesAsync(searchOptions);
            }
            else
            {
                var enrollee = await _enrolleeService.GetEnrolleeForUserIdAsync(User.GetPrimeUserId());
                enrollees = enrollee != null ? new[] { enrollee } : new Enrollee[0];
            }

            return Ok(new ApiOkResponse<IEnumerable<Enrollee>>(enrollees));
        }
    }
}
