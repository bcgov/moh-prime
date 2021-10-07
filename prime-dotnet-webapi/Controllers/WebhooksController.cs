using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prime.Models.VerifiableCredentials;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class WebhooksController : PrimeControllerBase
    {
        private readonly IVerifiableCredentialService _verifiableCredentialsService;

        public WebhooksController(IVerifiableCredentialService verifiableCredentialService)
        {
            _verifiableCredentialsService = verifiableCredentialService;
        }

        // POST: api/webhooks/1234-5678/topic/connections
        /// <summary>
        /// Handle webhook events sent from the issuing agent.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="topic"></param>
        /// <param name="data"></param>
        // Webhooks explained "In Soviet Russia API call you!" - Jason Aitchison 2020
        [HttpPost("{apiKey}/topic/{topic}", Name = nameof(Webhook))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        // TODO update to response code 202 when queue has been added for webhooks
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Webhook(string apiKey, string topic, [FromBody] WebhookData data)
        {
            if (apiKey != PrimeConfiguration.Current.VerifiableCredentialApi.WebhookKey)
            {
                return Forbid();
            }
            await _verifiableCredentialsService.WebhookAsync(data, topic);
            return NoContent();
        }
    }
}
