using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.Models;
using Prime.HttpClients;
using Prime.HttpClients.PharmanetCollegeApiDefinitions;

namespace PrimeTests.HttpClientMocks
{
    public class CollegeLicenceClientMock : ICollegeLicenceClient
    {
        [Flags]
        public enum OperationMode
        {
            Error = 0,
            NoRecord = 1,
            MatchingRecord = 2,
            NameDiscrepancy = 4,
            DateDiscrepancy = 8,
            NotPracticing = 16
        }

        private Enrollee _expectedEnrollee;
        private IEnumerator<OperationMode> _modeEnumerator;

        public CollegeLicenceClientMock(Enrollee expectedEnrollee = null, params OperationMode[] modes)
        {
            _modeEnumerator = modes.AsEnumerable().GetEnumerator();
            _expectedEnrollee = expectedEnrollee;
        }

        public Task<PharmanetCollegeRecord> GetCollegeRecordAsync(string licencePrefix, string licenceNumber)
        {
            OperationMode mode = GetNextMode();

            if (mode == OperationMode.Error)
            {
                throw new PharmanetCollegeApiException("PharmaNet Mock is in error mode.");
            }
            if (mode.HasFlag(OperationMode.NoRecord))
            {
                return Task.FromResult<PharmanetCollegeRecord>(null);
            }

            if (_expectedEnrollee == null)
            {
                throw new InvalidOperationException($"PharmaNet Mock is in a mode that requires an enrollee to copy but {nameof(_expectedEnrollee)} is null.");
            }

            var record = new PharmanetCollegeRecord
            {
                ApplicationUUID = new Guid().ToString(),
                FirstName = _expectedEnrollee.FirstName,
                LastName = _expectedEnrollee.LastName,
                DateofBirth = _expectedEnrollee.DateOfBirth,
                Status = mode.HasFlag(OperationMode.NotPracticing) ? "N" : "P",
                EffectiveDate = DateTime.Today
            };

            if (mode.HasFlag(OperationMode.NameDiscrepancy))
            {
                record.LastName += "extracharacters";
            }
            if (mode.HasFlag(OperationMode.DateDiscrepancy))
            {
                record.DateofBirth = record.DateofBirth.AddDays(1);
            }

            return Task.FromResult(record);
        }

        private OperationMode GetNextMode()
        {
            if (_modeEnumerator.MoveNext())
            {
                return _modeEnumerator.Current;
            }
            else
            {
                return OperationMode.Error;
            }
        }
    }
}
