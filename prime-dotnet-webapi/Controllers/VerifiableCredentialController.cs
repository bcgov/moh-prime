using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

using Prime.Models;
using Prime.Models.Api;
using Prime.Services;

// TODO how are we securing the endpoint?
namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class VerifiableCredentialController : ControllerBase
    {
        private readonly IVerifiableCredentialService _verifiableCredentialsService;

        public VerifiableCredentialController(
            IVerifiableCredentialService verifiableCredentialService
        )
        {
            _verifiableCredentialsService = verifiableCredentialService;
        }

        // POST: api/connections/create-invitation
        /// <summary>
        ///
        /// </summary>
        [HttpPost("/api/connections/create-connection", Name = nameof(CreateConnection))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<JObject>> CreateConnection()
        {
            var response = await _verifiableCredentialsService.CreateConnection();

            return Ok(ApiResponse.Result(response));
        }

        // POST: api/topic/:topic
        /// <summary>
        /// Handle webhook events sent from the issuing agent.
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="data"></param>
        // Webhooks explained "In Soviet Russia API call you!" - Jason Aitchison 2020
        [HttpPost("/api/webhooks/topic/{topic}", Name = nameof(Create))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Create(string topic, [FromBody] JObject data)
        {
            await _verifiableCredentialsService.Create(data, topic);

            return NoContent();
        }
    }
}
