using System;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

using Prime.Configuration.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.HttpClients.Mail;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/provisioner-access")]
    [ApiController]
    public class ProvisionerAccessController : PrimeControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEnrolleeService _enrolleeService;
        private readonly IEnrolmentCertificateService _certificateService;
        private readonly IEmailService _emailService;
        private readonly IBusinessEventService _businessEventService;
        private readonly IVendorAPILogService _vendorAPILogService;

        private const int _hpdidLimit_GetUpdatedGpids = 1000;
        private const int _hpdidLimit_HpdidLookup = 10;

        public ProvisionerAccessController(
            IEnrolleeService enrolleeService,
            IEnrolmentCertificateService enrolmentCertificateService,
            IEmailService emailService,
            IBusinessEventService businessEventService,
            IVendorAPILogService vendorAPILogService,
            IMapper mapper)
        {
            _enrolleeService = enrolleeService;
            _certificateService = enrolmentCertificateService;
            _emailService = emailService;
            _businessEventService = businessEventService;
            _vendorAPILogService = vendorAPILogService;
            _mapper = mapper;
        }

        // GET: api/provisioner-access/certificate/{guid}
        /// <summary>
        /// Gets the Enrolment Certificate based on the supplied Access Token GUID. This endpoint is not authenticated.
        /// </summary>
        [HttpGet("certificate/{accessTokenId}", Name = nameof(GetEnrolmentCertificate))]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolmentCertificate>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEnrolmentCertificate(Guid accessTokenId)
        {
            var certificate = await _certificateService.GetEnrolmentCertificateAsync(accessTokenId);

            if (certificate == null)
            {
                return NotFound($"No valid Enrolment Certificate Access Token found with id {accessTokenId}");
            }

            //set health authority
            if (certificate.CareSettings.Any(cs => cs.Code == (int)CareSettingType.HealthAuthority) &&
                certificate.HealthAuthories != null && certificate.HealthAuthories.Count() > 0)
            {
                var careSetting = certificate.CareSettings.First(cs => cs.Code == (int)CareSettingType.HealthAuthority);
                careSetting.Name += $" - {string.Join(", ", certificate.HealthAuthories.Select(ha => ha.Name))}";
            }

            return Ok(certificate);
        }

        // GET: api/provisioner-access/token
        /// <summary>
        /// Gets all of the access tokens for the user.
        /// </summary>
        [HttpGet("token", Name = nameof(GetAccessTokens))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<EnrolmentCertificateAccessToken>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAccessTokens()
        {
            var tokens = await _certificateService.GetCertificateAccessTokensForUsernameAsync(User.GetPrimeUsername());

            return Ok(tokens);
        }

        // POST: api/enrollees/5/provisioner-access/send-link/1
        /// <summary>
        /// Creates an EnrolmentCertificateAccessToken for the user if the user has a finished enrolment,
        /// then sends the link to a recipient by email based on Care Setting Code.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="providedEmails"></param>
        [HttpPost("/api/enrollees/{enrolleeId}/provisioner-access/send-link", Name = nameof(SendProvisionerLink))]
        [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.TriageEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolmentCertificateAccessToken>), StatusCodes.Status201Created)]
        public async Task<ActionResult> SendProvisionerLink(int enrolleeId, [FromBody] EmailsForCareSetting[] providedEmails)
        {
            var allEmailsValid = true;
            foreach (var emailPair in providedEmails)
            {
                allEmailsValid = allEmailsValid && emailPair.Emails.All(ee => Email.IsValidEmail(ee.Email));
            }

            if (!allEmailsValid)
            {
                return BadRequest("The email(s) provided are not valid.");
            }

            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
            if (enrollee == null)
            {
                return NotFound("No enrollee exists for this User Id.");
            }
            if (!enrollee.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }
            if (enrollee.ExpiryDate == null)
            {
                return BadRequest("The enrollee for this User Id is not in a finished state.");
            }
            if (!enrollee.CurrentStatus.IsType(StatusType.Editable))
            {
                return BadRequest("The enrollee for this User Id is not in an editable state.");
            }

            EnrolmentCertificateAccessToken createdToken = null;
            foreach (var emailPair in providedEmails)
            {

                if (emailPair.CareSettingCode == (int)CareSettingType.CommunityPractice)
                {
                    foreach (var email in emailPair.Emails)
                    {
                        createdToken = await _certificateService.CreateCertificateAccessTokenWithCareSettingAsync(enrolleeId, emailPair.CareSettingCode, null, [.. email.RemoteAccessSiteIds]);
                        await _emailService.SendProvisionerLinkAsync([email.Email], createdToken, emailPair.CareSettingCode);
                        await _businessEventService.CreateEmailEventAsync(enrolleeId, $"Provisioner link sent to email: {email.Email}");
                    }
                }
                else
                {
                    var emails = emailPair.Emails.Select(e => e.Email).ToArray();
                    createdToken = await _certificateService.CreateCertificateAccessTokenWithCareSettingAsync(enrolleeId, emailPair.CareSettingCode, emailPair.HealthAuthorityCode);
                    await _emailService.SendProvisionerLinkAsync(emails, createdToken, emailPair.CareSettingCode);
                    await _businessEventService.CreateEmailEventAsync(enrolleeId, $"Provisioner link sent to email(s): {string.Join(",", emails)}");
                }

            }

            return CreatedAtAction(
                nameof(GetEnrolmentCertificate),
                new { accessTokenId = createdToken.Id },
                createdToken
            );
        }

        // GET: api/provisioner-access/gpid
        /// <summary>
        /// Gets the GPID for the user. Only a valid token is required, no role is required.
        /// </summary>
        [HttpGet("gpid", Name = nameof(GetGpid))]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetGpid()
        {
            return Ok(await _enrolleeService.GetActiveGpidAsync(User.GetPrimeUsername()));
        }

        // GET: api/provisioner-access/gpid-detail
        /// <summary>
        /// Gets the GPID and detail info for the user. Only a valid token is required, no role is required.
        /// </summary>
        [HttpGet("gpid-detail", Name = nameof(GetGpidDetail))]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<GpidDetailLookup>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetGpidDetail()
        {
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            JwtPayload jwtPayload = new JwtSecurityToken(accessToken).Payload;
            string authorizedParty = jwtPayload.Azp;
            var logId = await _vendorAPILogService.CreateLogAsync(authorizedParty, Request.Path.Value, null);

            var result = new GpidDetailLookup();
            var enrollee = await _enrolleeService.GetActiveGpidDetailAsync(User.GetPrimeUsername());
            if (enrollee != null)
            {
                _mapper.Map(enrollee, result);
                await _vendorAPILogService.UpdateLogAsync(logId, SerializeObjectForLog(result));
                var enrolleeStub = await _enrolleeService.GetEnrolleeStubAsync(User.GetPrimeUsername());
                await _businessEventService.CreateEnrolleeEventAsync(enrolleeStub.Id,
                    $"\"First-Time Provisioning API\" (aka GetGpidDetail) returned data to calling entity {TranslateAuthorizedParty(authorizedParty)}");
                return Ok(result);
            }

            return Ok(enrollee);
        }

        // POST: api/provisioner-access/gpid-lookup
        /// <summary>
        /// Gets the enrollee licence information by providing GPID, Firstname, Lastname and Care Setting code.
        /// However, the enrollee must have given consent to share the licence information in PRIME.
        /// </summary>
        [HttpPost("gpid-lookup", Name = nameof(GpidLookup))]
        [Authorize(Roles = Roles.ExternalGpidAccess)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeLookup>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GpidLookup(GpidLookupOptions option)
        {
            var logId = await _vendorAPILogService.CreateLogAsync(User.GetPrimeUsername(), Request.Path.Value, SerializeObjectForLog(option));
            if (option.Gpid == null || option.FirstName == null || option.LastName == null || option.CareSetting == null)
            {
                var errorMessage = $"Missing input information: Gpid={option.Gpid}, firstname={option.FirstName}, lastName={option.LastName}, careSettingCode={option.CareSetting}.";
                await _vendorAPILogService.UpdateLogAsync(logId, null, errorMessage);
                return BadRequest(errorMessage);
            }
            else
            {
                var result = await _enrolleeService.GpidLookupAsync(option);
                await _vendorAPILogService.UpdateLogAsync(logId, SerializeObjectForLog(result));
                return Ok(result);
            }
        }

        // GET: api/provisioner-access/gpids?hpdids=11111&hpdids=22222
        /// <summary>
        /// Gets the GPID and renewal date for the user(s) with the provided HPDIDs (if they exist). Requires a valid direct access grant token.
        /// </summary>
        [HttpGet("gpids", Name = nameof(HpdidLookup))]
        [Authorize(Roles = Roles.ExternalHpdidAccess)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<HpdidLookup>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> HpdidLookup([FromQuery] string[] hpdids)
        {
            var logId = await _vendorAPILogService.CreateLogAsync(User.GetPrimeUsername(), Request.Path.Value, SerializeObjectForLog(hpdids));
            if (hpdids != null && hpdids.Length > _hpdidLimit_HpdidLookup)
            {
                var errorMessage = $"number of {nameof(hpdids)} should not exceed {_hpdidLimit_HpdidLookup}";
                await _vendorAPILogService.UpdateLogAsync(logId, null, errorMessage);
                return BadRequest(errorMessage);
            }
            else
            {
                var result = await _enrolleeService.HpdidLookupAsync(hpdids);
                await _vendorAPILogService.UpdateLogAsync(logId, SerializeObjectForLog(result));
                return Ok(result);
            }
        }

        // POST: api/provisioner-access/updated-gpids
        /// <summary>
        /// Returns all the HPDIDs from the given list of HPDIDs that have an AcceptedDate since the given date/time.
        /// Requires a valid direct access grant token.  Input parameters should be passed in request body, x-www-form-urlencoded.
        /// HTTP POST rather than GET due to potentially large number of HPDIDs and to be compatible with most HTTP clients.
        /// </summary>
        [HttpPost("updated-gpids", Name = nameof(GetUpdatedGpids))]
        [Authorize(Roles = Roles.ExternalHpdidAccess)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<string>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetUpdatedGpids([FromForm] string[] hpdids, [FromForm] DateTimeOffset updatedSince)
        {
            if (DateTimeOffset.MinValue.Equals(updatedSince))
            {
                return BadRequest($"{nameof(updatedSince)} parameter is required");
            }
            else if (hpdids != null && hpdids.Length > _hpdidLimit_GetUpdatedGpids)
            {
                return BadRequest($"number of {nameof(hpdids)} should not exceed {_hpdidLimit_GetUpdatedGpids}");
            }
            else
            {
                var result = await _enrolleeService.FilterToUpdatedAsync(hpdids, updatedSince);

                return Ok(result);
            }
        }

        private static string SerializeObjectForLog(object obj)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(obj, serializerSettings);
        }

        /// <summary>
        /// Translate given <paramref name="authorizedParty"/> to something PRIME administrator would understand
        /// </summary>
        /// <param name="authorizedParty"></param>
        /// <returns></returns>
        private string TranslateAuthorizedParty(string authorizedParty)
        {
            switch (authorizedParty)
            {
                case "PRIME-POS-GPID":
                    return "Medinet";
                case "PRIME-APPLICATION-LOCAL":
                    return "PRIME (testing in `dev`)";
                case "PRIME-APPLICATION-TEST":
                    return "PRIME (testing in `test`)";
                default:
                    return "N/A";
            }
        }
    }
}