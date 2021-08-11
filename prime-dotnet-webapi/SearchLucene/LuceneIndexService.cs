using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Prime.LuceneIndexer
{
    public class LuceneIndexService : BackgroundService
    {
        private readonly ApiDbContext _context;
        public LuceneIndexService(IServiceScopeFactory factory)
        {
            _context = factory.CreateScope().ServiceProvider.GetRequiredService<ApiDbContext>();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => IndexWorker.IndexAll(_context));
        }
    }
}
