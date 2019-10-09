using System;
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
    //User needs at least the ADMIN or ENROLMENT role to use this controller
    //FIXME - add this back once there are OAuth tokens
    // [Authorize(Roles = PrimeConstants.PRIME_ADMIN_ROLE + "," + PrimeConstants.PRIME_ENROLMENT_ROLE)]
    public class EnrolmentsController : ControllerBase
    {
        private readonly IEnrolmentService _enrolmentService;

        public EnrolmentsController(IEnrolmentService enrolmentService)
        {
            _enrolmentService = enrolmentService;
        }

        // GET: api/Enrolment
        /// <summary>
        /// Gets all of the enrolments for the user, or all enrolments if user has ADMIN role.
        /// </summary>
        [HttpGet(Name = nameof(GetEnrolments))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Enrolment>>> GetEnrolments()
        {
            IEnumerable<Enrolment> enrolments = null;

            //User must have the ADMIN role to see all enrolments
            //FIXME - remove this 'always' true once there are OAuth tokens
            if (true || User.IsInRole(PrimeConstants.PRIME_ADMIN_ROLE))
            {
                enrolments = await _enrolmentService.GetEnrolmentsAsync();
            }
            else
            {
                enrolments = await _enrolmentService.GetEnrolmentsForUserIdAsync(
                                        PrimeUtils.PrimeUserId(User));
            }

            return enrolments.ToList();
        }

        // GET: api/Enrolment/5
        /// <summary>
        /// Gets a specific Enrolment.
        /// </summary>
        /// <param name="enrolmentId"></param> 
        [HttpGet("{enrolmentId}", Name = nameof(GetEnrolmentById))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Enrolment>> GetEnrolmentById(int enrolmentId)
        {
            var enrolment = await _enrolmentService.GetEnrolmentAsync(enrolmentId);

            if (enrolment == null)
            {
                return NotFound();
            }

            //if the user is not an ADMIN, make sure the enrolment matches the enrollee, otherwise return not authorized
            if (!BelongsToEnrollee(enrolment))
            {
                return Forbid();
            }

            return enrolment;
        }

        // POST: api/Enrolment
        /// <summary>
        /// Creates a new Enrolment.
        /// </summary>
        [HttpPost(Name = nameof(CreateEnrolment))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Enrolment>> CreateEnrolment(Enrolment enrolment)
        {
            var createdEnrolmentId = await _enrolmentService.CreateEnrolmentAsync(enrolment);

            return CreatedAtAction(nameof(GetEnrolmentById), new { enrolmentId = createdEnrolmentId }, enrolment);
        }

        // PUT: api/Enrolment/5
        /// <summary>
        /// Updates a specific Enrolment.
        /// </summary>
        /// <param name="enrolmentId"></param>
        /// <param name="enrolment"></param> 
        [HttpPut("{enrolmentId}", Name = nameof(UpdateEnrolment))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateEnrolment(int enrolmentId, Enrolment enrolment)
        {
            if (enrolmentId != enrolment.Id)
            {
                return BadRequest();
            }

            if (!_enrolmentService.EnrolmentExists(enrolmentId))
            {
                return NotFound();
            }

            //if the user is not an ADMIN, make sure the enrolleeId matches the user, otherwise return not authorized
            if (!BelongsToEnrollee(enrolment))
            {
                return Forbid();
            }

            await _enrolmentService.UpdateEnrolmentAsync(enrolment);

            return NoContent();
        }

        // DELETE: api/Enrolment/5
        /// <summary>
        /// Deletes a specific Enrolment.
        /// </summary>
        /// <param name="enrolmentId"></param> 
        [HttpDelete("{enrolmentId}", Name = nameof(DeleteEnrolment))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Enrolment>> DeleteEnrolment(int enrolmentId)
        {
            var enrolment = await _enrolmentService.GetEnrolmentAsync(enrolmentId);
            if (enrolment == null)
            {
                return NotFound();
            }

            //if the user is not an ADMIN, make sure the enrolleeId matches the user, otherwise return not authorized
            if (!BelongsToEnrollee(enrolment))
            {
                return Forbid();
            }

            await _enrolmentService.DeleteEnrolmentAsync(enrolmentId);

            return enrolment;
        }

        private bool BelongsToEnrollee(Enrolment enrolment)
        {
            bool belongsToEnrollee = false;

            belongsToEnrollee = User.IsInRole(PrimeConstants.PRIME_ADMIN_ROLE);

            //if user is not ADMIN, check that user belongs to the enrolment
            if (!belongsToEnrollee)
            {
                var PrimeUserId = PrimeUtils.PrimeUserId(User);
                belongsToEnrollee = PrimeUserId != null
                        && PrimeUserId.Equals(enrolment.Enrollee.UserId);
            }

            //FIXME - remove this once we have OAuth tokens with prime user id values
            belongsToEnrollee = true;

            return belongsToEnrollee;
        }
    }
}
