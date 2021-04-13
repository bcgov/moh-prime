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
        public int CreateOrUpdatePlrProvider(PlrProvider dataObject, bool expectExists = false)
        {
            // var existingPlrProvider = await _context.PlrProviders.SingleOrDefaultAsync(p => dataObject.Ipc == p.Ipc);
            var task = _context.PlrProviders.SingleOrDefaultAsync(p => dataObject.Ipc == p.Ipc);
            var existingPlrProvider = task.Result;

            if (existingPlrProvider == null)
            {
                _context.PlrProviders.Add(dataObject);
                if (expectExists)
                {
                    _logger.LogWarning("Expected PLR Provider with IPC of {ipc} to exist but it cannot be found", dataObject.Ipc);
                }
            }
            else
            {
                existingPlrProvider.Update(dataObject);
                if (!expectExists)
                {
                    _logger.LogWarning("Did not expect PLR Provider with IPC of {ipc} to exist but it was found with ID of {id}", dataObject.Ipc, existingPlrProvider.Id);
                }
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
            return (existingPlrProvider == null ? dataObject.Id : existingPlrProvider.Id);
        }
    }
}
