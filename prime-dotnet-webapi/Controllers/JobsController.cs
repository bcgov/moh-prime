using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Configuration.Auth;
using Prime.Services;

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
        /// execute job to populate practitioner table
        /// - to query practitioner IDs for the unauthorized access from PharmaNet transaction log
        /// and store them in practitioner table.
        /// </summary>
        [HttpPost("populate/practitioner", Name = nameof(PopulatePractitionerTable))]
        [Authorize(Roles = Roles.PrimeApiServiceAccount)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PopulatePractitionerTable(DateTime? startDate = null, DateTime? endDate = null)
        {
            var result = await _reportingService.PopulatePractitionerTableAsync(startDate, endDate);
            return Ok(result);
        }

        // POST: api/jobs/update/practitioner
        /// <summary>
        /// execute job to update the contact information in practitioner table by calling PharmaNet API
        /// </summary>
        [HttpPost("update/practitioner", Name = nameof(UpdatePractitionerTable))]
        [Authorize(Roles = Roles.PrimeApiServiceAccount)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdatePractitionerTable()
        {
            var result = await _reportingService.UpdatePractitionerTableAsync();
            return Ok(result);
        }


        // POST: api/jobs/populate/transaction-log-temp
        /// <summary>
        /// execute job to update the contact information in practitioner table by calling PharmaNet API
        /// </summary>
        [HttpPost("populate/transaction-log-temp", Name = nameof(PopulateTransactionLogTemp))]
        [Authorize(Roles = Roles.PrimeApiServiceAccount)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PopulateTransactionLogTemp(int numberOfDays)
        {
            if (numberOfDays <= 0)
            {
                return Forbid();
            }

            var result = await _reportingService.PopulateTransactionLogTempAsync(numberOfDays);
            return Ok(result);
        }
    }
}
