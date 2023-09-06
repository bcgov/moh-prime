using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Configuration.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.HttpClients;
using Prime.HttpClients.PharmanetCollegeApiDefinitions;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsController : PrimeControllerBase
    {
        private readonly ILookupService _lookupService;
        private readonly ICollegeLicenceClient _collegeLicenceClient;

        public LookupsController(ILookupService lookupService, ICollegeLicenceClient collegeLicenceClient)
        {
            _lookupService = lookupService;
            _collegeLicenceClient = collegeLicenceClient;
        }

        //GET: /api/Lookups
        /// <summary>
        /// Gets all the lookup code values.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResultResponse<LookupEntity>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetLookups()
        {
            var lookupEntity = await _lookupService.GetLookupsAsync();

            return Ok(lookupEntity);
        }

        // Get /api/lookups/validate-licence
        /// <summary>
        /// For testing college licence validation
        /// </summary>
        [HttpGet("validate-licence", Name = nameof(LicenceCodeTest))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(typeof(ApiResultResponse<PharmanetCollegeRecord>), StatusCodes.Status200OK)]
        public async Task<ActionResult> LicenceCodeTest([FromQuery] string collegePrefix, [FromQuery] string licenceNumber)
        {
            var record = await _collegeLicenceClient.GetCollegeRecordAsync(collegePrefix, licenceNumber);

            return Ok(record);
        }

        // Get /api/lookups/self-declaration-question
        /// <summary>
        /// For self-declaration component
        /// </summary>
        [HttpGet("self-declaration-question", Name = nameof(SelfDeclarationQuestions))]
        [ProducesResponseType(typeof(ApiResultResponse<List<SelfDeclarationVersion>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> SelfDeclarationQuestions(DateTimeOffset targetDate, bool isDeviceProvider)
        {
            var record = await _lookupService.GetSelfDeclarationVersion(targetDate, isDeviceProvider);

            return Ok(record);
        }
    }
}
