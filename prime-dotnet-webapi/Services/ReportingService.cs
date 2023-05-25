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

        public async Task PopulatePractitionerTableAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
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

                var enrolleeLicenseNumbers = await _context.Enrollees
                    .Where(e => e.GPID != null && e.Certifications.Any())
                    .Select(e => e.Certifications.FirstOrDefault().LicenseNumber)
                    .ToListAsync();

                var questionablePractitionerIds = await _context.PharmanetTransactionLogs
                    .Where(l => l.TxDateTime >= startDate && l.TxDateTime <= endDate)
                    .Where(l => !enrolleeLicenseNumbers.Contains(l.PractitionerId))
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

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: PopulatePractitionerTableAsync - Message: {ex.Message}");
            }
        }

        public async Task UpdatePractitionerTableAsync()
        {
            try
            {
                var practitonerRecords = await _context.Practitioner.Where(p => p.ProcessedDate == null && p.FirstName == null)
                .Select(p => p).ToListAsync();

                foreach (var p in practitonerRecords)
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

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: PopulatePractitionerTableAsync - Message: {ex.Message}");
            }
        }
    }
}
