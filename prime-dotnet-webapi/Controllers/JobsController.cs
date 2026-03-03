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
        private readonly IOrganizationService _organizationService;

        public JobsController(
            IReportingService reportingService,
            IOrganizationService organizationService)
        {
            _reportingService = reportingService;
            _organizationService = organizationService;
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
        /// copy transaction log to temp table for reporting.
        /// numberOfDays - the number of day of log to copy over. For example,set numberOfDays to 15 to copy last 15 days of log.
        /// it is expected to be executed daily, and it will automatically remove the older record before copying the new ones.
        /// * to decrease the number of day of log in temp table, just decrease the number in the next cycle and it will remove the unwanted log automatically
        /// * to increase the number of day of log in temp table, it requires to purge the temp table first then run the job again
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
                return BadRequest();
            }

            var result = await _reportingService.PopulateTransactionLogTempAsync(numberOfDays);
            return Ok(result);
        }

        // POST: api/jobs/populate/organization-registration-id
        /// <summary>
        /// execute job to update organization registration ID where the registration ID is missing, then return the number of organizations updated.
        /// </summary>
        [HttpPost("populate/organization-registration-id", Name = nameof(UpdateMissingRegistrationIds))]
        [Authorize(Roles = Roles.PrimeApiServiceAccount)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateMissingRegistrationIds()
        {
            var result = await _organizationService.UpdateMissingRegistrationIds();
            return Ok(result);
        }

        // POST: api/jobs/archive/transaction-log
        /// <summary>
        /// archive transaction log to archive table.
        /// numberOfDays - the number of day of log to archive over to the table. By default, numberOfDays is set to 1.
        /// </summary>
        [HttpPost("archive/transaction-log", Name = nameof(ArchiveTransactionLog))]

        [Authorize(Roles = Roles.PrimeApiServiceAccount)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> ArchiveTransactionLog(int numberOfDays = 1)
        {
            if (numberOfDays <= 0)
            {
                return BadRequest();
            }

            var result = await _reportingService.ArchiveTransactionLogAsync(numberOfDays);
            return Ok(result);
        }

        // GET: api/jobs/archive/transaction-log
        /// <summary>
        /// Return a string showing the date range of the transaction log in the archive table, return null if there is no record
        /// </summary>
        [HttpGet("archive/transaction-log", Name = nameof(GetArchiveTransactionLogString))]

        [Authorize(Roles = Roles.PrimeApiServiceAccount)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> GetArchiveTransactionLogString()
        {

            var result = await _reportingService.GetArchiveTransactionLogStringAsync();
            if (string.IsNullOrEmpty(result))
            {
                return NoContent();
            }
            return Ok(result);
        }

        // Delete: api/jobs/archive/transaction-log
        /// <summary>
        /// Clear transaction log archive table.
        /// </summary>
        [HttpDelete("archive/transaction-log", Name = nameof(ClearArchiveTransactionLog))]

        [Authorize(Roles = Roles.PrimeApiServiceAccount)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> ClearArchiveTransactionLog()
        {

            var result = await _reportingService.ClearTransactionLogArchiveAsync();

            return Ok(result);
        }
    }
}
