using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using prime.Models;

namespace prime.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public ApplicationController(ApiDbContext context)
        {
            _context = context;
        }

        // POST api/v1/applications/
        [HttpPost]
        public ActionResult<IEnumerable<string>> Post([FromBody] string value)
        {
            return new string[] { "test" };
        }        
    }
}
