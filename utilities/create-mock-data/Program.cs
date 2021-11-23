using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Npgsql;
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
                EnrolleeFactory.IdCounter = (dbContext.Enrollees.Count() != 0) ? (dbContext.Enrollees.Max(e => e.Id) + 1) : 1;
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

                    // Update database's Enrollee sequence so can create Enrollees through application as per normal usage 
                    using (var conn = dbContext.Database.GetDbConnection()) 
                    {                    
                        conn.Open();
                        using (var cmd = new NpgsqlCommand($"ALTER SEQUENCE public.\"Enrollee_Id_seq\" RESTART WITH {EnrolleeFactory.IdCounter};", conn as NpgsqlConnection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    Log.Information($"Updated Enrollee sequence to {EnrolleeFactory.IdCounter}");
                }
                catch (Exception e)
                {
                    Log.Error(e, "Unexpected error encountered generating enrollees.");
                }
            }

            int numSites = int.Parse(args[1]);
            if (numSites > 0)
            {
                var siteFactory = new CommunitySiteFactory();
                try
                {
                    Log.Information($"Beginning to generate {numSites} sites at {DateTime.Now}");
                    for (int i = 0; i < numSites; i++)
                    {
                        CommunitySite site = siteFactory.Generate();
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
