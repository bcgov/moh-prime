using Microsoft.AspNetCore.Mvc;

namespace Pidp.Features.Parties
{
    [Route("api/[controller]")]
    public class PartiesController : PidpControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResultResponse<IEnumerable<Index.Model>>>> GetParties([FromServices] IQueryHandler<Index.Query, List<Index.Model>> handler)
        {
            return Ok(await handler.HandleAsync(new Index.Query()));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResultResponse<int>), StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateParty([FromServices] ICommandHandler<Create.Command, int> handler,
                                                    Create.Command command)
        {
            return Ok(await handler.HandleAsync(command));
        }
    }
}
