using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/emails")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailsController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        // GET: api/emails/management/statuses
        /// <summary>
        /// Update all logged email statuses sent using the CHES email service
        /// </summary>
        [HttpGet("management/statuses", Name = nameof(UpdateEmailLogStatuses))]
        [Authorize(Roles = AuthConstants.PRIME_API_SERVICE_ACCOUNT_ROLE + "," + AuthConstants.ADMIN_POLICY)]
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
