using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prime.Auth;
using Prime.Models.Api;
using Prime.Services;
using Prime.Services.Clients;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/document-access")]
    [ApiController]
    public class DocumentAccessController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IDocumentAccessTokenService _documentAccessTokenService;
        private readonly IDocumentManagerClient _documentManagerClient;


        public DocumentAccessController(
            IDocumentService documentService,
            IDocumentAccessTokenService documentAccessTokenService,
            IDocumentManagerClient documentManagerClient)
        {
            _documentService = documentService;
            _documentAccessTokenService = documentAccessTokenService;
            _documentManagerClient = documentManagerClient;
        }

        // GET: api/document-access/file-download/{accessTokenId}
        /// <summary>
        /// Gets the Document for downloading based on the supplied Document Access Token GUID.
        /// </summary>
        /// <remarks>This endpoint is not authenticated.</remarks>
        [HttpGet("file-download/{accessTokenId}", Name = nameof(GetDocumentByAccessToken))]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        // [ProducesResponseType(typeof(ApiResultResponse<File>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDocumentByAccessToken(Guid accessTokenId)
        {
            var documentAccessToken = await _documentAccessTokenService.GetDocumentAccessNoTrackingAsync(accessTokenId);

            if (documentAccessToken == null)
            {
                return NotFound(ApiResponse.Message($"No valid Document Access Token was found with id {accessTokenId}"));
            }

            var document = await _documentManagerClient.GetFileAsync(documentAccessToken.DocumentGuid);
            return File(document, "application/octet-stream");
        }

        // DELETE: api/document-access/{accessTokenId}
        /// <summary>
        /// Delete a Document Access Token.
        /// </summary>
        [HttpDelete("{accessTokenId}", Name = nameof(DeleteDocumentAccessToken))]
        [Authorize(Policy = AuthConstants.SUPER_ADMIN_POLICY)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDocumentAccessToken(Guid accessTokenId)
        {
            var documentAccessToken = await _documentAccessTokenService.GetDocumentAccessNoTrackingAsync(accessTokenId);

            if (documentAccessToken == null)
            {
                return NotFound(ApiResponse.Message($"No valid Document Access Token was found with id {accessTokenId}"));
            }

            await _documentAccessTokenService.DeleteDocumentAccessTokenAsync(accessTokenId);

            return NoContent();
        }
    }
}
