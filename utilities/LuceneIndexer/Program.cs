using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using LuceneIndexer;

namespace LuceneIndexer
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("PrimeDatabase");
            // default to current folder
            var indexPathName = System.AppDomain.CurrentDomain.BaseDirectory;
            if (args.Length > 0 && !string.IsNullOrEmpty(args[0])) {
                indexPathName = args[0];
            }
            IndexWorker.IndexAll(connectionString, indexPathName);
        }
    }
}
