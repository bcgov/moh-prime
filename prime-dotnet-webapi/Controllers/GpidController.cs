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
    public class GpidController : ControllerBase
    {
        private readonly IEnrolmentService _enrolmentService;

        public GpidController(IEnrolmentService enrolmentService)
        {
            _enrolmentService = enrolmentService;
        }

        // POST: api/Gpid/Access
        /// <summary>
        /// Creates a GpidAccessTicket for the user if the user has a finished Enrolement.
        /// </summary>
        [HttpPost("/Access", Name = nameof(CreateGpidAccessTicket))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiOkResponse<GpidAccessTicket>), StatusCodes.Status201Created)]
        public async Task<ActionResult<GpidAccessTicket>> CreateGpidAccessTicket(Enrollee enrollee)
        {
            if (enrollee == null)
            {
                this.ModelState.AddModelError("Enrollee", "Could not create an access ticket, the passed in Enrollee cannot be null.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            var enrolment = await _enrolmentService.GetEnrolmentForUserIdAsync(enrollee.UserId);

            if (enrolment == null)
            {
                this.ModelState.AddModelError("Enrollee.UserId", "No enrolment exists for this User Id.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            // var createdEnrolmentId = await _enrolmentService.CreateEnrolmentAsync(enrolment);

            // return CreatedAtAction(nameof(GetEnrolmentById), new { enrolmentId = createdEnrolmentId }, new ApiCreatedResponse<Enrolment>(enrolment));
        }
    }
}
