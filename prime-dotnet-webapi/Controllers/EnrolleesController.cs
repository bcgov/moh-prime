using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels;
using Prime.HttpClients.DocumentManagerApiDefinitions;
using Prime.ViewModels.Plr;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.ViewEnrollee)]
    public class EnrolleesController : PrimeControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IAdminService _adminService;
        private readonly IBusinessEventService _businessEventService;
        private readonly IEmailService _emailService;
        private readonly IDocumentService _documentService;
        private readonly IPlrProviderService _plrProviderService;

        public EnrolleesController(
            IEnrolleeService enrolleeService,
            IAdminService adminService,
            IBusinessEventService businessEventService,
            IEmailService emailService,
            IPlrProviderService plrProviderService,
            IDocumentService documentService)
        {
            _enrolleeService = enrolleeService;
            _adminService = adminService;
            _businessEventService = businessEventService;
            _emailService = emailService;
            _documentService = documentService;
            _plrProviderService = plrProviderService;
        }

        // GET: api/Enrollees
        /// <summary>
        /// Gets all of the enrollees for the user, or all enrollees if user has ADMIN role.
        /// </summary>
        [HttpGet(Name = nameof(GetEnrollees))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Enrollee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<EnrolleeListViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEnrollees([FromQuery] EnrolleeSearchOptions searchOptions)
        {
            if (User.IsAdministrant())
            {
                var notifiedIds = await _enrolleeService.GetNotifiedEnrolleeIdsForAdminAsync(User);
                var enrollees = await _enrolleeService.GetEnrolleesAsync(searchOptions);
                var result = enrollees.Select(e => e.SetNotification(notifiedIds.Contains(e.Id)));
                return Ok(result);
            }
            else
            {
                var enrollee = await _enrolleeService.GetEnrolleeForUserIdAsync(User.GetPrimeUserId());
                return Ok(enrollee == null ? Enumerable.Empty<Enrollee>() : new[] { enrollee });
            }
        }

        // POST: api/Enrollees
        /// <summary>
        /// Creates a new Enrollee.
        /// </summary>
        [HttpPost(Name = nameof(CreateEnrollee))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateEnrollee(EnrolleeCreatePayload payload)
        {
            if (payload?.Enrollee == null)
            {
                return BadRequest("Could not create an enrollee, the passed in Enrollee cannot be null.");
            }

            if (await _enrolleeService.UserIdExistsAsync(User.GetPrimeUserId()))
            {
                return BadRequest("An enrollee already exists for this User Id, only one enrollee is allowed per User Id.");
            }

            var createModel = payload.Enrollee;
            createModel.SetPropertiesFromToken(User);

            if (!createModel.Validate(User))
            {
                return BadRequest("One or more Properties did not match the information on the card.");
            }

            string filename = null;
            if (!createModel.IsBcServicesCard())
            {
                if (payload.IdentificationDocumentGuid.HasValue)
                {
                    filename = await _documentService.FinalizeDocumentUpload(payload.IdentificationDocumentGuid.Value, DestinationFolders.IdentificationDocuments);
                    if (string.IsNullOrWhiteSpace(filename))
                    {
                        return BadRequest("Identification document could not be created; network error or upload is already submitted");
                    }
                }
                else
                {
                    return BadRequest("Identification Document Guid was not supplied with request; Cannot create enrollee without identification.");
                }
            }

            var createdEnrolleeId = await _enrolleeService.CreateEnrolleeAsync(createModel);
            var enrollee = await _enrolleeService.GetEnrolleeAsync(createdEnrolleeId);

            if (filename != null)
            {
                await _enrolleeService.CreateIdentificationDocument(enrollee.Id, payload.IdentificationDocumentGuid.Value, filename);
            }

            return CreatedAtAction(
                nameof(GetEnrolleeById),
                new { enrolleeId = createdEnrolleeId },
                enrollee
            );
        }

        // GET: api/Enrollees/5
        /// <summary>
        /// Gets a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}", Name = nameof(GetEnrolleeById))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEnrolleeById(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId, User.IsAdministrant());
            if (enrollee == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }
            if (!enrollee.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            if (User.IsAdministrant())
            {
                await _businessEventService.CreateAdminViewEventAsync(enrolleeId, "Admin viewing the current Enrolment");
            }

            return Ok(enrollee);
        }

        // PUT: api/Enrollees/5
        /// <summary>
        /// Updates a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="enrollee"></param>
        /// <param name="beenThroughTheWizard"></param>
        [HttpPut("{enrolleeId}", Name = nameof(UpdateEnrollee))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateEnrollee(int enrolleeId, EnrolleeUpdateModel enrollee, [FromQuery] bool beenThroughTheWizard)
        {
            if (enrollee == null)
            {
                return BadRequest("Profile update model cannot be null.");
            }

            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }
            if (!record.MatchesUserIdOf(User))
            {
                return Forbid();
            }

            enrollee.SetPropertiesFromToken(User);

            if (!enrollee.Validate(User))
            {
                return BadRequest("One or more Properties did not match the information on the card.");
            }

            // If the enrollee is not in the status of 'Editable', it cannot be updated
            if (!await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, StatusType.Editable))
            {
                return BadRequest("Enrollee can not be updated when the current status is not 'Editable'.");
            }

            await _enrolleeService.UpdateEnrolleeAsync(enrolleeId, enrollee, beenThroughTheWizard);

            return NoContent();
        }

        // DELETE: api/Enrollees/5
        /// <summary>
        /// Deletes a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpDelete("{enrolleeId}", Name = nameof(DeleteEnrollee))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteEnrollee(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }

            await _enrolleeService.DeleteEnrolleeAsync(enrolleeId);

            return Ok(enrollee);
        }

        // GET: api/Enrollees/5/certifications
        /// <summary>
        /// Gets an Enrollee's Certifications.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/certifications", Name = nameof(GetCertifications))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<CertificationViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetCertifications(int enrolleeId)
        {
            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            return Ok(await _enrolleeService.GetCertificationsAsync(enrolleeId));
        }

        // GET: api/Enrollees/5/obo-sites
        /// <summary>
        /// Gets an Enrollee's Obo Sites.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/obo-sites", Name = nameof(GetOboSites))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<OboSiteViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetOboSites(int enrolleeId)
        {
            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            return Ok(await _enrolleeService.GetOboSitesAsync(enrolleeId));
        }
    }
}
