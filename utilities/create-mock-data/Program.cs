using System;
using System.Linq;

using Serilog;

using Prime;
using Prime.Models;
using PrimeTests.ModelFactories;

namespace create_mock_data
{
    class Program
    {
        /// <summary>
        /// See the README.md file
        /// </summary>
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            if (args.Length == 0) 
            {
                Log.Warning("Usage:  `dotnet run <number of Enrollees to generate> <number of Sites to generate>`");
                return;
            }

            ApiDbContext dbContext = new ApiDbContextFactory().CreateDbContext(args);
            int numEnrollees = int.Parse(args[0]);
            if (numEnrollees > 0)
            {
                var enrolleeFactory = new EnrolleeFactory();
                // Avoid primary key conflicts 
                EnrolleeFactory.IdCounter = (dbContext.Enrollees.Max(e => e.Id) + 1);
                try
                {
                    Log.Information($"Beginning to generate {numEnrollees} enrollees at {DateTime.Now}");
                    for (int i = 0; i < numEnrollees; i++)
                    {
                        Enrollee enrollee = enrolleeFactory.Generate();
                        dbContext.Enrollees.Add(enrollee);
                    }
                    dbContext.SaveChanges();
                    Log.Information($"Completed generating enrollees at {DateTime.Now}");
                }
                catch (Exception e)
                {
                    Log.Error(e, "Unexpected error encountered generating enrollees.");
                }
            }

            int numSites = int.Parse(args[1]);
            if (numSites > 0)
            {
                var siteFactory = new SiteFactory();
                try
                {
                    Log.Information($"Beginning to generate {numSites} sites at {DateTime.Now}");
                    for (int i = 0; i < numSites; i++)
                    {
                        Site site = siteFactory.Generate();
                        dbContext.Sites.Add(site);
                    }
                    dbContext.SaveChanges();
                    Log.Information($"Completed generating sites at {DateTime.Now}");
                }
                catch (Exception e)
                {
                    Log.Error(e, "Unexpected error encountered generating sites.");
                }
            }
        }
    }
}
