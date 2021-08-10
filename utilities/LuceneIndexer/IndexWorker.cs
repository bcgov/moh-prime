using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Npgsql;

namespace LuceneIndexer
{
    public class EnrolleeSearchModel
    {
        public int Id { get; set; }
        public string GPID { get; set; }
        public string HPDID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GivenNames { get; set; }
        public string PreferredFirstName { get; set; }
        public string PreferredMiddleName { get; set; }
        public string PreferredLastName { get; set; }
        public long DateOfBirth { get; set; }
        public string Email { get; set; }
        public string SmsPhone { get; set; }
        public string Phone { get; set; }
        public List<string> LicenseNumbers { get; set; }
    }

    public static class IndexWorker
    {
        public static IndexWriter GetIndexWriter(string pathName)
        {
            var analyzer = new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48);
            var indexPath = Path.Combine(pathName, "index");
            var directory = FSDirectory.Open(indexPath);
            var config = new IndexWriterConfig(Lucene.Net.Util.LuceneVersion.LUCENE_48, analyzer);
            return new IndexWriter(directory, config);
        }

        public static void IndexAll(string connString, string pathName)
        {
            var enrolleeList = new List<EnrolleeSearchModel>();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                // enrollee
                using var cmd = new NpgsqlCommand(@"SELECT
                                                        ""Id"",
                                                        ""GPID"",
                                                        ""HPDID"",
                                                        ""FirstName"",
                                                        ""LastName"",
                                                        ""GivenNames"",
                                                        ""PreferredFirstName"",
                                                        ""PreferredMiddleName"",
                                                        ""PreferredLastName"",
                                                        ""DateOfBirth"",
                                                        ""Email"",
                                                        ""SmsPhone"",
                                                        ""Phone""
                                                    FROM public.""Enrollee""", conn);
                using var dbReader = cmd.ExecuteReader();
                while (dbReader.Read())
                {
                    var enrollee = new EnrolleeSearchModel
                    {
                        Id = dbReader.GetInt32(0),
                        GPID = dbReader.SafeGetString(1),
                        HPDID = dbReader.SafeGetString(2),
                        FirstName = dbReader.SafeGetString(3),
                        LastName = dbReader.SafeGetString(4),
                        GivenNames = dbReader.SafeGetString(5),
                        PreferredFirstName = dbReader.SafeGetString(6),
                        PreferredMiddleName = dbReader.SafeGetString(7),
                        PreferredLastName = dbReader.SafeGetString(8),
                        DateOfBirth = dbReader.GetDateTime(9).Ticks,
                        Email = dbReader.SafeGetString(10),
                        SmsPhone = dbReader.SafeGetString(11),
                        Phone = dbReader.SafeGetString(12),
                        LicenseNumbers = new List<string>()
                    };
                    enrolleeList.Add(enrollee);
                }
            }

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                foreach (var enrollee in enrolleeList)
                {
                    // license number
                    using var cmd = new NpgsqlCommand($@"SELECT ""LicenseNumber""
                                                        FROM public.""Certification""
                                                        WHERE ""EnrolleeId"" = {enrollee.Id}", conn);
                    using var dbReader = cmd.ExecuteReader();
                    while (dbReader.Read())
                    {
                        enrollee.LicenseNumbers.Add(dbReader.SafeGetString(0));
                    }
                }
            }

            using (var indexWriter = GetIndexWriter(pathName))
            {
                var doc = new Document();
                foreach (var enrollee in enrolleeList)
                {
                    doc.Add(new Field("Id", enrollee.Id.ToString(), TextField.TYPE_STORED));
                    doc.Add(new Field("GPID", enrollee.GPID, TextField.TYPE_STORED));
                    doc.Add(new Field("HPDID", enrollee.HPDID, TextField.TYPE_STORED));
                    doc.Add(new Field("FirstName", enrollee.FirstName, TextField.TYPE_STORED));
                    doc.Add(new Field("LastName", enrollee.LastName, TextField.TYPE_STORED));
                    doc.Add(new Field("GivenNames", enrollee.GivenNames, TextField.TYPE_STORED));
                    doc.Add(new Field("PreferredFirstName", enrollee.PreferredFirstName, TextField.TYPE_STORED));
                    doc.Add(new Field("PreferredMiddleName", enrollee.PreferredMiddleName, TextField.TYPE_STORED));
                    doc.Add(new Field("PreferredLastName", enrollee.PreferredLastName, TextField.TYPE_STORED));
                    doc.Add(new Field("DateOfBirth", enrollee.DateOfBirth.ToString(), TextField.TYPE_STORED));
                    doc.Add(new Field("Email", enrollee.Email, TextField.TYPE_STORED));
                    doc.Add(new Field("SmsPhone", enrollee.SmsPhone, TextField.TYPE_STORED));
                    doc.Add(new Field("Phone", enrollee.Phone, TextField.TYPE_STORED));

                    foreach (var licenseNumber in enrollee.LicenseNumbers)
                    {
                        doc.Add(new Field("LicenseNumber", licenseNumber, TextField.TYPE_STORED));
                    }
                    indexWriter.AddDocument(doc);
                }
                indexWriter.Commit();
            }
        }

        public static string SafeGetString(this NpgsqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex)) {
                return reader.GetString(colIndex);
            }
            return string.Empty;
        }
    }
}
