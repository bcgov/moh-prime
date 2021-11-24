
using Microsoft.EntityFrameworkCore;

namespace Pidp.Services
{
    public class FirstService : IFirstService
    {
        protected readonly ApiDbContext _context;

        public FirstService(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<bool> GetSomethingAsync()
        {
            var something = await _context.FirstModel.ToListAsync();

            return something.Count > 0;
        }

        public async Task<IEnumerable<string>> GetLookupModelNamesAsync()
        {
            var modelsNames = await _context.NewModel!
                                .Select(nm => nm!.Name)
                                .ToListAsync();

            return modelsNames;
        }
    }
}
