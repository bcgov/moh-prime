using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Newtonsoft.Json.Linq;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PHSAController : ControllerBase
    {
        private readonly IPHSAService _phsaService;

        public PHSAController(
            IPHSAService phsaService
        )
        {
            _phsaService = phsaService;
        }

        // POST: api/phsa
        /// <summary>
        /// Creates a phsa object and stores it in the DB
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResultResponse<int>), StatusCodes.Status201Created)]
        public async Task<ActionResult<List<PHSA>>> PostPHSA(FromBodyText body)
        {

            var result = await _phsaService.CreatePHSAAsync(body);

            return Ok(ApiResponse.Result(result));

        }

    }
}
