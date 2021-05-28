using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ExcelDataReader;
using Prime;
using Prime.Models;

namespace SiteExcelToSql
{

    class Program
    {
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        static void Main(string[] args)
        {
            string outputFileName = "output.sql"; // default output sql filename
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: command <site_excel.xlsx> <output.sql>");
            }
            else if (args.Length == 2)
            {
                outputFileName = args[1];
            }

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            // create data structure to hold read data
            var siteList = new List<Site>();

            using (var excelFs = File.Open(args[0], FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(excelFs))
                {
                    // skip first 2 header rows
                    reader.Read();
                    reader.Read();

                    // start to read data rows on site info sheet
                    while (reader.Read())
                    {
                        siteList.Add(reader.ParseSite());
                    }

                    // read remote access sheet
                    reader.NextResult();

                    // skip 2 header rows
                    reader.Read();
                    reader.Read();

                    var userCount = 0;
                    while (reader.Read())
                    {
                        var pec = reader.GetString(0);
                        var user = reader.PasseRemoteUser();

                        if (string.IsNullOrEmpty(pec))
                        {
                            continue;
                        }
                        userCount++;

                        // assume PEC is unique
                        var matchingSite = siteList.SingleOrDefault(s => s.PEC == pec);
                        if (matchingSite == null)
                        {
                            Console.WriteLine("No matching site with PEC \"{0}\" for user \"{1} {2}\" at row {3}",
                                pec, user.FirstName, user.LastName, reader.Depth);
                        }
                        else
                        {
                            // attach remote user to site
                            matchingSite.RemoteUsers.Add(user);
                        }
                    }

                    PrintStats(siteList, userCount);
                }
            }

            var templateSql = File.ReadAllText(Path.Combine(AssemblyDirectory, "template.sql"));

            using var fs = new FileStream(Path.Combine(AssemblyDirectory, outputFileName), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            // clear old file content
            fs.SetLength(0);

            var writer = new StreamWriter(fs);
            writer.WriteLine(templateSql);

            // data section
            for (var i = 0; i < siteList.Count; i++)
            {
                // output site SQL
                writer.WriteLine("\t-- Site {0} - {1} | {2}", i + 1, siteList[i].PEC, siteList[i].DoingBusinessAs);
                writer.WriteLine("\tINSERT INTO public.\"Site\" (\"CreatedUserId\", \"CreatedTimeStamp\", \"UpdatedUserId\", \"UpdatedTimeStamp\", \"PEC\", \"Completed\", \"DoingBusinessAs\", \"OrganizationId\", \"Status\") "
                                + "VALUES(vCreatorUUID, now(), vCreatorUUID, now(), '{0}', TRUE, '{1}', vOrganizationID, 1) "
                                + "RETURNING \"Id\" INTO vSiteId;",
                                siteList[i].PEC, siteList[i].DoingBusinessAs.Replace("'", "''"));

                // output BusinessDay SQL
                writer.WriteLine("\t-- Create business days x{0}", siteList[i].BusinessHours.Count);
                foreach (var hour in siteList[i].BusinessHours)
                {
                    writer.WriteLine("\tINSERT INTO public.\"BusinessDay\" (\"CreatedUserId\", \"CreatedTimeStamp\", \"UpdatedUserId\", \"UpdatedTimeStamp\", \"Day\", \"StartTime\", \"EndTime\", \"SiteId\") "
                        + "VALUES(vCreatorUUID, now(), vCreatorUUID, now(), {0}, '{1}', '{2}', vSiteId);",
                        (int)hour.Day, hour.StartTime, hour.EndTime);
                }

                // output remote user SQL
                writer.WriteLine("\t-- Create remote users x{0}", siteList[i].RemoteUsers.Count);
                foreach (var remoteUser in siteList[i].RemoteUsers)
                {
                    writer.WriteLine("\tINSERT INTO public.\"RemoteUser\" (\"CreatedUserId\", \"CreatedTimeStamp\", \"UpdatedUserId\", \"UpdatedTimeStamp\", \"SiteId\", \"FirstName\", \"LastName\", \"Email\") "
                                    + "VALUES(vCreatorUUID, now(), vCreatorUUID, now(), vSiteId, '{0}', '{1}', '');",
                                    remoteUser.FirstName.Replace("'", "''"), remoteUser.LastName.Replace("'", "''"));
                }

                writer.WriteLine("");
            }

            // end section
            writer.WriteLine("END$$;");
            writer.Flush();
        }

        private static void PrintStats(ICollection<Site> sites, int userCount)
        {
            Console.WriteLine("total sites: {0}", sites.Count);
            var remoteUserWithSiteCount = 0;
            foreach (var site in sites)
            {
                remoteUserWithSiteCount += site.RemoteUsers.Count;
            }

            if (userCount == remoteUserWithSiteCount)
            {
                Console.WriteLine("all {0} remote users have matching site", userCount);
            }
            else
            {
                Console.WriteLine("{0} remote users do not have matching site", (userCount - remoteUserWithSiteCount));
            }
        }
    }
}
