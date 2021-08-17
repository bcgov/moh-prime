using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Prime.Auth;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UtilController : PrimeControllerBase
    {
        [HttpGet("gccollect", Name = nameof(GCCollect))]
        [Authorize(Roles = Roles.ManageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult GCCollect()
        {
            System.GC.Collect();

            return NoContent();
        }
    }
}
