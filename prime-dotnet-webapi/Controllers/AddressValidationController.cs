using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels;
using Prime.Services.Clients;
using static Prime.Services.Clients.AddressValidationClient;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Policy = AuthConstants.USER_POLICY)]
    public class AddressValidationController : ControllerBase
    {
        private readonly IAddressValidationClient _addressValidationClient;

        public AddressValidationController(IAddressValidationClient addressValidationClient)
        {
            _addressValidationClient = addressValidationClient;
        }


        // GET: api/AddressValidation/find
        /// <summary>
        /// Gets autocomplete results
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="lastId"></param>
        [HttpGet("find", Name = nameof(Find))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<object>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Find([FromQuery] string searchTerm, [FromQuery] string lastId = "0")
        {
            var result = await _addressValidationClient.Find(searchTerm, lastId);
            return Ok(ApiResponse.Result(result.Property("Items").Value));
        }

        // GET: api/AddressValidation/retrieve
        /// <summary>
        /// Gets autocomplete retrieve result
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("retrieve", Name = nameof(Retrieve))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<AddressAutocompleteRetrieveResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Retrieve([FromQuery] string id)
        {
            var result = await _addressValidationClient.Retrieve(id);
            return Ok(ApiResponse.Result(result));
        }
    }
}
