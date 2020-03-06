using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;
using Prime.Models.Api;

namespace Prime.Services
{
    public class SubmissionService : BaseService, ISubmissionService
    {
        public SubmissionService(ApiDbContext context, IHttpContextAccessor httpContext) : base(context, httpContext)
        {
        }

        public async Task<Enrollee> PerformSubmissionActionAsync(int enrolleeId, SubmissionAction action)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateAlwaysManualAsync(int enrolleeId, bool alwaysManual)
        {
            var enrollee = await _context.Enrollees
               .SingleAsync(e => e.Id == enrolleeId);

            enrollee.AlwaysManual = alwaysManual;
            await _context.SaveChangesAsync();
        }






    }
}
