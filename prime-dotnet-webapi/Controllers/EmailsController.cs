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

        public EmailsController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        // POST: api/emails/management/statuses
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
    }
}
