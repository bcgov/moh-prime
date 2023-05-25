
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Configuration.Auth;
using Prime.Services;
using Prime.ViewModels.Emails;
using System.Collections.Generic;
using Prime.Models.Api;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : PrimeControllerBase
    {
        private readonly IReportingService _reportingService;
        public JobsController(
            IReportingService reportingService)
        {
            _reportingService = reportingService;
        }

        // POST: api/jobs/populate/practitioner
        /// <summary>
        /// execute job to update practitioner table
        /// </summary>
        [HttpPost("populate/practitioner", Name = nameof(PopulatePractitionerTable))]
        [Authorize(Roles = Roles.PrimeApiServiceAccount)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PopulatePractitionerTable()
        {
            await _reportingService.PopulatePractitionerTableAsync();
            return NoContent();
        }

        // POST: api/jobs/update/practitioner
        /// <summary>
        /// execute job to update practitioner table
        /// </summary>
        [HttpPost("update/practitioner", Name = nameof(UpdatePractitionerTable))]
        [Authorize(Roles = Roles.PrimeApiServiceAccount)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdatePractitionerTable()
        {
            await _reportingService.UpdatePractitionerTableAsync();
            return NoContent();
        }
    }
}
