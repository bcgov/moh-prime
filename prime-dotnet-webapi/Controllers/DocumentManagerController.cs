using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models.Api;
using Prime.HttpClients;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/document")]
    [ApiController]
    [Authorize(Policy = Policies.AnyUser)]
    public class DocumentManagerController : ControllerBase
    {
        private readonly IDocumentManagerClient _client;

        public DocumentManagerController(IDocumentManagerClient documentManagerClient)
        {
            _client = documentManagerClient;
        }

        // POST: api/Document
        /// <summary>
        /// Initialize uploading of a file with the Document Manager.
        /// </summary>
        [HttpPost(Name = nameof(InitializeFileUploadWithDocumentManager))]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult> InitializeFileUploadWithDocumentManager()
        {
            var metadata = Request.Headers["Upload-MetaData"].ToString();
            var filename = ParseFilenameFromMetadata(metadata);
            var fileSize = Request.Headers["Upload-Length"].ToString();

            var response = await _client.InitializeFileUploadAsync(filename, fileSize, "sites/businessLicences");

            HttpContext.Response.Headers.Add("Location", response.Headers.GetValues("Location").FirstOrDefault());
            return Ok();
        }

        // POST: api/Document/Business-Licence
        /// <summary>
        ///
        /// </summary>
        [HttpPost("Business-Licence", Name = nameof(InitializeBusinessLicenceUploadWithDocumentManager))]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult> InitializeBusinessLicenceUploadWithDocumentManager()
        {
            var metadata = Request.Headers["Upload-MetaData"].ToString();
            var filename = ParseFilenameFromMetadata(metadata);
            var fileSize = Request.Headers["Upload-Length"].ToString();

            var response = await _client.InitializeFileUploadAsync(filename, fileSize, "sites/business_licences");

            HttpContext.Response.Headers.Add("Location", response.Headers.GetValues("Location").FirstOrDefault());
            return Ok();
        }

        // POST: api/Document/Self-Declaration
        /// <summary>
        ///
        /// </summary>
        [HttpPost("Self-Declaration", Name = nameof(InitializeSelfDeclarationUploadWithDocumentManager))]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult> InitializeSelfDeclarationUploadWithDocumentManager()
        {
            var metadata = Request.Headers["Upload-MetaData"].ToString();
            var filename = ParseFilenameFromMetadata(metadata);
            var fileSize = Request.Headers["Upload-Length"].ToString();

            var response = await _client.InitializeFileUploadAsync(filename, fileSize, "self_declarations");

            HttpContext.Response.Headers.Add("Location", response.Headers.GetValues("Location").FirstOrDefault());
            return Ok();
        }

        private string ParseFilenameFromMetadata(string metadata)
        {
            // Upload-Metadata is in the form:
            // filename <Base64EncodedValue>,filetype <Base64EncodedValue>

            var dictionary = metadata.Split(",")
                .Select(x => x.Split(' '))
                .ToDictionary(x => x[0], x => x[1]);

            string value;
            if (!dictionary.TryGetValue("filename", out value))
            {
                throw new ArgumentException($"Could not parse Upload-Metadata [{metadata}], could not find the filename");
            }

            return Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }

        // GET: api/Document/1234-5678
        /// <summary>
        /// Returns a short lived single-use token to download a file from the document manager
        /// </summary>
        [HttpGet("{documentGuid}", Name = nameof(GetFileFromDocumentManager))]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetFileFromDocumentManager(Guid documentGuid)
        {
            var token = await _client.CreateDownloadTokenAsync(documentGuid);

            return Ok(ApiResponse.Result(token));
        }
    }
}
