using Microsoft.AspNetCore.Mvc;

namespace Pidp.Features.Parties
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PartiesController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Index.Response>> GetParties([FromServices] IQueryHandler<Index.Query, Index.Response> handler)
        {
            return await handler.HandleAsync(new Index.Query());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateParty([FromServices] ICommandHandler<Create.Command, int> handler,
                                                         Create.Command command)
        {
            return await handler.HandleAsync(command);
        }
    }
}
