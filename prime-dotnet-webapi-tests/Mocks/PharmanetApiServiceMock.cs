using System;
using System.Threading.Tasks;

using Prime.Models;
using Prime.Services;

namespace PrimeTests.Mocks
{
    public class PharmanetApiServiceMock : BaseMockService, IPharmanetApiService
    {
        [Flags]
        public enum Mode
        {
            ERROR = 0,
            NO_RECORD = 1,
            MATCHING_RECORD = 2,
            NAME_DISCREPANCY = 4,
            DATE_DISCREPANCY = 8,
            NOT_PRACTICING = 16
        }

        public Mode OperationMode { get; set; }
        public Enrollee ExpectedEnrollee { get; set; }

        public PharmanetApiServiceMock(Mode startingMode = Mode.NO_RECORD, Enrollee expectedEnrollee = null) : base()
        {
            OperationMode = startingMode;
            ExpectedEnrollee = expectedEnrollee;
        }

        public override void SeedData() { }

        public Task<PharmanetCollegeRecord> GetCollegeRecordAsync(Certification certification)
        {
            if (OperationMode == Mode.ERROR)
            {
                throw new DefaultPharmanetApiService.PharmanetCollegeApiException();
            }
            if (OperationMode.HasFlag(Mode.NO_RECORD))
            {
                return Task.FromResult<PharmanetCollegeRecord>(null);
            }

            if (ExpectedEnrollee == null)
            {
                throw new InvalidOperationException($"PharmaNet mock is in a mode that requires an enrollee to copy but {nameof(ExpectedEnrollee)} is null.");
            }

            var record = new PharmanetCollegeRecord
            {
                applicationUUID = new Guid().ToString(),
                firstName = ExpectedEnrollee.FirstName,
                lastName = ExpectedEnrollee.LastName,
                dateofBirth = ExpectedEnrollee.DateOfBirth,
                status = OperationMode.HasFlag(Mode.NOT_PRACTICING) ? "N" : "P",
                effectiveDate = DateTime.Today
            };

            if (OperationMode.HasFlag(Mode.NAME_DISCREPANCY))
            {
                record.lastName += "extracharacters";
            }
            if (OperationMode.HasFlag(Mode.DATE_DISCREPANCY))
            {
                record.dateofBirth = record.dateofBirth.AddDays(1);
            }

            return Task.FromResult(record);
        }
    }
}
