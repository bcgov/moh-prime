using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Services;
using Prime.ViewModels;

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
        /// Get a list of the latest Agreement Versions
        /// </summary>
        [HttpGet(Name = nameof(GetLatestAgreementVersions))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<AgreementVersionListViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetLatestAgreementVersions([FromQuery] AgreementGroup? type)
        {
            var agreements = await _agreementService.GetLatestAgreementVersionsAsync(type);
            return Ok(agreements);
        }

        /// /api/agreements/2
        /// <summary>
        /// Get an Agreement Version by id
        /// </summary>
        /// <param name="agreementVersionId"></param>
        [HttpGet("{agreementVersionId}", Name = nameof(GetAgreementVersionById))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<AgreementVersionViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAgreementVersionById(int agreementVersionId)
        {
            var agreement = await _agreementService.GetAgreementVersionAsync(agreementVersionId);
            return Ok(agreement);
        }
    }
}
