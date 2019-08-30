using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using prime.Models;

namespace prime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public TokenController(ApiDbContext context)
        {
            _context = context;
        }

        // GET api/token
        [HttpGet]
        public ActionResult<object> Get(string token)
        {
            // verify google token
            // sign new token
            
            return "this is a token";
        }
    }
}
