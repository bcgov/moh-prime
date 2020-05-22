using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Prime.Models;
using Prime.Models.Api;
using Prime.Services;

using Prime.Auth;
using Prime.ViewModels;

using System.Net.Http;
using System.Net;
using System;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.Extensions.Primitives;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/document")]
    [ApiController]
    [AllowAnonymous]
    public class DocumentManagerController : ControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly HttpClient _client;

        public DocumentManagerController(IEnrolleeService enrolleeService)
        {
            _enrolleeService = enrolleeService;
            _client = new HttpClient();
        }

        // POST: api/Document
        /// <summary>
        ///
        /// </summary>
        [HttpPost(Name = nameof(InitializeFileUploadWithDocumentManager))]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult> InitializeFileUploadWithDocumentManager()
        {
            var metaData = Request.Headers["Upload-MetaData"].ToString();
            var parts = metaData.Split(",");
            var filename = parts[0].Split(" ")[1];
            var filetype = parts[1].Split(" ")[1];

            var auth = Request.Headers["Authorization"].ToString();

            var tusResumable = Request.Headers["Tus-Resumable"].ToString();
            var uploadLength = Request.Headers["Upload-Length"].ToString();

            var data = new Dictionary<string, string>
            {
                { "folder", "prime/site/businessLicence" },
                { "pretty_folder", "prime/site/businessLicence" },
                { "filename", filename },
            };

            var content = new FormUrlEncodedContent(data);

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(PrimeConstants.DOCUMENT_MANAGER_URL),
                Method = HttpMethod.Post,
                Content = content,
                Headers = {
                    {"Tus-Resumable", tusResumable},
                    {"Upload-Length", uploadLength},
                    {"Authorization", auth},
                }
            };
            var response = new HttpResponseMessage();
            try
            {
                response = await _client.SendAsync(request);

                foreach (KeyValuePair<string, IEnumerable<string>> entry in response.Headers)
                {
                    Response.Headers.Add(entry.Key, new StringValues(entry.Value.ToArray()));
                }
            }
            catch (HttpRequestException e)
            {
                throw new Exception("ERROR: " + e);
            }

            return Ok();
        }

    }
}
