using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Prime.Models;
using Prime.Models.Plr;
using Prime.ViewModels.Plr;
using Prime.ViewModels;
using Prime.HttpClients;

namespace Prime.Services
{
    public class ReportingService : BaseService, IReportingService
    {
        private const int CalculationPeriodInDays = 14;

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
                var enrolleeCerts = _context.Enrollees
                    .Where(e => e.GPID != null && e.Certifications.Any())
                    .Select(e => new
                    {
                        e.Certifications.FirstOrDefault().LicenseNumber,
                        e.Certifications.FirstOrDefault().Prefix
                    });
                // query the unauthorized access practitioner ID from pharmanet transaction log table
                var questionablePractitionerIds = await _context.PharmanetTransactionLogs
                    .Where(l => l.TxDateTime >= startDate && l.TxDateTime <= endDate)
                    .Where(l => !enrolleeCerts.Where(e => e.LicenseNumber == l.PractitionerId && e.Prefix == l.CollegePrefix).Any())
                    .Where(l => !_context.Practitioner.Where(p => p.PracRefId == l.CollegePrefix && p.CollegeId == l.PractitionerId).Any())
                    .Select(l => new
                    {
                        l.PractitionerId,
                        l.CollegePrefix
                    }).ToListAsync();

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
                _logger.LogError($"Error: PopulatePractitionerTableAsync - Message: {ex.Message}");
                result = $"Error: PopulatePractitionerTableAsync - Message: {ex.Message}";
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
                    }
                    p.ProcessedDate = DateTime.Now;

                    _context.Update(p);
                }
                result = $"practitionerRecords count = {practitionerRecords.Count}";

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: PopulatePractitionerTableAsync - Message: {ex.Message}");
                result = $"Error: PopulatePractitionerTableAsync - Message: {ex.Message}";
            }

            return result;
        }
    }
}