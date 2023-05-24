using System;
using System.Threading.Tasks;

using Prime.HttpClients.PharmanetCollegeApiDefinitions;

namespace Prime.HttpClients
{
    /// <summary>
    /// Pharmanet API is not availible in local dev, so a dummy client is used instead to return test data.
    /// </summary>
    public class DummyCollegeLicenceClient : ICollegeLicenceClient
    {
        public Task<PharmanetCollegeRecord> GetCollegeRecordAsync(string licencePrefix, string licenceNumber)
        {
            if (licenceNumber == "error")
            {
                throw new PharmanetCollegeApiException();
            }

            var info = licenceNumber.TrimStart('0') switch
            {
                "1" => new { Date = "2000-05-17", Name = "ONE" },
                "2" => new { Date = "1998-08-07", Name = "TWO" },
                "3" => new { Date = "1998-08-08", Name = "THREE" },
                "4" => new { Date = "1999-10-01", Name = "FOUR" },
                "5" => new { Date = "1999-01-31", Name = "FIVE" },
                "6" => new { Date = "2000-02-25", Name = "SIX" },
                "7" => new { Date = "1999-03-14", Name = "SEVEN" },
                "8" => new { Date = "1999-01-04", Name = "EIGHT" },
                "9" => new { Date = "1997-10-12", Name = "NINE" },
                "10" => new { Date = "2000-05-30", Name = "TEN" },
                "11" => new { Date = "2000-06-07", Name = "ELEVEN" },
                _ => null,
            };

            if (info == null)
            {
                info = new { Date = "1999-04-29", Name = "THIRTEEN" };
            }

            if (info == null)
            {
                return Task.FromResult<PharmanetCollegeRecord>(null);
            }

            return Task.FromResult(new PharmanetCollegeRecord
            {
                ApplicationUUID = new Guid().ToString(),
                FirstName = "PRIMET",
                LastName = info.Name,
                DateofBirth = DateTime.Parse(info.Date),
                Status = "P",
                EffectiveDate = DateTime.Today
            });
        }
    }
}
