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
using Prime.ViewModels;
using Prime.ViewModels.Labtech;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/parties/[controller]")]
    [ApiController]
    // User needs at least the READONLY ADMIN or ENROLLEE role to use this controller
    //[Authorize(Policy = Policies.User)]
    public class LabtechsController : ControllerBase
    {
        private readonly ILabtechService _labtechService;

        public LabtechsController(ILabtechService labtechService)
        {
            _labtechService = labtechService;
        }

        // POST: api/parties/labtechs
        /// <summary>
        /// Creates a new Enrollee.
        /// </summary>
        [HttpPost(Name = nameof(CreateLabtech))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateLabtech(LabtechCreateModel labtech)
        {
            await _labtechService.CreateLabtechAsync(labtech, User);
            return Created();
        }
    }
}
