using System;
using System.IO;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
using Prime;
using Prime.Models;
using Serilog;

namespace PlrIntakeUtility
{
    class Program
    {
        /// <summary>
        /// Can be used like this:  `dotnet run PRIME_Test_Data_PLR_IAT20210212.xls intake.log`
        /// </summary>
        /// <param name="args">Expecting path to .xls file and desired log file</param>
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

                using (var stream = File.Open(args[0], FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        int numProviders = 0;
                        int rowNum = 1;
                        do
                        {
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
                                    Log.Error(e, $"Error ingesting row at row number {rowNum}.");
                                }
                            }
                        } while (reader.NextResult());

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
