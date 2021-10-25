using System;
using Prime;
using Prime.Models;
using PrimeTests.ModelFactories;
using Serilog;

namespace create_mock_data
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                ApiDbContext dbContext = new ApiDbContextFactory().CreateDbContext(args);
                Enrollee enrollee = new EnrolleeFactory().Generate();
                dbContext.Enrollees.Add(enrollee);
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error(e, "Unexpected error encountered.");
            }
        }
    }
}
