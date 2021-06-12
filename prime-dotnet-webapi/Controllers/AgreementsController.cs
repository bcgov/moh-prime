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
    public class AgreementsController : PrimeControllerBase
    {
        private readonly IAgreementService _agreementService;

        public AgreementsController(IAgreementService agreementService)
        {
            _agreementService = agreementService;
        }

        /// <summary>
        /// Get a list of the latest enrollee Agreement Versions
        /// </summary>
        [HttpGet("enrollee/latest", Name = nameof(GetLatestEnrolleeAgreementVersions))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<AgreementVersionListViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetLatestEnrolleeAgreementVersions()
        {
            var agreements = await _agreementService.GetLatestEnrolleeAgreementVersionsAsync();
            return Ok(agreements);
        }

        /// /api/agreements/2
        /// <summary>
        /// Get an enrollee Agreement Version by id
        /// </summary>
        /// <param name="agreementId"></param>
        [HttpGet("{agreementId}", Name = nameof(GetAgreementVersionById))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<AgreementVersionViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAgreementVersionById(int agreementId)
        {
            var agreement = await _agreementService.GetAgreementVersionById(agreementId);
            return Ok(agreement);
        }
    }
}
