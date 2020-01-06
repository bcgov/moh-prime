using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.Models;
using Prime.Services;

namespace PrimeTests.Mocks
{
    public class PharmanetApiServiceMock : BaseMockService, IPharmanetApiService
    {
        [Flags]
        public enum OperationMode
        {
            ERROR = 0,
            NO_RECORD = 1,
            MATCHING_RECORD = 2,
            NAME_DISCREPANCY = 4,
            DATE_DISCREPANCY = 8,
            NOT_PRACTICING = 16
        }

        private Enrollee _expectedEnrollee;
        private IEnumerator<OperationMode> _modeEnumerator;

        public PharmanetApiServiceMock(Enrollee expectedEnrollee = null, params OperationMode[] modes) : base()
        {
            _modeEnumerator = modes.AsEnumerable().GetEnumerator();
            _expectedEnrollee = expectedEnrollee;
        }

        public override void SeedData() { }

        public Task<PharmanetCollegeRecord> GetCollegeRecordAsync(Certification certification)
        {
            OperationMode mode = GetNextMode();

            if (mode == OperationMode.ERROR)
            {
                throw new DefaultPharmanetApiService.PharmanetCollegeApiException("PharmaNet Mock is in error mode.");
            }
            if (mode.HasFlag(OperationMode.NO_RECORD))
            {
                return Task.FromResult<PharmanetCollegeRecord>(null);
            }

            if (_expectedEnrollee == null)
            {
                throw new InvalidOperationException($"PharmaNet Mock is in a mode that requires an enrollee to copy but {nameof(_expectedEnrollee)} is null.");
            }

            var record = new PharmanetCollegeRecord
            {
                applicationUUID = new Guid().ToString(),
                firstName = _expectedEnrollee.FirstName,
                lastName = _expectedEnrollee.LastName,
                dateofBirth = _expectedEnrollee.DateOfBirth,
                status = mode.HasFlag(OperationMode.NOT_PRACTICING) ? "N" : "P",
                effectiveDate = DateTime.Today
            };

            if (mode.HasFlag(OperationMode.NAME_DISCREPANCY))
            {
                record.lastName += "extracharacters";
            }
            if (mode.HasFlag(OperationMode.DATE_DISCREPANCY))
            {
                record.dateofBirth = record.dateofBirth.AddDays(1);
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
                return OperationMode.ERROR;
            }
        }
    }
}
