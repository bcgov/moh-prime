using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Models;
using Prime.Services;
using Prime.Models.Api;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/enrollees")]
    [ApiController]
    // User needs at least the ADMIN or ENROLLEE role to use this controller
    [Authorize(Policy = PrimeConstants.USER_POLICY)]
    public class SubmissionsController : ControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IAccessTermService _accessTermService;
        private readonly IEnrolleeProfileVersionService _enrolleeProfileVersionService;

        public SubmissionsController(
            IEnrolleeService enrolleeService,
            IAccessTermService accessTermService,
            IEnrolleeProfileVersionService enrolleeProfileVersionService)
        {
            _enrolleeService = enrolleeService;
            _accessTermService = accessTermService;
            _enrolleeProfileVersionService = enrolleeProfileVersionService;
        }

        // POST: api/Enrollees/5/submit
        /// <summary>
        /// Performs a submission-related action on an Enrolle, such as submitting their profile for adjudication.
        /// </summary>
        [HttpPost("{enrolleeId}/{submissionAction:submissionAction}", Name = nameof(SumbissionAction))]
        [AllowAnonymous]
        public async Task<ActionResult<SubmissionAction>> SumbissionAction(int enrolleeId, SubmissionAction submissionAction)
        {


            return Ok(submissionAction);
        }
    }
}
