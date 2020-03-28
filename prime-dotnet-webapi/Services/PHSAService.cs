using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prime.Models;
using Newtonsoft.Json.Linq;

namespace Prime.Services
{
    public class PHSAService : BaseService, IPHSAService
    {
        public PHSAService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task<int> CreatePHSAAsync(string body)
        {
            PHSA obj = new PHSA();

            obj.Body = body;
            obj.SubmissionTime = DateTime.Now;

            _context.PHSA.Add(obj);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create PHSA.");
            }

            return obj.Id;
        }
    }
}
