using Microsoft.EntityFrameworkCore;

using Pidp.Data;

namespace Pidp.Features.Parties
{
    public class Index
    {
        public class Query : IQuery<List<Model>>
        {
        }

        public class Model
        {
            public string FullName { get; set; } = string.Empty;
        }

        public class IndexQueryHandler : IQueryHandler<Query, List<Model>>
        {
            private readonly PidpDbContext _context;

            public IndexQueryHandler(PidpDbContext context)
            {
                _context = context;
            }

            public async Task<List<Model>> HandleAsync(Query command)
            {
                return await _context.Parties
                    .Select(party => new Model
                    {
                        FullName = $"{party.FirstName} {party.LastName}"
                    })
                    .ToListAsync();
            }
        }
    }
}
