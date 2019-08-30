using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

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

            if (application == null)
            {
                return NotFound();
            }

            return application;
        }

        // POST api/v1/application/
        [HttpPost]
        [EnableCors("AllowAll")]

        public async Task<ActionResult<IEnumerable<Application>>> Post([FromBody] Application application)
        {
            Console.Out.WriteLine("POST!");
            application.AppliedDate = DateTime.Now;

            if (application.PharmacistRegistrationNumber != null)
            {
                application.Approved = true;
                application.ApprovedDate = DateTime.Now;
                application.ApprovedReason = "Valid registration number provided.";
            }

            _context.Application.Add(application);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = application.Id }, application);
        }

        // PUT: api/v1/application/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Application application)
        {
            Console.Out.WriteLine("PUT!");
            if (id != application.Id)
            {
                return BadRequest();
            }

            if (application.Approved == true)
            {
                application.ApprovedDate = DateTime.Now;
                application.ApprovedReason = "Approved by system administrator.";
            }

            _context.Entry(application).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
