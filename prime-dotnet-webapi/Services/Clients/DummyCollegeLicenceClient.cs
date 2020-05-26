using System;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services.Clients
{
    /// <summary>
    /// Pharmanet API is not availible in local dev, so a dummy client is used instead to return test data.
    /// </summary>
    public class DummyCollegeLicenceClient : ICollegeLicenceClient
    {
        public Task<PharmanetCollegeRecord> GetCollegeRecordAsync(Certification cert)
        {
            if (cert.LicenseNumber == "error")
            {
                throw new PharmanetCollegeApiException();
            }

            int parsed;
            if (!Int32.TryParse(cert.LicenseNumber, out parsed) || parsed < 1 || parsed > 11)
            {
                return Task.FromResult<PharmanetCollegeRecord>(null);
            }

            var lookup = new[]
            {
                null,
                new {Date = "2000-05-17", Name = "ONE"},
                new {Date = "1998-08-07", Name = "TWO"},
                new {Date = "1998-08-08", Name = "THREE"},
                new {Date = "1999-10-01", Name = "FOUR"},
                new {Date = "1999-01-31", Name = "FIVE"},
                new {Date = "2000-02-25", Name = "SIX"},
                new {Date = "1999-03-14", Name = "SEVEN"},
                new {Date = "1999-01-04", Name = "EIGHT"},
                new {Date = "1997-10-12", Name = "NINE"},
                new {Date = "2000-05-30", Name = "TEN"},
                new {Date = "2000-06-07", Name = "ELEVEN"}
            };
            var info = lookup[parsed];

            return Task.FromResult(new PharmanetCollegeRecord
            {
                applicationUUID = new Guid().ToString(),
                firstName = "PRIMET",
                lastName = info.Name,
                dateofBirth = DateTime.Parse(info.Date),
                status = "P",
                effectiveDate = DateTime.Today
            });
        }
    }
}
