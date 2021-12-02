using Microsoft.EntityFrameworkCore;

using Pidp.Data;

namespace Pidp.Features.Parties
{
    public class Index
    {
        public class Query : IQuery<Response>
        {
        }

        public class Response
        {
            public List<Model> Results { get; set; } = new();

            public class Model
            {
                public string FullName { get; set; } = string.Empty;
            }
        }

        public class IndexQueryHandler : IQueryHandler<Query, Response>
        {
            private readonly PidpDbContext _context;

            public IndexQueryHandler(PidpDbContext context)
            {
                _context = context;
            }

            public async Task<Response> HandleAsync(Query command)
            {
                return new Response
                {
                    Results = await _context.Parties
                        .Select(party => new Response.Model
                        {
                            FullName = $"{party.FirstName} {party.LastName}"
                        })
                        .ToListAsync()
                };
            }
        }
    }
}
