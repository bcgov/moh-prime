using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prime.Auth;
using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/document-access")]
    [ApiController]
    public class DocumentAccessController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IDocumentAccessTokenService _documentAccessTokenService;


        public DocumentAccessController(
            IDocumentService documentService,
            IDocumentAccessTokenService documentAccessTokenService)
        {
            _documentService = documentService;
            _documentAccessTokenService = documentAccessTokenService;
        }

        // GET: api/document-access/file-download/{accessTokenId}
        /// <summary>
        /// Gets the Document for downloading based on the supplied Document Access Token GUID.
        /// </summary>
        /// <remarks>This endpoint is not authenticated.</remarks>
        [HttpGet("file-download/{accessTokenId}", Name = nameof(GetDownloadDocument))]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolmentCertificate>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolmentCertificate>> GetDownloadDocument(Guid accessTokenId)
        {
            var document = await _documentService.GetDocumentByAccessToken(accessTokenId);

            // TODO does this return null if not found?
            if (document == null)
            {
                return NotFound(ApiResponse.Message($"No valid Document Access Token was found with id {accessTokenId}"));
            }

            // TODO download the document

            return Ok(ApiResponse.Result(document));
        }

        // DELETE: api/document-access/file-download/{accessTokenId}
        /// <summary>
        /// Delete the Document for downloading based on the supplied Document Access Token GUID.
        /// </summary>
        /// <remarks>This endpoint is not authenticated.</remarks>
        [HttpDelete("file-download/{accessTokenId}", Name = nameof(DeleteDownloadDocument))]
        [Authorize(Policy = AuthConstants.SUPER_ADMIN_POLICY)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolmentCertificate>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolmentCertificate>> DeleteDownloadDocument(Guid accessTokenId)
        {
            if (accessTokenId == null)
            {

            }

            await _documentAccessTokenService.DeleteDocumentAccessToken(accessTokenId);

            // TODO does this return null if not found?
            if (document == null)
            {
                return NotFound(ApiResponse.Message($"No valid Document Access Token was found with id {accessTokenId}"));
            }

            // TODO download the document

            return Ok(ApiResponse.Result(document));
        }
    }
}
