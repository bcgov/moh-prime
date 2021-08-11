using System.Collections.Generic;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Prime.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Prime.LuceneIndexer
{
    public static class IndexWorker
    {
        private static RAMDirectory _directory;
        private static StandardAnalyzer _analyzer;

        public static void IndexAll(ApiDbContext dbContext)
        {
            _analyzer = new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48);
            // index folder will be suffixed with 'index'
            _directory = new RAMDirectory();
            var config = new IndexWriterConfig(Lucene.Net.Util.LuceneVersion.LUCENE_48, _analyzer);

            using var writer = new IndexWriter(_directory, config);
            foreach (var enrollee in dbContext.Enrollees.Include(e => e.Addresses)
                                                            .ThenInclude(a => a.Address)
                                                        .Include(e => e.Certifications))
            {
                var doc = enrollee.ToLuceneDocument();

                foreach (var cert in enrollee.Certifications)
                {
                    doc.Add(new Field("LicenseNumber", cert.LicenseNumber, TextField.TYPE_NOT_STORED));
                }
                foreach (var address in enrollee.Addresses)
                {
                    doc.Add(new Field("Address.City", address.Address.City, TextField.TYPE_STORED));
                    doc.Add(new Field("Address.Street", address.Address.Street, TextField.TYPE_STORED));
                    doc.Add(new Field("Address.Postal", address.Address.Postal, TextField.TYPE_STORED));
                }
                writer.AddDocument(doc);
            }
            writer.Commit();
        }

        public static Document ToLuceneDocument(this Enrollee enrollee)
        {
            var doc = new Document
            {
                new Field("Id", enrollee.Id.ToString(), TextField.TYPE_STORED),
                new Field("GPID", enrollee.GPID??"", TextField.TYPE_STORED),
                new Field("HPDID", enrollee.HPDID??"", TextField.TYPE_STORED),
                new Field("FirstName", enrollee.FirstName, TextField.TYPE_STORED),
                new Field("LastName", enrollee.LastName, TextField.TYPE_STORED),
                new Field("GivenNames", enrollee.GivenNames??"", TextField.TYPE_STORED),
                new Field("PreferredFirstName", enrollee.PreferredFirstName??"", TextField.TYPE_STORED),
                new Field("PreferredMiddleName", enrollee.PreferredMiddleName??"", TextField.TYPE_STORED),
                new Field("PreferredLastName", enrollee.PreferredLastName??"", TextField.TYPE_STORED),
                new Field("DateOfBirth", enrollee.DateOfBirth.Ticks.ToString(), TextField.TYPE_STORED),
                new Field("Email", enrollee.Email, TextField.TYPE_STORED),
                new Field("SmsPhone", enrollee.SmsPhone??"", TextField.TYPE_STORED),
                new Field("Phone", enrollee.Phone??"", TextField.TYPE_STORED)
            };

            return doc;
        }

        public static IEnumerable<int> SearchEnrollee(string searchText)
        {
            var reader = DirectoryReader.Open(_directory);
            var searcher = new IndexSearcher(reader);
            var parser = new MultiFieldQueryParser(Lucene.Net.Util.LuceneVersion.LUCENE_48,
                new string[] {"Id", "GPID", "HPDID", "FirstName", "LastName", "GivenNames", "PreferredFirstName", "PreferredMiddleName",
                "PreferredLastName", "DateOfBirth", "Email", "SmsPhone", "Phone", "LicenseNumber", "Address.City", "Address.Street",
                "Address.Postal"},
                _analyzer);
            var query = parser.Parse(searchText);
            var hits = searcher.Search(query, null, 1000).ScoreDocs;
            return hits.Select(hit =>
            {
                var hitDoc = searcher.Doc(hit.Doc);
                return int.Parse(hitDoc.Get("Id"));
            });
        }
    }
}
