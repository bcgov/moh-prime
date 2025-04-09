using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

using Prime.Models;
using Prime.HttpClients;
using System.Text;

namespace Prime.Services
{
    public class ReportingService : BaseService, IReportingService
    {
        private const int CalculationPeriodInDays = 4;

        private readonly ICollegeLicenceClient _collegeLicenceClient;

        public ReportingService(
            ApiDbContext context,
            ILogger<ReportingService> logger,
            ICollegeLicenceClient collegeLicenceClient)
            : base(context, logger)
        {
            _collegeLicenceClient = collegeLicenceClient;
        }

        public async Task<string> PopulatePractitionerTableAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            var result = "";
            try
            {
                if (startDate == null)
                {
                    startDate = DateTime.Now.AddDays(-CalculationPeriodInDays);
                }

                if (endDate == null)
                {
                    endDate = DateTime.Now;
                }

                // get all approved enrollee
                var enrolleeLicences = _context.Certifications
                    .Where(c => c.Enrollee.GPID != null && c.Prefix != null)
                    // do not pull prefix from LicenseDetail since we are not sure if prescribing or not
                    // and the Prefix here has been verified from PharmaNet API
                    .Select(e => e);

                _logger.LogInformation("Execute query to get questionable practitioner ID");

                // query the unauthorized access practitioner ID from pharmanet transaction log table
                var questionablePractitionerIds = await _context.PharmanetTransactionLogs
                    .Where(l => l.TxDateTime >= startDate && l.TxDateTime <= endDate && l.CollegePrefix != null && l.PractitionerId != null)
                    .Where(l => !enrolleeLicences.Where(e =>
                        // for college BCCNM (code 3), compare PharmaNet ID of the college license to Practitioner Id of the log
                        e.CollegeCode == CollegeCode.BCCNM && e.PractitionerId == l.PractitionerId && e.Prefix == l.CollegePrefix).Any())
                    .Where(l => !enrolleeLicences.Where(e =>
                        //for other college, use License Number
                        e.CollegeCode != CollegeCode.BCCNM && e.LicenseNumber == l.PractitionerId && e.Prefix == l.CollegePrefix).Any())
                    .Where(l => !_context.Practitioner.Where(p => p.PracRefId == l.CollegePrefix && p.CollegeId == l.PractitionerId).Any())
                    .Select(l => new
                    {
                        l.PractitionerId,
                        l.CollegePrefix
                    }).Distinct().ToListAsync();

                _logger.LogInformation("Save practitioner IDs to database");

                foreach (var p in questionablePractitionerIds)
                {
                    _context.Practitioner.Add(new Practitioner
                    {
                        CollegeId = p.PractitionerId,
                        PracRefId = p.CollegePrefix
                    });
                }

                result = $"Start date = {startDate:dd MMM yyyy}, End Date = {endDate:dd MMM yyyy}, questionablePractitionerIds count = {questionablePractitionerIds.Count} ";

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {nameof(PopulatePractitionerTableAsync)} - Message: {ex.Message}");
                result = $"Error: {nameof(PopulatePractitionerTableAsync)} - Message: {ex.Message}";
            }

            return result;
        }

        public async Task<string> UpdatePractitionerTableAsync()
        {
            var result = "";
            try
            {
                var practitionerRecords = await _context.Practitioner.Where(p => p.ProcessedDate == null && p.FirstName == null)
                .Select(p => p).ToListAsync();

                foreach (var p in practitionerRecords)
                {
                    var collegeRecord = await _collegeLicenceClient.GetCollegeRecordAsync(p.PracRefId, p.CollegeId);
                    if (collegeRecord != null)
                    {
                        p.FirstName = collegeRecord.FirstName;
                        p.LastName = collegeRecord.LastName;
                        p.MiddleInitial = collegeRecord.MiddleInitial;
                        p.DateofBirth = collegeRecord.DateofBirth;
                        p.Status = collegeRecord.Status;
                        p.EffectiveDate = collegeRecord.EffectiveDate;
                    }
                    p.ProcessedDate = DateTime.Now;

                    _context.Update(p);
                }
                result = $"practitionerRecords count = {practitionerRecords.Count}";

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {nameof(UpdatePractitionerTableAsync)} - Message: {ex.Message}");
                result = $"Error: {nameof(UpdatePractitionerTableAsync)} - Message: {ex.Message}";
            }

            return result;
        }

        public async Task<int> PopulateTransactionLogTempAsync(int numberInDays)
        {
            // delete outdated records
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.Append("delete from \"PharmanetTransactionLogTemp\"");
            deleteSql.Append($"where \"TxDateTime\" < current_date + interval '-{numberInDays}' day");

            await _context.Database.ExecuteSqlRawAsync(deleteSql.ToString());

            long? maxTransactionId = await _context.PharmanetTransactionLogTemps.MaxAsync(l => (long?)l.TransactionId);

            // copy records over to temp table
            StringBuilder copySql = new StringBuilder();
            copySql.Append("insert into \"PharmanetTransactionLogTemp\"");
            copySql.Append("(\"Id\", \"CreatedTimeStamp\", \"TransactionId\", \"TxDateTime\", \"UserId\", \"PharmacyId\", \"TransactionType\", ");
            copySql.Append("\"TransactionSubType\", \"PractitionerId\", \"CollegePrefix\", \"TransactionOutcome\", \"ProviderSoftwareId\", ");
            copySql.Append("\"ProviderSoftwareVersion\", \"LocationIpAddress\", \"SourceIpAddress\")");
            copySql.Append("SELECT \"Id\", \"CreatedTimeStamp\", \"TransactionId\", \"TxDateTime\", \"UserId\", \"PharmacyId\", \"TransactionType\", ");
            copySql.Append("\"TransactionSubType\", \"PractitionerId\", \"CollegePrefix\", \"TransactionOutcome\", \"ProviderSoftwareId\", ");
            copySql.Append("\"ProviderSoftwareVersion\", \"LocationIpAddress\", \"SourceIpAddress\"");
            copySql.Append("from \"PharmanetTransactionLog\" ptl ");
            copySql.Append($"where \"TxDateTime\" > current_date + interval '-{numberInDays}' day ");

            // if there is existing records with the max transaction Id, use it to filter the number of records copy over
            if (maxTransactionId.HasValue)
            {
                copySql.Append($"and \"TransactionId\" > '{maxTransactionId}' ");
            }

            _context.Database.SetCommandTimeout(300);
            int result = await _context.Database.ExecuteSqlRawAsync(copySql.ToString());

            return result;
        }
    }
}
