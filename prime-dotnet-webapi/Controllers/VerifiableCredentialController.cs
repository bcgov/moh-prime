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
            // TODO how should this respond and what occurs on their side regarding retries?
            var response = await _verifiableCredentialsService.CreateConnection();
            return Ok(ApiResponse.Result(response));
        }

        // TODO add security to the endpoint by adding a param after /topic before param
        // POST: api/topic/:topic
        /// <summary>
        /// Handle webhook events sent from the issuing agent.
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="data"></param>
        // Webhooks explained "In Soviet Russia API call you!" - Jason Aitchison 2020
        // Reverse third-party API, which instead of pulling data from the third-party it pushes data
        [HttpPost("/api/webhooks/topic/{topic}", Name = nameof(Webhook))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Webhook(string topic, [FromBody] JObject data)
        {
            // TODO how should this respond and what occurs on their side regarding retries?
            await _verifiableCredentialsService.Webhook(data, topic);
            return NoContent();
        }
    }
}
