using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models.Api;
using Prime.HttpClients;
using static Prime.HttpClients.AddressAutocompleteClient;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.PrimeAdministrant)]
    public class AddressAutocompleteController : PrimeControllerBase
    {
        private readonly IAddressAutocompleteClient _addressAutocompleteClient;

        public AddressAutocompleteController(IAddressAutocompleteClient addressAutocompleteClient)
        {
            _addressAutocompleteClient = addressAutocompleteClient;
        }

        // GET: api/AddressAutocomplete/find
        /// <summary>
        /// Gets autocomplete results
        /// Find addresses matching the search term.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="lastId"></param>
        [HttpGet("find", Name = nameof(Find))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<AddressAutocompleteFindResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Find([FromQuery] string searchTerm, [FromQuery] string lastId = null)
        {
            if (searchTerm == null)
            {
                return BadRequest();
            }
            var result = await _addressAutocompleteClient.Find(searchTerm, lastId);
            return OkResult(result);
        }

        // GET: api/AddressAutocomplete/retrieve
        /// <summary>
        /// Gets autocomplete retrieve result
        /// Returns the full address details based on the Id.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("retrieve", Name = nameof(Retrieve))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<AddressAutocompleteRetrieveResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Retrieve([FromQuery] string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = await _addressAutocompleteClient.Retrieve(id);
            return OkResult(result);
        }
    }
}
