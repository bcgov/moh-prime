using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Models;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //User needs at least the ADMIN or ENROLMENT role to use this controller
    //[Authorize(Roles = PrimeConstants.PRIME_ADMIN_ROLE + "," + PrimeConstants.PRIME_ENROLMENT_ROLE)]
    public class LookupsController : ControllerBase
    {
        private readonly ILookupService _lookupService;

        public LookupsController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        //GET: /api/Lookup
        /// <summary>
        /// Gets all the lookup code values.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<LookupEntity>> GetLookups()
        {
            LookupEntity lookupEntity = new LookupEntity();

            lookupEntity.Colleges = await _lookupService.GetLookupsAsync<College>();
            lookupEntity.JobNames = await _lookupService.GetLookupsAsync<JobName>();
            lookupEntity.Licenses = await _lookupService.GetLookupsAsync<License>();
            lookupEntity.OrganizationNames = await _lookupService.GetLookupsAsync<OrganizationName>();
            lookupEntity.OrganizationTypes = await _lookupService.GetLookupsAsync<OrganizationType>();
            lookupEntity.Practices = await _lookupService.GetLookupsAsync<Practice>();

            return lookupEntity;
        }
    }
}
