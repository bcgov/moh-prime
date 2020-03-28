
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prime.Models.Api;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PHSAController : ControllerBase
    {
        private readonly IPHSAService _phsaService;

        public PHSAController(
            IPHSAService phsaService
        )
        {
            _phsaService = phsaService;
        }

        // POST: api/phsa
        /// <summary>
        /// Creates a phsa object and stores it in the DB, returns the id
        /// </summary>
        [HttpPost(Name = nameof(PostPHSA))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResultResponse<int>), StatusCodes.Status201Created)]
        public async Task<ActionResult<int>> PostPHSA(FromBodyText body)
        {
            try
            {
                var result = await _phsaService.CreatePHSAAsync(body);
                return Ok(ApiResponse.Result(result));
            }
            catch (JsonReaderException ex)
            {
                return BadRequest(ApiResponse.Message("Unable to parse Json Object."));
            }
        }

    }
}
