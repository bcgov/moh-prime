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
using System.Net.Http.Headers;

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
        public async Task<HttpResponseMessage> InitializeFileUploadWithDocumentManager()
        {
            var authBytes = "eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJmRW9CaFBWeERlSTI4ZGx2OWZTNWI4OHFWSWlibm83SXJpbkhJWTNlcXQwIn0.eyJqdGkiOiI0MWI3ZWZhMC1kYzZkLTQ0YzEtOGRiMy0wMmE0N2E0ZDUzYWMiLCJleHAiOjE1ODg2NzQ1OTksIm5iZiI6MCwiaWF0IjoxNTg4NjM4NjAwLCJpc3MiOiJodHRwczovL3Nzby1kZXYucGF0aGZpbmRlci5nb3YuYmMuY2EvYXV0aC9yZWFsbXMvdjRtYnFxYXMiLCJhdWQiOiJhY2NvdW50Iiwic3ViIjoiZjQzYmMwMDktMTg3MS00MWMwLWIzNmQtODBkZDNiYzViYTZkIiwidHlwIjoiQmVhcmVyIiwiYXpwIjoicHJpbWUtYXBwbGljYXRpb24tZGV2Iiwibm9uY2UiOiI5OWZiNDI0NS0xZGM0LTQ1NjktYjg4MS0wNGMwOTk0OWNmY2QiLCJhdXRoX3RpbWUiOjE1ODg2Mzg1OTksInNlc3Npb25fc3RhdGUiOiIwZDU1OWQyMS05MzQ4LTRiYWYtYjA5OC1lZWM5MmI0MmI0ZWYiLCJhY3IiOiIxIiwiYWxsb3dlZC1vcmlnaW5zIjpbIioiXSwicmVhbG1fYWNjZXNzIjp7InJvbGVzIjpbInByaW1lX3VzZXIiLCJvZmZsaW5lX2FjY2VzcyIsInVtYV9hdXRob3JpemF0aW9uIiwiZmVhdHVyZV9zaXRlX3JlZ2lzdHJhdGlvbiJdfSwicmVzb3VyY2VfYWNjZXNzIjp7ImFjY291bnQiOnsicm9sZXMiOlsibWFuYWdlLWFjY291bnQiLCJtYW5hZ2UtYWNjb3VudC1saW5rcyIsInZpZXctcHJvZmlsZSJdfX0sInNjb3BlIjoib3BlbmlkIHByb2ZpbGUgYWRkcmVzcyBlbWFpbCIsImF1ZCI6InByaW1lLXdlYi1hcGkiLCJpZGVudGl0eV9wcm92aWRlciI6ImJjc2MiLCJiaXJ0aGRhdGUiOiIyMDAwLTA2LTA3IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJhZGRyZXNzIjp7InN0cmVldF9hZGRyZXNzIjoiNzcyNCBQTEVBU0FOVCBDSVJDVVMiLCJsb2NhbGl0eSI6IkhJR0hMQU5EUyIsInJlZ2lvbiI6IkJDIiwicG9zdGFsX2NvZGUiOiJWOUIgNVk4IiwiY291bnRyeSI6IkNBIn0sIm5hbWUiOiJQUklNRVQgRUxFVkVOIiwiaWRlbnRpdHlfYXNzdXJhbmNlX2xldmVsIjozLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJidWlvM25hbGFzZXNkcmxmcW5rdmM0NG9iY2pzbms2YiIsImdpdmVuX25hbWVzIjoiUFJJTUVUIElkZWxsYSIsImdpdmVuX25hbWUiOiJQUklNRVQiLCJmYW1pbHlfbmFtZSI6IkVMRVZFTiJ9.EFIPTJuAtyHdmE8IcUGQCOHWnBXR_F__jPUlcKnXbawDEgiclLoarefpfU5whYH_3Ex0MJs3vJEjaxidfpBSSpzlx-LzYD2PzaHsb4N_RetP8NYiQYb3-RaqUUUqYPi40q10BnTEFHbRvwrGQvrTdE3VNL0ncgo3hAR6IcQPcZxqSaDQr8yHas2MWZ8IbrxP91DdE623Hti6w3oSpWFiGl_Q2YCOAxn06vcdoXapnsd1crfKQWIIGoPm4dhGKODDKfipqQeWp8fUH1-_vQ6L-7OH3p8gkgciVW8REDiIYqmh5LShpgVy3H6zuB8hOT_yU6C5phCI0RVaE2JzRiYAZA";
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authBytes);

            var metaData = Request.Headers["Upload-MetaData"].ToString();
            var parts = metaData.Split(",");
            var filename = parts[0].Split(" ")[1];
            var filetype = parts[1].Split(" ")[1];

            var tusResumable = Request.Headers["Tus-Resumable"].ToString();
            var uploadLength = Request.Headers["Upload-Length"].ToString();

            var data = new Dictionary<string, string>
            {
                { "folder", "prime/sites/businessLicence" },
                { "pretty_folder", "prime/sites/businessLicence" },
                { "filename", filename },
            };

            var content = new FormUrlEncodedContent(data);

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:6001/documents"),
                Method = HttpMethod.Post,
                Content = content,
                Headers = {
                    {"Tus-Resumable", tusResumable},
                    {"Upload-Length", uploadLength},
                }
            };

            var response = await _client.SendAsync(request);

            var result = new HttpResponseMessage
            {
                Content = response.Content,
                StatusCode = HttpStatusCode.Created,
            };
            foreach (KeyValuePair<string, IEnumerable<string>> entry in response.Headers)
            {
                result.Headers.Add(entry.Key, entry.Value);
            }
            return response;
        }
    }
}
