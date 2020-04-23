
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prime.Models.Api;
using Prime.Services;
using System.IO;

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
        public async Task<ActionResult<int>> PostPHSA()
        {
            StreamReader reader = new StreamReader(Request.Body);
            string content = await reader.ReadToEndAsync();
            if (content.Contains("error"))
            {
                return BadRequest();
            }
            var result = await _phsaService.CreatePHSAAsync(content);
            return Ok(ApiResponse.Result(result));
        }

    }
}
