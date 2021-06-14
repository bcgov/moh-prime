using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Services;
using Prime.HttpClients;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/document-access")]
    [ApiController]
    public class DocumentAccessController : PrimeControllerBase
    {
        private readonly IDocumentAccessTokenService _documentAccessTokenService;
        private readonly IDocumentManagerClient _documentManagerClient;

        public DocumentAccessController(
            IDocumentAccessTokenService documentAccessTokenService,
            IDocumentManagerClient documentManagerClient)
        {
            _documentAccessTokenService = documentAccessTokenService;
            _documentManagerClient = documentManagerClient;
        }

        // GET: api/document-access/{accessTokenId}
        /// <summary>
        /// Gets the Document for downloading based on the supplied Document Access Token GUID.
        /// </summary>
        /// <remarks>This endpoint is not authenticated.</remarks>
        [HttpGet("{accessTokenId}", Name = nameof(GetDocumentByAccessToken))]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDocumentByAccessToken(Guid accessTokenId)
        {
            var documentAccessToken = await _documentAccessTokenService.GetDocumentAccessTokenAsync(accessTokenId);
            if (documentAccessToken == null)
            {
                return NotFound();
            }

            var response = await _documentManagerClient.GetFileResponseAsync(documentAccessToken.DocumentGuid);
            if (response == null)
            {
                return NotFound();
            }

            Response.Headers.Add("Content-Disposition", response.Content.Headers.ContentDisposition.ToString());
            return File(await response.Content.ReadAsStreamAsync(), "application/octet-stream");
        }

        // DELETE: api/document-access/{accessTokenId}
        /// <summary>
        /// Delete a Document Access Token.
        /// </summary>
        [HttpDelete("{accessTokenId}", Name = nameof(DeleteDocumentAccessToken))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDocumentAccessToken(Guid accessTokenId)
        {
            var documentAccessToken = await _documentAccessTokenService.GetDocumentAccessTokenAsync(accessTokenId);

            if (documentAccessToken == null)
            {
                return NotFound($"No valid Document Access Token was found with id {accessTokenId}");
            }

            await _documentAccessTokenService.DeleteDocumentAccessTokenAsync(accessTokenId);

            return NoContent();
        }
    }
}
