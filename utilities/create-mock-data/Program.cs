using System;
using Prime;
using Prime.Models;
using PrimeTests.ModelFactories;
using Serilog;

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
            int numEnrollees = int.Parse(args[0]);

            try
            {
                ApiDbContext dbContext = new ApiDbContextFactory().CreateDbContext(args);
                Log.Information($"Beginning to generate {numEnrollees} enrollees at {DateTime.Now}");
                for (int i = 0; i < numEnrollees; i++)
                {
                    Enrollee enrollee = new EnrolleeFactory().Generate();
                    dbContext.Enrollees.Add(enrollee);
                }
                dbContext.SaveChanges();
                Log.Information($"Completed generating enrollees at {DateTime.Now}");
            }
            catch (Exception e)
            {
                Log.Error(e, "Unexpected error encountered.");
            }
        }
    }
}
