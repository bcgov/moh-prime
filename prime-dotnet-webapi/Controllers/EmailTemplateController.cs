using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels;
using Prime.ViewModels.Emails;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/email-templates")]
    [ApiController]
    public class EmailTemplateController : ControllerBase
    {
        private readonly IEmailTemplateService _emailTemplateService;

        public EmailTemplateController(IEmailTemplateService emailTemplateService)
        {
            _emailTemplateService = emailTemplateService;
        }

        // GET: api/email-templates
        /// <summary>
        /// Get email templates
        /// </summary>
        [HttpGet("", Name = nameof(GetEmailTemplates))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<EmailTemplateListViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEmailTemplates()
        {
            var templates = await _emailTemplateService.GetEmailTemplatesAsync();
            return Ok(ApiResponse.Result(templates));
        }

        // GET: api/email-templates/1
        /// <summary>
        /// Get email template by ID
        /// </summary>
        /// <param name="emailTemplateId"></param>
        [HttpGet("{emailTemplateId}", Name = nameof(GetEmailTemplate))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<EmailTemplateViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEmailTemplate(int emailTemplateId)
        {
            var template = await _emailTemplateService.GetEmailTemplateAsync(emailTemplateId);
            return Ok(ApiResponse.Result(template));
        }
    }
}
