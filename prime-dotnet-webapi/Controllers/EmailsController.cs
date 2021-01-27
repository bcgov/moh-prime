using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IEmailDocumentsService _emailR;
        public EmailsController(
            IEmailService emailService,
            IEmailDocumentsService renderingService)
        {
            _emailR = renderingService;
            _emailService = emailService;
        }

        [HttpGet(Name = nameof(TEST))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<StatusCodeResult> TEST()
        {
            await _emailR.GenerateSiteRegistrationAttachmentsAsync(1);

            return NoContent();
        }

        // POST: api/Emails/management/statuses
        /// <summary>
        /// Update all logged email statuses sent using the CHES email service
        /// </summary>
        [HttpPost("management/statuses", Name = nameof(UpdateEmailLogStatuses))]
        [Authorize(Roles = Roles.PrimeApiServiceAccount + "," + Roles.PrimeAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<StatusCodeResult> UpdateEmailLogStatuses()
        {
            await _emailService.UpdateEmailLogStatuses();

            return NoContent();
        }

        // POST: api/Emails/management/enrollees/renewal
        /// <summary>
        /// Send enrollee renewal reminder emails
        /// </summary>
        [HttpPost("management/enrollees/renewal", Name = nameof(SendEnrolleeRenewalEmails))]
        [Authorize(Roles = Roles.PrimeApiServiceAccount + "," + Roles.PrimeAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SendEnrolleeRenewalEmails()
        {
            await _emailService.SendEnrolleeRenewalEmails();

            return NoContent();
        }
    }
}
