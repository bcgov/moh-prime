using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // GET api/v1/application
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> Get()
        {
            return await _context.Application.ToListAsync();
        }

        // GET api/v1/application/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> Get(int id)
        {
            var application = await _context.Application.FindAsync(id);

            if(application == null)
            {
                return NotFound();
            }

            return application;
        }

        // POST api/v1/application/
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Application>>> Post([FromBody] Application application)
        {
            _context.Application.Add(application);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(Get), new {id = application.Id}, application);
        }        
    }
}
