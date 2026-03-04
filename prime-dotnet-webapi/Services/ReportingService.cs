using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

using Prime.Models;
using Prime.HttpClients;
using System.Text;
using FluentValidation.Validators;

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

        public async Task<int> ArchiveTransactionLogAsync(int numberOfDays)
        {
            // if there is already record in archive table, do not execute the archive process as the archive log needs to be pushed to S3 storage
            // before the new process can be executed.
            if (await _context.PharmanetTransactionLogArchives.AnyAsync())
            {
                return 0;
            }

            StringBuilder archiveSql = new StringBuilder();
            archiveSql.Append("insert into \"PharmanetTransactionLogArchive\"");
            archiveSql.Append("(\"Id\", \"CreatedTimeStamp\", \"TransactionId\", \"TxDateTime\", \"UserId\", \"PharmacyId\", \"TransactionType\", ");
            archiveSql.Append("\"TransactionSubType\", \"PractitionerId\", \"CollegePrefix\", \"TransactionOutcome\", \"ProviderSoftwareId\", ");
            archiveSql.Append("\"ProviderSoftwareVersion\", \"LocationIpAddress\", \"SourceIpAddress\")");
            archiveSql.Append("SELECT \"Id\", \"CreatedTimeStamp\", \"TransactionId\", \"TxDateTime\", \"UserId\", \"PharmacyId\", \"TransactionType\", ");
            archiveSql.Append("\"TransactionSubType\", \"PractitionerId\", \"CollegePrefix\", \"TransactionOutcome\", \"ProviderSoftwareId\", ");
            archiveSql.Append("\"ProviderSoftwareVersion\", \"LocationIpAddress\", \"SourceIpAddress\"");
            archiveSql.Append("from \"PharmanetTransactionLog\" ptl ");
            archiveSql.Append($"where \"TxDateTime\" < (select min(\"TxDateTime\") + interval '{numberOfDays} day' from \"PharmanetTransactionLog\" )");

            _context.Database.SetCommandTimeout(300);
            int result = await _context.Database.ExecuteSqlRawAsync(archiveSql.ToString());

            // delete the archived records from main log table
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.Append("delete from \"PharmanetTransactionLog\" ptl ");
            deleteSql.Append($"where ptl.\"Id\" in (select \"Id\" from \"PharmanetTransactionLogArchive\")");

            await _context.Database.ExecuteSqlRawAsync(deleteSql.ToString());

            return result;
        }

        public async Task<string> GetArchiveTransactionLogStringAsync()
        {
            _context.Database.SetCommandTimeout(300);
            var log = await _context.PharmanetTransactionLogArchives
                .Select(l => l.TxDateTime)
                .GroupBy(l => 1)
                .Select(ll => new
                {
                    MaxDate = ll.Max(),
                    MinDate = ll.Min(),
                }).FirstOrDefaultAsync();

            if (log == null)
            {
                return null;
            }
            else
            {
                return log.MaxDate == log.MinDate
                    ? $"{log.MaxDate:dd MMM yyyy}"
                    : $"{log.MinDate:dd-MMM-yyyy}_{log.MaxDate:dd-MMM-yyyy}";
            }
        }

        public async Task<int> ClearTransactionLogArchiveAsync()
        {
            int result = await _context.PharmanetTransactionLogArchives.ExecuteDeleteAsync();

            return result;
        }
    }
}
