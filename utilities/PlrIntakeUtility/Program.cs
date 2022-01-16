using System;
using System.Globalization;
using System.IO;
using Microsoft.EntityFrameworkCore;

using CsvHelper;
using Serilog;

using Prime;
using Prime.Models;

namespace PlrIntakeUtility
{
    class Program
    {
        /// <summary>
        /// Can be used like this:  `dotnet run PRIME_Test_Data_PLR_IAT20210617_v2.0.csv intake.log`
        /// </summary>
        /// <param name="args">Expecting path to .csv file and desired log file</param>
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(args[1])
                .CreateLogger();

            // See https://github.com/ExcelDataReader/ExcelDataReader#important-note-on-net-core
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            Log.Information($"Started loading {args[0]} at {DateTime.Now}");
            try
            {
                ApiDbContext dbContext = new ApiDbContextFactory().CreateDbContext(args);
                
                using (var stream = new StreamReader(args[0]))
                {
                    using (var reader = new CsvReader(stream, CultureInfo.InvariantCulture))
                    {
                        int numProviders = 0;
                        int rowNum = 1;

                        // Consume header row
                        reader.Read();

                        PlrIntaker intaker = new PlrIntaker();
                        while (reader.Read())
                        {
                            try
                            {
                                rowNum++;
                                PlrProvider provider = intaker.ReadRow(reader);
                                intaker.CheckData(provider, rowNum);
                                dbContext.PlrProviders.Add(provider);
                                try
                                {
                                    dbContext.SaveChanges();
                                    numProviders++;
                                }
                                catch (DbUpdateException e)
                                {
                                    // e.g. May get `duplicate key value violates unique constraint "IX_PlrProvider_Ipc"` error
                                    Log.Error(e, $"Error saving {nameof(PlrProvider)} at row number {rowNum} to the database.");
                                    dbContext.PlrProviders.Remove(provider);
                                }
                            }
                            catch (Exception e)
                            {
                                Log.Error(e, $"Error ingesting row at row number {rowNum}, IPC: {reader.GetField<string>(0)}");
                            }
                        }

                        Log.Information($"Number of providers loaded: {numProviders}, Final row number: {rowNum}.");
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Unexpected error encountered.");
            }
            finally
            {
                Log.Information($"Program ended at {DateTime.Now}");
                Console.WriteLine($"Program completed ... check {args[1]}.");
            }
        }
    }
}
