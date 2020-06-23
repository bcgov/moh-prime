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
        [HttpPost("/api/connections/create-invitation", Name = nameof(CreateInvitation))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<JObject>> CreateInvitation()
        {

            var response = await _verifiableCredentialsService.CreateInvitation();

            return Ok(ApiResponse.Result(response));
        }

        // POST: api/topic/:topic
        /// <summary>
        ///
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="data"></param>
        // Webhooks explained "In Soviet Russia API call you!" - Jason Aitchison 2020
        [HttpPost("/api/webhooks/topic/{topic}", Name = nameof(Create))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Create(WebhookTopic topic, [FromBody] Object data)
        {
            // TODO what are we going to get?
            // TODO how do we determine the topic?
            await _verifiableCredentialsService.create(data, topic);

            // TODO does there need to be a response?
            return NoContent();
        }
    }
}
