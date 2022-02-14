using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class HealthcheckController : PrimeControllerBase
    {
        private readonly ILookupService _lookupService;

        public HealthcheckController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        // GET: api/Healthcheck
        /// <summary>
        /// Does a healthcheck that queries the care setting lookup table to wake up the database
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task GetHealthcheck()
        {
            await _lookupService.GetCareSettingCountAsync();
        }
    }
}
