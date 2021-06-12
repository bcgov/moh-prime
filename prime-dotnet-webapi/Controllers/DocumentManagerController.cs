using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Prime.Auth;
using Prime.Models.Api;
using Prime.HttpClients;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/document")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.PrimeAdministrant)]
    public class DocumentManagerController : PrimeControllerBase
    {
        private readonly IDocumentManagerClient _client;
        private readonly ILogger _logger;

        public DocumentManagerController(IDocumentManagerClient documentManagerClient, ILogger<DocumentManagerController> logger)
        {
            _client = documentManagerClient;
            _logger = logger;
        }

        // POST: api/Document
        /// <summary>
        /// Initialize uploading of a file with the Document Manager.
        /// </summary>
        [HttpPost(Name = nameof(InitializeFileUploadWithDocumentManager))]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult> InitializeFileUploadWithDocumentManager()
        {
            var fileSize = Request.Headers["Upload-Length"].ToString();
            var metadata = Request.Headers["Upload-MetaData"].ToString();
            var filename = ParseFilenameFromMetadata(metadata);

            var response = await _client.InitializeUploadAsync(filename, fileSize);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error when contacting the Document Manger. StatusCode: {response.StatusCode}, Body: {body}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when contacting the Document Manager");
            }

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

            if (!dictionary.TryGetValue("filename", out string value))
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

            return Ok(token);
        }
    }
}
