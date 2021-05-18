using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Services;
using Prime.Models.Api;
using Prime.ViewModels;
using Prime.Services.Razor;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AgreementVersionsController : ControllerBase
    {
        private readonly IAgreementVersionService _agreementVersionService;

        public AgreementVersionsController(IAgreementVersionService agreementVersionService)
        {
            _agreementVersionService = agreementVersionService;
        }

        /// <summary>
        /// Get a list of the latest enrollee Agreement Versions
        /// </summary>
        [HttpGet("enrollee/latest", Name = nameof(GetLatestEnrolleeAgreementVersions))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<AgreementVersionViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetLatestEnrolleeAgreementVersions()
        {
            var result = await _agreementVersionService.GetLatestEnrolleeAgreementVersionsAsync();
            return Ok(ApiResponse.Result(result));
        }
    }
}
