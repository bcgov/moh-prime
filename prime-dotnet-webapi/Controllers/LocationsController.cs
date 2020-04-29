using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prime.Models.MongoModels;
using Prime.Services;

namespace Prime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly LocationService _locationService;

        public LocationsController(LocationService locationService)
        {
            _locationService = locationService;
        }

        // GET: /api/Locations
        /// <summary>
        /// Gets all the locations
        /// </summary>
        [HttpGet(Name = nameof(Get))]
        public async Task<ActionResult<List<Locations>>> Get() =>
            await _locationService.Get();


        // GET: api/Locations/5
        /// <summary>
        /// Gets a specific Location.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id:length(24)}", Name = "GetLocations")]
        public async Task<ActionResult<Locations>> Get(string id)
        {
            var location = await _locationService.Get(id);

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        // POST: api/Locations
        /// <summary>
        /// Creates a new Location.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Locations>> Create(Locations location)
        {
            await _locationService.Create(location);

            return CreatedAtRoute("GetLocations", new { id = location.Id.ToString() }, location);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Locations locationIn)
        {
            var location = await _locationService.Get(id);

            if (location == null)
            {
                return NotFound();
            }

            _locationService.Update(id, locationIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var location = await _locationService.Get(id);

            if (location == null)
            {
                return NotFound();
            }

            _locationService.Remove(location.Id);

            return NoContent();
        }
    }
}

