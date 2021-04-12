using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Prime.Models;

namespace Prime.Services
{
    public class PlrProviderService : BaseService, IPlrProviderService
    {
        private readonly ILogger _logger;

        public PlrProviderService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            ILogger<PlrProviderService> logger)
            : base(context, httpContext)
        {
            _logger = logger;
        }

        //        public async Task<int> CreateOrUpdatePlrProviderAsync(PlrProvider dataObject)

        public int CreateOrUpdatePlrProvider(PlrProvider dataObject)
        {
            // var existingPlrProvider = await _context.PlrProviders.SingleOrDefaultAsync(p => dataObject.Ipc == p.Ipc);
            var task = _context.PlrProviders.SingleOrDefaultAsync(p => dataObject.Ipc == p.Ipc);
            var existingPlrProvider = task.Result;

            if (existingPlrProvider == null)
            {
                _context.PlrProviders.Add(dataObject);
            }
            else
            {
                _logger.LogInformation("Existing PLR Provider found with ID of {id} that has matching IPC of {ipc}", existingPlrProvider.Id, dataObject.Ipc);
                // TODO: Update existingPlrProvider fields
            }

            try
            {
                // await _context.SaveChangesAsync();
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError(e, "Error updating PLR Provider with with ID of {id}", existingPlrProvider.Id);
                return -1;
            }

            // TODO: Need something like IPartyChangeModel?
            return dataObject.Id;
        }
    }
}
